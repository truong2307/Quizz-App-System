using Microsoft.EntityFrameworkCore;
using PersonalApp.DataAccess.Data;
using PersonalApp.DataAccess.Hubs;
using PersonalApp.DataAccess.Initializer;
using PersonalApp.ConfigureServicesExtension;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureServiceLifeTime();
builder.Services.ConfigureService();

builder.Services.AddSignalR();

var app = builder.Build();

//initial user
var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
using (var scope = scopedFactory.CreateScope())
{
    var seeding = scope.ServiceProvider.GetService<IndentityUserSeeding>();
    await seeding.Initialize();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

//app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<UserHub>("/hubs/userCount");

app.Run();
