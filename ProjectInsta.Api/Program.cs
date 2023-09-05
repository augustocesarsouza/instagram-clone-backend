using ProjectInsta.Application.MyHubs;
using ProjectInsta.Infra.IoC;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolity", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.UseWebSockets();
app.UseRouting();

app.UseAuthorization();
app.UseCors("CorsPolity");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MessageHub>("/chat");
});

app.Run();
