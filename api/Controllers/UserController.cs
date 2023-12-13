using Microsoft.AspNetCore.Authorization;

namespace api.Controllers;

[Authorize] // AllowAnonymous can NOT be here!
public class UserController(IUserRepository _userRepository) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken cancellationToken)
    {
        List<UserDto> userDtos = await _userRepository.GetAllAsync(cancellationToken);

        if (!userDtos.Any()) // []
            return NoContent();

        return userDtos;
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<UserDto>> GetById(CancellationToken cancellationToken)
    {
        UserDto? userDto = await _userRepository.GetByIdAsync(ClaimPrincipalExtensions.GetUserId(User), cancellationToken);

        if (userDto is null)
            return NotFound("No user was found");

        return userDto;
    }

}
