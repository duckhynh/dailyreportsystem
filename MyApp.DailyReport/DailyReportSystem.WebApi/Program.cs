using DailyReportSystem.Application.Services;
using DailyReportSystem.Core.Interfaces;
using DailyReportSystem.Infrastructure;
using DailyReportSystem.Infrastructure.Repositories;
using DailyReportSystem.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure SQL Server Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Configure JWT Authentication
var jwtKey = builder.Configuration["JwtSettings:SecretKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey!)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Audience"]
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----- THÊM RECORD MẪU (SEED DATA) ĐỂ CÓ THỂ TEST -----
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    if (!context.Users.Any())
    {
        // Nhường quyền tự sinh ID cho CSDL (tránh lỗi IDENTITY_INSERT)
        var testAdmin = new DailyReportSystem.Core.Entities.User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345"), FullName = "Nguyễn Văn Admin", Role = DailyReportSystem.Core.Enums.RoleType.Admin };
        var testManager = new DailyReportSystem.Core.Entities.User { Username = "manager", PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345"), FullName = "Trần Văn Manager", Role = DailyReportSystem.Core.Enums.RoleType.Manager };
        var testEmployee = new DailyReportSystem.Core.Entities.User { Username = "testuser", PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345"), FullName = "Nguyễn Văn Test", Role = DailyReportSystem.Core.Enums.RoleType.Employee };
        
        var testProject = new DailyReportSystem.Core.Entities.Project { ProjectName = "Dự án Demo SWD392", Description = "Project Template" };
        
        context.Users.AddRange(testAdmin, testManager, testEmployee);
        context.Projects.Add(testProject);
        context.SaveChanges(); // Lấy ID vừa được tạo

        var testTask = new DailyReportSystem.Core.Entities.ProjectTask { ProjectId = testProject.Id, TaskName = "Thiết kế CSDL", Description = "Lên cấu trúc bảng cho dự án", Status = "Open" };
        context.ProjectTasks.Add(testTask);

        context.UserProjects.Add(new DailyReportSystem.Core.Entities.UserProject { UserId = testEmployee.Id, ProjectId = testProject.Id, AssignedDate = System.DateTime.UtcNow });
        context.SaveChanges();
    }
}
// --------------------------------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

// Use Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
