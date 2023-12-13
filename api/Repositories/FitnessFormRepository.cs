namespace api.Repositories;

public class FitnessFormRepository : IFitnessFormRepository
{
    private const string _collectionName = "fit-form-users";
    private readonly IMongoCollection<FitFormUser>? _collection;

    public FitnessFormRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<FitFormUser>(_collectionName);
    }

    public async Task<UserFormDto?> CreateAsync(RegisterFormDto userIn, CancellationToken cancellationToken)
    {
        bool doesExistEmail = await _collection.Find<FitFormUser>(user => user.Email == userIn.Email.ToLower().Trim()).AnyAsync(cancellationToken);

        if (doesExistEmail)
            return null;

        FitFormUser fitFormUser = _Mappers.ConvertRegisterFormDtoToFitFormUser(userIn);

        if (_collection is not null)
            await _collection.InsertOneAsync(fitFormUser, null, cancellationToken);

        if (fitFormUser.Id is not null)
        {
            UserFormDto userFormDto = _Mappers.ConvertFitFormUserToUserFormDto(fitFormUser);

            return userFormDto;
        }
        return null;
    }

    public async Task<List<UserFormDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<FitFormUser> fitFormUsers = await _collection.Find<FitFormUser>(new BsonDocument()).ToListAsync(cancellationToken);

        List<UserFormDto> userFormDtos = new List<UserFormDto>();

        if (fitFormUsers.Any())
        {
            foreach (FitFormUser fitFormUser in fitFormUsers)
            {
                UserFormDto userFormDto = _Mappers.ConvertFitFormUserToUserFormDto(fitFormUser);

                userFormDtos.Add(userFormDto);
            }

            return userFormDtos;
        }

        return userFormDtos;
    }

    public async Task<UserFormDto?> GetFitUserAsync(string userId, CancellationToken cancellationToken)
    {
        FitFormUser fitFormUser = await _collection.Find(v => v.Id == userId).FirstOrDefaultAsync();

        if (fitFormUser.Id is not null)
        {
            UserFormDto userDto = _Mappers.ConvertFitFormUserToUserFormDto(fitFormUser);

            return userDto;
        }

        return null;
    }

    public async Task<UpdateResult> UpdateByFitIdAsync(string userId, UpdateFormDto userIn, CancellationToken cancellationToken)
    {
        var updatedFit = Builders<FitFormUser>.Update
        .Set((FitFormUser doc) => doc.FirstName, userIn.FirstName)
        .Set(doc => doc.LastName, userIn.LastName)
        .Set(doc => doc.Email, userIn.Email)
        .Set(doc => doc.Mobile, userIn.Mobile)
        .Set(doc => doc.Weight, userIn.Weight)
        .Set(doc => doc.Height, userIn.Height)
        .Set(doc => doc.Bmi, userIn.Bmi)
        .Set(doc => doc.BmiResult, userIn.BmiResult)
        .Set(doc => doc.Gender, userIn.Gender)
        .Set(doc => doc.RequireTrainer, userIn.RequireTrainer)
        .Set(doc => doc.Package, userIn.Package)
        .Set(doc => doc.Important, userIn.Important)
        .Set(doc => doc.HaveGymBefore, userIn.HaveGymBefore)
        .Set(doc => doc.EnquiryDate, userIn.EnquiryDate);

        return await _collection.UpdateOneAsync<FitFormUser>(doc => doc.Id == userId, updatedFit, null, cancellationToken);
    }

    public async Task<DeleteResult> DeleteAsync(string userId, CancellationToken cancellationToken)
    {
        return await _collection.DeleteOneAsync<FitFormUser>(doc => doc.Id == userId);
    }
}
