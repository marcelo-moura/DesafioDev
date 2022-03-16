using DesafioDev.Application.AutoMapper.Mappings;
using DesafioDev.Infra.Data.Context;
using DesafioDev.Infra.IoC;
using DesafioDev.WebAPI.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddConfigurationApi();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.ResolveDependencias(builder.Configuration);
builder.Services.AddJWTConfiguration(builder.Configuration);
builder.Services.AddAutoMapper(typeof(ViewModelToDomainProfile), typeof(DomainToViewModelProfile));

builder.Services.AddDbContext<DesafioDevContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

if (builder.Environment.IsDevelopment())
    builder.Services.MigrateDatabase(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseConfigurationApi();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerConfiguration(apiVersionDescriptionProvider);

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
});

app.Run();
