namespace api.DTOs;

public record UserFormDto(
    string Id,
    string FirstName,
    string LastName,
    string Email,
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