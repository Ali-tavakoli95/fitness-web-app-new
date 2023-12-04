namespace api.Controllers;

public class AccountController(IAccountRepository _accountRepository) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<LoggedInDto>> Register(RegisterDto userInput, CancellationToken cancellationToken)
    {
        if (userInput.Password != userInput.ConfirmPassword)
            return BadRequest("گذرواژه ها مطابقت ندارند!");

        LoggedInDto? loggedInDto = await _accountRepository.CreateAsync(userInput, cancellationToken);

        if (loggedInDto is null)
            return BadRequest("ایمیل/نام کاربری گرفته شده است.");

        return loggedInDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoggedInDto>> Login(LoginDto userInput, CancellationToken cancellationToken)
    {
        LoggedInDto? userDto = await _accountRepository.LoginAsync(userInput, cancellationToken);

        if (userDto is null)
            return Unauthorized("نام کاربری یا رمز عبور اشتباه است.");

        return userDto;
    }
}
