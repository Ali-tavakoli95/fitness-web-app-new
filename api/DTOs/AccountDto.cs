namespace api.DTOs;

public record RegisterDto(
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    [DataType(DataType.Password), Length(7, 20, ErrorMessage = "Min of 7 and max of 20 chars are requried")] string Password,
    [DataType(DataType.Password), Length(7, 20)] string ConfirmPassword,
    [Range(18, 99)] int Age,
    [Length(3, 20)] string Gender,
    [Length(3, 30)] string Country,
    [Length(3, 30)] string City
);

public record LoginDto(
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
    string Email,
    [DataType(DataType.Password), MinLength(7), MaxLength(20)]
    string Password
);

public record LoggedInDto(
    string Id,
    string Email,
    string Token
);
