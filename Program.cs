using Microsoft.EntityFrameworkCore;
using learning_asp_core.Data;
using learning_asp_core.Services;
using learning_asp_core.Utils;

var builder = WebApplication.CreateBuilder(args);

// Configure database.
builder.Services.AddDbContext<ApiContext>(
    opt => opt.UseInMemoryDatabase("OrderWorkflowDb")
);

// Register HttpClient with retry policy using extension method
builder.Services.AddHttpClient("retryClient")
    .AddPolicyHandler(HttpClientConfig.GetRetryPolicy());

// Add Db Context
builder.Services.AddDbContext<AwsContext>(options =>
{
    options.UseSqlServer("Server=localhost;Database=aws;User Id=sa;Password=Ahead123!;Trusted_Connection=True;");
});

// Add services to the container.
builder.Services.AddSingleton<WorkflowService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
