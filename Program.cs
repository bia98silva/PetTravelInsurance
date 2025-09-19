using Microsoft.EntityFrameworkCore;
using PetTravelInsurance.Data;
using PetTravelInsurance.Models;
using PetTravelInsurance.Repositories;
using PetTravelInsurance.Services;
using System.Text.Json.Serialization; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IRepository<Tutor>, Repository<Tutor>>();
builder.Services.AddScoped<IRepository<Pet>, Repository<Pet>>();
builder.Services.AddScoped<IRepository<PlanoPet>, Repository<PlanoPet>>();
builder.Services.AddScoped<IRepository<Contrato>, Repository<Contrato>>();
builder.Services.AddScoped<IContratoRepository, ContratoRepository>();
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IPlanoPetService, PlanoPetService>();
builder.Services.AddScoped<IContratoService, ContratoService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();