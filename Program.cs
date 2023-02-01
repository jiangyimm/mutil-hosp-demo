using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using multi_hosp_demo.Controllers;
using multi_hosp_demo.Entities;
using multi_hosp_demo.MultiHosp;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(p => p.OperationFilter<AddHospCodeHeaderFilter>());

builder.Services.AddDbContext<QcContext>(option => option.UseNpgsql(builder.Configuration["DbconnectString:Control"],
                option =>
                {
                    //option.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "control");
                    //option.MaxBatchSize(100);
                    //option.MigrationsAssembly("Synyi.Record.QualityControl.Service");
                }));

//AddMultiHosp
builder.Services.AddMultiHosp();

builder.Services.AddScoped<IService, ServiceA>();
builder.Services.AddScoped<IService, ServiceB>();

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