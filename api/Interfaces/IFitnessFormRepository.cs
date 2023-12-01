namespace api.Interfaces;

public interface IFitnessFormRepository
{
    public Task<FitFormUser> CreateAsync(FitFormUser userInput, CancellationToken cancellationToken);

    public Task<List<FitFormUser>> GetAllAsync(CancellationToken cancellationToken);

    public Task<FitFormUser> GetFitUserAsync(string userId, CancellationToken cancellationToken);

    public Task<UpdateResult> UpdateByFitIdAsync(string userId, FitFormUser userIn, CancellationToken cancellationToken);

    public Task<DeleteResult> DeleteAsync(string userId, CancellationToken cancellationToken);

}
