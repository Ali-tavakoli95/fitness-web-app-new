namespace api.Controllers;

public class FitnessController(IFitnessFormRepository _fitnessFormRepository) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserFormDto>> Create(RegisterFormDto userIn, CancellationToken cancellationToken)
    {
        UserFormDto? userFormDto = await _fitnessFormRepository.CreateAsync(userIn, cancellationToken);

        if (userFormDto is null)
            return BadRequest("Email is taken.");

        return userFormDto;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<UserFormDto>>> GetAll(CancellationToken cancellationToken)
    {
        List<UserFormDto> userFormDtos = await _fitnessFormRepository.GetAllAsync(cancellationToken);

        if (!userFormDtos.Any())
            return NoContent();

        return userFormDtos;
    }

    [HttpGet("get-fit-user-by-id/{userId}")]
    public async Task<ActionResult<UserFormDto>> GetFitUser(string userId, CancellationToken cancellationToken)
    {
        UserFormDto? userFormDto = await _fitnessFormRepository.GetFitUserAsync(userId, cancellationToken);

        if (userFormDto is null)
            return NoContent();

        return userFormDto;
    }

    [HttpPut("update/{userId}")]
    public async Task<ActionResult<UpdateResult>> UpdateByFitId(string userId, UpdateFormDto userIn, CancellationToken cancellationToken)
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
