using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineExam.DataAccess.Repository.IRepository;
using OnlineExam.DataAccess.Repository;
using OnlineExamination.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using OnlineExam.Utility;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IEmailSender,EmailSender>();
builder.Services.AddRazorPages();
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
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages(); //to map razor pages (for identity page)
app.MapControllerRoute(
    name: "default",
    pattern: "{area=DefaultPage}/{controller=Home}/{action=Index}/{id?}");


app.Run();
