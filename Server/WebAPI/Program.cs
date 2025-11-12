using FileRepositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Register repositories
builder.Services.AddScoped<IUserRepository, UserFileRepository>();
builder.Services.AddScoped<IPostRepository, PostFileRepository>();
builder.Services.AddScoped<ICommentRepository, CommentFileRepository>();
builder.Services.AddScoped<ISubForumRepository, SubForumFileRepository>();

// Enable CORS so the Blazor app can call this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7023") // Blazor app runs here (HTTPS)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Show detailed errors in development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazor");
app.UseAuthorization();
app.MapControllers();

app.Run();