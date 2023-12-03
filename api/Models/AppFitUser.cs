namespace api.Models;

public record AppFitUser(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    string Email,
    byte[]? PasswordSalt, // array
    byte[]? PasswordHash,
    int Age,
    string Gender,
    string Country,
    string City
    
);
