using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ForumFörFöräldrar.Data;
using ForumFörFöräldrar.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ForumFörFöräldrar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ForumFörFöräldrarContextConnection") ?? throw new InvalidOperationException("Connection string 'ForumFörFöräldrarContextConnection' not found.");

            builder.Services.AddDbContext<ForumFörFöräldrarContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ForumFörFöräldrarUserT>(
            options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ForumFörFöräldrarContext>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("HasToBeAdmin",
                    policy => policy.RequireRole("Admin"));
            });

            //builder.Services.AddRazorPages(options =>
            //{
            //    options.Conventions.AuthorizeFolder("/Admin", "HasToBeAdmin");
            //});

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
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}