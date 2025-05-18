using Common.Dto;
using Repository.Interfaces;
using Repository.Entities;
using Repository.Repositories;
using Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// רישום של DbContext והקשר למסד הנתונים
builder.Services.AddDbContext<DataBase>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IContext, DataBase>();

// רישום AutoMapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MyMapper));

// רישום ריפוזיטוריז
builder.Services.AddScoped<IRepository<Analyst>, AnalystRepository>();
builder.Services.AddScoped<IRepository<Child>, ChildRepository>();
builder.Services.AddScoped<IRepository<FormCategory>, FormCategoryRepository>();
builder.Services.AddScoped<IRepository<Comment>, CommentRepository>();
builder.Services.AddScoped<IRepository<LessonSummary>, LessonSummaryRepository>();

// רישום שירותים
builder.Services.AddScoped<IService<AnalystDto>, AnalystService>();
builder.Services.AddScoped<IService<ChildDto>, ChildService>();
builder.Services.AddScoped<FormCategoryService>();
builder.Services.AddScoped<IService<CommentDto>, CommentService>();
builder.Services.AddScoped<IService<LessonSummaryDto>, LessonSummaryService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


