namespace api.Repositories;

public class UserRepository : IUserRepository
{
    IMongoCollection<AppFitUser>? _collection;

    public UserRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppFitUser>("users");
    }

    public async Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<AppFitUser> appFitUsers = await _collection.Find<AppFitUser>(new BsonDocument()).ToListAsync(cancellationToken);

        List<UserDto> userDtos = new List<UserDto>();

        if (appFitUsers.Any())
        {
            foreach (AppFitUser appFitUser in appFitUsers)
            {
                UserDto userDto = _Mappers.ConvertAppFitUserToUserDto(appFitUser);

                userDtos.Add(userDto);
            }

            return userDtos;
        }

        return userDtos;
    }

    public async Task<UserDto?> GetByIdAsync(string? userId, CancellationToken cancellationToken)
    {
        AppFitUser appFitUser = await _collection.Find<AppFitUser>(user => user.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (appFitUser is not null)
            return _Mappers.ConvertAppFitUserToUserDto(appFitUser);

        return null;
    }
}
