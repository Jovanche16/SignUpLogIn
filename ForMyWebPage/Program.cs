using ForMyWebPage.Models;
using ForMyWebPage.Repositories;
using ForMyWebPage.Repositories.IRepositories;
using ForMyWebPage.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentityCore<User>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AlbumAppContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<AccessTokenGenerator>();
builder.Services.AddSingleton<RefreshTokenGenerator>();
builder.Services.AddSingleton<RefreshTokenValidator>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddDbContext<AlbumAppContext>();

builder.Services.AddCors(o =>o.AddPolicy("corsPolicy", build =>
{
    build.WithOrigins("http://localhost:3000","http://localhost:3001").AllowAnyMethod().AllowAnyHeader();
}
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("corsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();