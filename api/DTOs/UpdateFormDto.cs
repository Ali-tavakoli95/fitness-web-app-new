namespace api.DTOs;

public record UpdateFormDto(
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    [MinLength(3), MaxLength(30)] string UserName,
    [MinLength(11), MaxLength(11)] string Mobile,
    [DataType(DataType.Password), MinLength(7), MaxLength(20)] string Password,
    [DataType(DataType.Password), MinLength(7), MaxLength(20)] string ConfirmPassword,
    [MinLength(3), MaxLength(30)] string FirstName,
    [MinLength(3), MaxLength(30)] string LastName,
    int Weight,
    double Height,
    double Bmi,
    string BmiResult,
    string Gender,
    string RequireTrainer,
    string Package,
    string[] Important,
    string HaveGymBefore,
    string EnquiryDate
);
