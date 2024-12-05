using learning_asp_core.Data;
using learning_asp_core.Services;
using learning_asp_core.Utils;

var builder = WebApplication.CreateBuilder(args);

// Configure database.
// todo

// Register HttpClient with retry policy using extension method
builder.Services.AddHttpClient("retryClient")
    .AddPolicyHandler(HttpClientConfig.GetRetryPolicy());

// Add services to the container.
// builder.Services.AddScoped<WorkflowService>();
builder.Services.AddScoped<WorkflowService>();
builder.Services.AddScoped<AppDbContext>();

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
