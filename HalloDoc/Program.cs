
using DataLayer.Models;
using BusinessLayer.Repository.Implementation;
using BusinessLayer.Repository.Interface;
using BusinessLayer.Utility;

using Microsoft.EntityFrameworkCore;
using DataLayer.DTO.AdminDTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HallodocContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddSingleton<BusinessLayer.Utility.IEmailSender, EmailSender>();
builder.Services.AddTransient<IAdminDashboard, AdminDashboard>();
builder.Services.AddTransient<IRequestTable, RequestTable>();
builder.Services.AddTransient<IPatientRequest, PatientRequest>();
builder.Services.AddTransient<IFamilyRequest, FamilyRequest>();
builder.Services.AddTransient<IBusinessRequest, BusinessRequest>();
builder.Services.AddTransient<IConciergeRequest, ConciergeRequest>();
builder.Services.AddTransient<IRequestForMe, RequestForMe>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
