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
        List<AppFitUser> appUsers = await _collection.Find<AppFitUser>(new BsonDocument()).ToListAsync(cancellationToken);

        List<UserDto> userDtos = new List<UserDto>();

        if (appUsers.Any())
        {
            foreach (AppFitUser appUser in appUsers)
            {
                UserDto userDto = new UserDto(
                    Id: appUser.Id!,
                    Email: appUser.Email
                );

                userDtos.Add(userDto);
            }

            return userDtos;
        }

        return userDtos;
    }

    public async Task<UserDto?> GetByIdAsync(string userId, CancellationToken cancellationToken)
    {
        AppFitUser appFitUser = await _collection.Find<AppFitUser>(user => user.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (appFitUser is not null)
            return new UserDto(
                Id: appFitUser.Id!,
                Email: appFitUser.Email
            );

        return null;
    }
}
