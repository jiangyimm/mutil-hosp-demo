using Microsoft.EntityFrameworkCore;
using multi_hosp_demo.Entities;
using multi_hosp_demo.MultiHosp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<QcContext>(option => option.UseNpgsql(builder.Configuration["DbconnectString:Control"],
                option =>
                {
                    //option.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "control");
                    //option.MaxBatchSize(100);
                    //option.MigrationsAssembly("Synyi.Record.QualityControl.Service");
                }));

//AddMultiHosp
builder.Services.AddMultiHosp();

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
