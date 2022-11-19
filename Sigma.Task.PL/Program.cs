using Microsoft.EntityFrameworkCore;
using Sigma.Task.BL;
using Sigma.Task.BL.CandidatesManager;
using Sigma.Task.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("CandidatesDb");
builder.Services.AddDbContext<CandidatesContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICandidatesRepository, EFCandidatesRepository>();
builder.Services.AddScoped<ICandidatesManager, CandidatesManager>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

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
