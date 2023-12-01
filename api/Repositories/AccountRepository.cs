namespace api.Repositories;

public class AccountRepository : IAccountRepository
{
    private const string _collectionName = "users";
    private readonly IMongoCollection<AppUser>? _collection;
    private readonly ITokenService _tokenService;

    public AccountRepository(IMongoClient client, IMongoDbSettings dbSettings, ITokenService tokenService)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppUser>(_collectionName);
        _tokenService = tokenService;
    }

    public async Task<LoggedInDto?> CreateAsync(RegisterDto userInput, CancellationToken cancellationToken)
    {
        bool doesAccountExist = await _collection.Find<AppUser>(user =>
        user.Email == userInput.Email.ToLower().Trim()).AnyAsync(cancellationToken);

        if (doesAccountExist)
            return null;

        using var hmac = new HMACSHA512();

        AppUser appUser = new AppUser(
            Id: null,
            Email: userInput.Email.ToLower().Trim(),
            PasswordSalt: hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.Password)),
            PasswordHash: hmac.Key
        );

        if (_collection is not null)
            await _collection.InsertOneAsync(appUser, null, cancellationToken);

        if (appUser.Id is not null)
        {
            LoggedInDto loggedInDto = new LoggedInDto(
                Id: appUser.Id,
                Token: _tokenService.CreateToken(appUser),
                Email: appUser.Email
            );

            return loggedInDto;
        }

        return null;
    }

    public async Task<LoggedInDto?> LoginAsync(LoginDto userInput, CancellationToken cancellationToken)
    {
        AppUser appUser = await _collection.Find<AppUser>(user =>
            user.Email == userInput.Email.ToLower().Trim()).FirstOrDefaultAsync(cancellationToken);

        if (appUser is null)
            return null;

        using var hmac = new HMACSHA3_512(appUser.PasswordSalt!);

        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.Password));

        if (appUser.PasswordHash is not null && appUser.PasswordHash.SequenceEqual(ComputeHash))
        {
            if (appUser.Id is not null)
            {
                return new LoggedInDto(
                    Id: appUser.Id,
                    Token: _tokenService.CreateToken(appUser),
                    Email: appUser.Email
                );
            }
        }

        return null;
    }
}
