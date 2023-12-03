namespace api.Repositories;

public class AccountRepository : IAccountRepository
{
    private const string _collectionName = "users";
    private readonly IMongoCollection<AppFitUser>? _collection;
    private readonly ITokenService _tokenService;

    public AccountRepository(IMongoClient client, IMongoDbSettings dbSettings, ITokenService tokenService)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppFitUser>(_collectionName);
        _tokenService = tokenService;
    }

    public async Task<LoggedInDto?> CreateAsync(RegisterDto userInput, CancellationToken cancellationToken)
    {
        bool doesAccountExist = await _collection.Find<AppFitUser>(user =>
        user.Email == userInput.Email.ToLower().Trim()).AnyAsync(cancellationToken);

        if (doesAccountExist)
            return null;

        using var hmac = new HMACSHA512();

        AppFitUser appFitUser = new AppFitUser(
            Id: null,
            Email: userInput.Email.ToLower().Trim(),
            PasswordSalt: hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.Password)),
            PasswordHash: hmac.Key,
            Age: userInput.Age,
            Gender: userInput.Gender,
            Country: userInput.Country,
            City: userInput.City
        );

        if (_collection is not null)
            await _collection.InsertOneAsync(appFitUser, null, cancellationToken);

        if (appFitUser.Id is not null)
        {
            LoggedInDto loggedInDto = new LoggedInDto(
                Id: appFitUser.Id,
                Token: _tokenService.CreateToken(appFitUser),
                Email: appFitUser.Email
            );

            return loggedInDto;
        }

        return null;
    }

    public async Task<LoggedInDto?> LoginAsync(LoginDto userInput, CancellationToken cancellationToken)
    {
        AppFitUser appFitUser = await _collection.Find<AppFitUser>(user =>
            user.Email == userInput.Email.ToLower().Trim()).FirstOrDefaultAsync(cancellationToken);

        if (appFitUser is null)
            return null;

        using var hmac = new HMACSHA512(appFitUser.PasswordSalt!);

        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.Password));

        if (appFitUser.PasswordHash is not null && appFitUser.PasswordHash.SequenceEqual(ComputeHash))
        {
            if (appFitUser.Id is not null)
            {
                return new LoggedInDto(
                    Id: appFitUser.Id,
                    Token: _tokenService.CreateToken(appFitUser),
                    Email: appFitUser.Email
                );
            }
        }

        return null;
    }
}
