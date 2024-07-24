using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System.Text.Json.Serialization;
using Web.Hubs;
using Web.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PRN221_ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Test") ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.")));
builder.Services.AddScoped<IRepository<Movie>, MovieRepository>();
builder.Services.AddScoped<IRepository<Seat>, SeatRepository>();
builder.Services.AddScoped<IRepository<Theater>, TheaterRepository>();
builder.Services.AddScoped<IRepository<Showtime>, ShowTimeRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.IsEssential = true;
});
var app = builder.Build();
app.MapHub<SignalRHub>("/abcd");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
app.UseAuthorization();

app.MapRazorPages();

app.Run();
