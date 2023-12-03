namespace api.Interfaces;

public interface IFitnessFormRepository
{
    public Task<UserFormDto?> CreateAsync(RegisterFormDto userInput, CancellationToken cancellationToken);

    public Task<List<UserFormDto>> GetAllAsync(CancellationToken cancellationToken);

    public Task<UserFormDto?> GetFitUserAsync(string userId, CancellationToken cancellationToken);

    public Task<UpdateResult> UpdateByFitIdAsync(string userId, UpdateFormDto userIn, CancellationToken cancellationToken);

    public Task<DeleteResult> DeleteAsync(string userId, CancellationToken cancellationToken);

}
