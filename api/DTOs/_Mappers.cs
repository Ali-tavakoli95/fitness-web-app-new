namespace api.DTOs;

public static class _Mappers
{
    public static AppFitUser ConvertRegisterDtoToAppFitUser(RegisterDto userInput)
    {
        using var hmac = new HMACSHA512();

        return new AppFitUser(
            Id: null,
            Email: userInput.Email.ToLower().Trim(),
            PasswordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.Password)),
            PasswordSalt: hmac.Key,
            Age: userInput.Age,
            Gender: userInput.Gender,
            Country: userInput.Country,
            City: userInput.City
        );
    }

    public static LoggedInDto ConvertAppFitUserToLoggedInDto(AppFitUser appFitUser, string token)
    {
        return new LoggedInDto(
                Id: appFitUser.Id!,
                Token: token,
                Email: appFitUser.Email
            );
    }

    public static UserDto ConvertAppFitUserToUserDto(AppFitUser appFitUser)
    {
        return new UserDto(
             Id: appFitUser.Id!,
             Email: appFitUser.Email
        );
    }

    public static FitFormUser ConvertRegisterFormDtoToFitFormUser(RegisterFormDto userIn)
    {
        return new FitFormUser(
            Id: null,
            FirstName: userIn.FirstName,
            LastName: userIn.LastName,
            Email: userIn.Email,
            Mobile: userIn.Mobile,
            Weight: userIn.Weight,
            Height: userIn.Height,
            Bmi: userIn.Bmi,
            BmiResult: userIn.BmiResult,
            Gender: userIn.Gender,
            RequireTrainer: userIn.RequireTrainer,
            Package: userIn.Package,
            Important: userIn.Important,
            HaveGymBefore: userIn.HaveGymBefore,
            EnquiryDate: userIn.EnquiryDate
        );
    }

    public static UserFormDto ConvertFitFormUserToUserFormDto(FitFormUser fitFormUser)
    {
        return new UserFormDto(
                Id: fitFormUser.Id!,
                Email: fitFormUser.Email,
                Mobile: fitFormUser.Mobile,
                FirstName: fitFormUser.FirstName,
                LastName: fitFormUser.LastName,
                Weight: fitFormUser.Weight,
                Height: fitFormUser.Height,
                Bmi: fitFormUser.Bmi,
                BmiResult: fitFormUser.BmiResult,
                Gender: fitFormUser.Gender,
                RequireTrainer: fitFormUser.RequireTrainer,
                Package: fitFormUser.Package,
                Important: fitFormUser.Important,
                HaveGymBefore: fitFormUser.HaveGymBefore,
                EnquiryDate: fitFormUser.EnquiryDate
            );
    }
}
