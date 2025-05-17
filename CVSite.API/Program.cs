using CVSite.SERVICE;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddGitHubIntegration(options =>
{
    builder.Configuration.GetSection(nameof(GitHubOptions)).Bind(options);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//1
//builder.Services.Configure<GitHubOptions>(
    //builder.Configuration.GetSection("Github"));
    //builder.Configuration.GetSection(nameof(GitHubOptions)));
//builder.Services.AddScoped<IGitHubService, GitHubService>();

//2
//builder.Services.Configure<GitHubOptions>(builder.Configuration.GetSection(nameof(GitHubOptions)));
//builder.Services.AddGitHubIntegration(options => builder.Configuration.GetSection(nameof(GitHubOptions)).Bind(options));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
