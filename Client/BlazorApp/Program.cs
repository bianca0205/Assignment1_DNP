using BlazorApp.Auth;
using BlazorApp.Components;
using BlazorApp.Services;
using BlazorApp.Services.Comment;
using BlazorApp.Services.Post;
using BlazorApp.Services.User;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register HttpClient with base address pointing to Web API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7005")
});

// Register your HTTP services
builder.Services.AddScoped<IUserService, HttpUserService>();
builder.Services.AddScoped<IPostService, HttpPostService>();
builder.Services.AddScoped<ICommentService, HttpCommentService>();
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapStaticAssets();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();