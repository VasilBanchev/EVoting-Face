using BlazorServer.Pages;
using BlazorServer.Services;
using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Server;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PresentationLayerBlazor.Data;
using ServiceLayer;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("VotingDBContextConnection") ?? throw new InvalidOperationException("Connection string 'VotingDBContextConnection' not found.");

        builder.Services.AddDbContext<VotingDBContext>(options => options.UseSqlServer(connectionString));

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();


        builder.Services.AddScoped<CustomAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());

        builder.Services.AddScoped<ErrorModel>();
        builder.Services.AddScoped<IdentityManager>();
        builder.Services.AddScoped<IdentityContext>();
        builder.Services.AddScoped<PartyManager>();
        builder.Services.AddScoped<PartyContext>();

//        builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<VotingDBContext>().AddDefaultTokenProviders();


        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 5;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;

            // Log in required.
            options.SignIn.RequireConfirmedAccount = false; // default
            options.SignIn.RequireConfirmedEmail = false; // default
        });





        builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                        .AddRoles<IdentityRole>()  // <------
                        .AddEntityFrameworkStores<VotingDBContext>();
        builder.Services.AddScoped<UserManager<User>>();


        builder.Services.AddSingleton<WeatherForecastService>();

        builder.Services.AddServerSideBlazor(options =>
        {
            options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(5);
        });


        builder.Services.AddSignalR(e => {
            e.MaximumReceiveMessageSize = 102400000;
        });

        builder.Services.AddServerSideBlazor().AddCircuitOptions(o =>
        {
           
                o.DetailedErrors = true;
            
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}