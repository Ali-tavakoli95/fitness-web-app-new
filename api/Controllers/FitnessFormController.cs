namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FitnessController(IFitnessFormRepository _fitnessFormRepository) : BaseApiController
{
    public async Task<ActionResult<FitFormUser>> Create(FitFormUser userIn, CancellationToken cancellationToken)
    {
        FitFormUser fitFormUser = await _fitnessFormRepository.CreateAsync(userIn, cancellationToken);

        return fitFormUser;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<FitFormUser>>> GetAll(CancellationToken cancellationToken)
    {
        List<FitFormUser> fitFormUsers = await _fitnessFormRepository.GetAllAsync(cancellationToken);

        if (!fitFormUsers.Any())
            return NoContent();

        return fitFormUsers;
    }

    [HttpGet("get-fit-user-by-id/{userId}")]
    public async Task<ActionResult<FitFormUser>> GetFitUser(string userId, CancellationToken cancellationToken)
    {
        FitFormUser fitFormUser = await _fitnessFormRepository.GetFitUserAsync(userId, cancellationToken);

        if (fitFormUser is null)
            return NoContent();

        return fitFormUser;
    }

    [HttpPut("update/{userId}")]
    public async Task<ActionResult<UpdateResult>> UpdateByFitId(string userId, FitFormUser userIn, CancellationToken cancellationToken)
    {
        UpdateResult updateResult = await _fitnessFormRepository.UpdateByFitIdAsync(userId, userIn, cancellationToken);

        return updateResult;
    }

    [HttpDelete("delete/{userId}")]
    public async Task<ActionResult<DeleteResult>> Delete(string userId, CancellationToken cancellationToken)
    {
        DeleteResult deleteResult = await _fitnessFormRepository.DeleteAsync(userId, cancellationToken);

        return deleteResult;
    }
}
