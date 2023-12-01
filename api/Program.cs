var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddRepositoryServices();

var app = builder.Build();

// app.UseHttpsRedirection(); disable https/ssl for development only! 

app.UseCors(); // this line is added

app.UseAuthentication(); // this line has to be between Cors and Authorization!

app.UseAuthorization();

app.MapControllers();

app.Run();
