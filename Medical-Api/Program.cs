using M_Core.UserStatics;
using M_Services;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JWT"));
        builder.Services.AddApplicationServices(builder.Configuration.GetConnectionString("constr"));
        builder.Services.AddApplicationIdentity();
        builder.Services.AddApplicationJwtAuth(builder.Configuration.GetSection("JWT").Get<JwtConfig>());

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        //await app.SeedDataAsync();

        app.Run();
    }
}