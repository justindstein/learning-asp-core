using learning_asp_core.Data;
using learning_asp_core.Services;
using learning_asp_core.Utils.Configs;

var builder = WebApplication.CreateBuilder(args);

////
//// Configure the web host to listen on specific ports
//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    // Configure the HTTP port (non-SSL)
//    serverOptions.ListenAnyIP(8080);

//    // Configure the HTTPS port (SSL)
//    serverOptions.ListenAnyIP(8081, listenOptions =>
//    {
//        listenOptions.UseHttps();
//    });
//});
////


// Configucre database.
// todo

// Register HttpClient with retry policy using extension method
builder.Services.AddHttpClient("retryClient")
    .AddPolicyHandler(HttpClientConfig.GetRetryPolicy());

// Add services to the container.
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<WorkflowService>();
builder.Services.AddScoped<AzureService>();
builder.Services.AddScoped<AheadService>();
builder.Services.AddScoped<ApprovalService>();
builder.Services.AddScoped<GoogleService>();

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
