using Electronic_Application;
using Electronic_Application.Behaviours;
using Electronic_Application.Contracts.Infastructure;
using Electronic_Application.Models.Email;
using Electronic_Infastructure;
using Electronic_Infastructure.Mail;
using Electronic_Infastructure.SignalRHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebSockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
await builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureApplication(builder.Configuration);

builder.Services.AddSignalR();
//Add Cors
//builder.Services.AddCors(opt =>
//{
//    opt.AddPolicy("CorsPolicy", policy =>
//    {
//        var allowOrigins = builder.Configuration.GetSection("AllowOrigins").Get<string[]>();
//        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(allowOrigins).AllowCredentials();       
//    });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((hosts) => true));
});


builder.Services.AddControllers(options =>
                   options.Filters.Add<ApiExceptionFilter>());

//Disabled default model validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//pipeline
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();

//app.UseEndpoints(endpoints => {
//    endpoints.MapControllers();
//    endpoints.MapHub<MessageHub>("/messageHub");
//});
app.MapHub<MessageHub>("/messageHub");
app.MapControllers();

app.Run();
