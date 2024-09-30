using GYM_MANAGEMENT.BAL.DTOs.TimeDto;
using GYM_MANAGEMENT.BAL.Infrastructure;
using GYM_MANAGEMENT.BAL.Services;
using GYM_MANAGEMENT.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GYM_MANAGEMENT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IMembersService, MemberService>(); // Registers the MembersService for dependency injection
            builder.Services.AddTransient<ISubscriptionService, SubscriptionService>(); // Registers the SubscriptionService for dependency injection
            builder.Services.AddTransient<IMemberSubscriptionService, MemberSubscriptionService>(); // Registers the MemberSubscriptionService for dependency injection
            builder.Services.AddTransient<ICheckinService, CheckinService>(); // Registers the CheckinService for dependency injection
            builder.Services.AddTransient<IClock, SystemClock>(); // Registers the SystemClock for dependency injection

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
