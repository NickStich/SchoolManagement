using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolManagement.BusinessLogic.Interfaces;
using SchoolManagement.BusinessLogic.Services;
using SchoolManagement.Common.Entity;
using SchoolManagement.DAL;
using SchoolManagement.DAL.Interfaces;
using SchoolManagement.DAL.Repositories;
using SchoolManagement.Helpers;
using SchoolManagement.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SchoolDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddScoped<ICrudRepository<Student>, StudentRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<TeacherRepository>();
builder.Services.AddScoped<SubjectRepository>();
builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<BlobStorageHelper>();

builder.Services.ConfigureOptions<BlobSettings>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.EnableAnnotations();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    //app.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller}/{action=Index}/{id?}");
});


//app.MapFallbackToFile("index.html"); ;

app.Run();
