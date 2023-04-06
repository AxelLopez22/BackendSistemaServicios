using ApiServicios.Context;
using ApiServicios.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificationsOrigins = "_myAllowSpecificationsOrigins";
// Add services to the container.

builder.Services.AddDbContext<SistemaServiciosContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDb"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificationsOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddScoped<IServicesServices, ServicesServices>();
builder.Services.AddScoped<IClienteServices, ClienteServices>();
builder.Services.AddScoped<IClienteServicio, ClienteServicioServices>();
builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();

    var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificationsOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
