namespace api.Models;

public record FitFormUser(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
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
