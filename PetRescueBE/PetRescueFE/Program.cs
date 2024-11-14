using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.SignalR;
using PetRescueFE;
using PetRescueFE.Pages.Events;
using PetRescueFE.SignalRealtime;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using PetRescueFE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 15;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(2000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7297/api/"); // Thay bằng URL API gốc của bạn
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<EventGlobalUtility>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<WebService>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
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
app.UseSession();

app.UseAuthorization();
app.MapRazorPages();
app.MapHub<NotificationHub>("/notificationHub");


app.MapRazorPages();

app.Run();
