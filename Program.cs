using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using multi_hosp_demo.Entities;
using multi_hosp_demo.MultiHosp;
using SoapCore;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(p => p.OperationFilter<AddHospCodeHeaderFilter>());

var isMultiHosp = builder.Configuration.GetValue<bool>("IsMultiHospital");
if (isMultiHosp)
{
    builder.Services.AddDbContext<QcContext, MultiHospDbContext>(option =>
            option.UseNpgsql(builder.Configuration["DbconnectString:Control"]));
}
else
{
    builder.Services.AddDbContext<QcContext>(option =>
            option.UseNpgsql(builder.Configuration["DbconnectString:Control"]));
}


//AddMultiHosp
builder.Services.AddMultiHosp();

builder.Services.AddScoped<IService, ServiceA>();
builder.Services.AddScoped<IService, ServiceB>();

builder.Services.AddSoapCore();
builder.Services.AddScoped<ISampleService, SampleService>();

var app = builder.Build();

//UseMultiHosp
app.UseMultiHosp();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var qcContext = app.Services.CreateScope().ServiceProvider.GetService<QcContext>();
await qcContext.Database.MigrateAsync();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<ISampleService>("/ServicePath.asmx", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<ISampleService>("/ServicePath1.asmx", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});

app.Run();
public class AddHospCodeHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "HospCode",
            In = ParameterLocation.Header,
            Description = "院区代码",
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }
}