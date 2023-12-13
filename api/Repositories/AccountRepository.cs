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

        AppFitUser appFitUser = _Mappers.ConvertRegisterDtoToAppFitUser(userInput);

        if (_collection is not null)
            await _collection.InsertOneAsync(appFitUser, null, cancellationToken);

        if (appFitUser.Id is not null)
        {
            // string token = _tokenService.CreateToken(appFitUser);

            LoggedInDto loggedInDto = _Mappers.ConvertAppFitUserToLoggedInDto(
                appFitUser, _tokenService.CreateToken(appFitUser));

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
                string token = _tokenService.CreateToken(appFitUser);

                return _Mappers.ConvertAppFitUserToLoggedInDto(appFitUser, token);
            }
        }

        return null;
    }
}
