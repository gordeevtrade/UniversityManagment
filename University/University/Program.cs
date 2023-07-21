using Microsoft.EntityFrameworkCore;
using University.BuisnessLogic;
using University.BuisnessLogic.Interface;
using University.DAL;
using University.DAL.Repositories;
using University.DAL.Repositories.Interfaces;
using University.Mapping;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UniversityContext>(options =>
{
    options.UseSqlServer(connection);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IUnitOfWOrk, UnitOfWork>();
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IGroupService, GroupService>();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UniversityContext>();
    dbContext.Database.Migrate();
}



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Course/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Course}/{action=GetAllCourse}/{id?}");

app.Run();
