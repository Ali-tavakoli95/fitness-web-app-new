namespace api.DTOs;

public record UpdateFormDto(
    [MinLength(3), MaxLength(30)] string FirstName,
    [MinLength(3), MaxLength(30)] string LastName,
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    double Mobile,
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
