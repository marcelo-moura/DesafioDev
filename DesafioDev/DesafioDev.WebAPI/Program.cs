using DesafioDev.Application.AutoMapper.Mappings;
using DesafioDev.Infra.Data.Context;
using DesafioDev.Infra.IoC;
using DesafioDev.WebAPI.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddConfigurationApi();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.ResolveDependencias(builder.Configuration);
builder.Services.AddAutoMapper(typeof(ViewModelToDomainProfile), typeof(DomainToViewModelProfile));

builder.Services.AddDbContext<DesafioDevContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerConfiguration(apiVersionDescriptionProvider);

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseConfigurationApi();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
