
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UI.Data;
using UI.Services;

var builder = WebApplication.CreateBuilder(args);
// Add database connection
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>();
//    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthService, AuthService>();


////Mise en place de l'authentification

builder.Services.AddAuthentication();
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            RequireExpirationTime = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration.GetSection("JwtConfig:Issuer").Value,
//            ValidAudience = builder.Configuration.GetSection("JwtConfig:Audience").Value,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
//                .GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value))
//        };
//    });

// Add logging
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});



builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
   // pattern: "{controller=Authentication}/{action=login}/{id?}");
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
