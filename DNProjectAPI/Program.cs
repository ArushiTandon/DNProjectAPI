using DNProjectAPI.Data;
using DNProjectAPI.iService;
using DNProjectAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

// Add services to the container.

builder.Services.AddControllers();
 // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//My application will use Authentication
builder.Services.AddAuthentication(options =>
{
    //when a request arrives, use JWT to identify the user
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //if user is not authenticated, return 401 unauthorized
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    //The authentication type is JWT
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    //parameters to know the genuinety of token.
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        
        ValidateIssuer = true, //checks who created this token
        ValidateAudience = true, //who is this token intended for?
        ValidateLifetime = true, //expiration of token
        ValidateIssuerSigningKey = true, //Was token signed with my secret key?
        ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer"), //tells who is the issuer
        ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience"), //tell about the audience if differes it rejetcs the token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Key"))) //to check the secret key
    };
});

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

 // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
        