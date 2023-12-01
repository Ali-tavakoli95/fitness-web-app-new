namespace api.Repositories;

public class FitnessFormRepository
{
    private const string _collectionName = "fit-form-users";
    private readonly IMongoCollection<FitFormUser>? _collection;

    public FitnessFormRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<FitFormUser>(_collectionName);
    }

    public async Task<FitFormUser> CreateAsync(FitFormUser userIn, CancellationToken cancellationToken)
    {
        FitFormUser fitFormUser = new FitFormUser(
            Id: null,
            FirstName: userIn.FirstName,
            LastName: userIn.LastName,
            Email: userIn.Email,
            Mobile: userIn.Mobile,
            Weight: userIn.Weight,
            Height: userIn.Height,
            Bmi: userIn.Bmi,
            BmiResult: userIn.BmiResult,
            Gender: userIn.Gender,
            RequireTrainer: userIn.RequireTrainer,
            Package: userIn.Package,
            Important: userIn.Important,
            HaveGymBefore: userIn.HaveGymBefore,
            EnquiryDate: userIn.EnquiryDate
        );

        if (_collection is not null)
            await _collection.InsertOneAsync(fitFormUser, null, cancellationToken);

        return fitFormUser;
    }

    public async Task<List<FitFormUser>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<FitFormUser> fitFormUsers = await _collection.Find<FitFormUser>(new BsonDocument()).ToListAsync(cancellationToken);

        return fitFormUsers;
    }

    public async Task<FitFormUser> GetFitUserAsync(string userId, CancellationToken cancellationToken)
    {
        FitFormUser fitFormUser = await _collection.Find(v => v.Id == userId).FirstOrDefaultAsync();

        return fitFormUser;
    }

    public async Task<UpdateResult> UpdateByFitIdAsync(string userId, FitFormUser userIn, CancellationToken cancellationToken)
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

        return await _collection.UpdateOneAsync<FitFormUser>(doc => doc.Id == userId, updatedFit);
    }

    public async Task<DeleteResult> DeleteAsync(string userId, CancellationToken cancellationToken)
    {
        return await _collection.DeleteOneAsync<FitFormUser>(doc => doc.Id == userId);
    }
}
