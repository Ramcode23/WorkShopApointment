using AppoimentsWorkShop.DataAccess;
using AppoimentsWorkShop.Helpers;
using AppoimentsWorkShop.Models;
using AppoimentsWorkShop.Services;
using AppoimentsWorkShop.Utitlities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderCtx, loggerCof) =>
{
    loggerCof.
    WriteTo.Console()
    .WriteTo.Debug()
    .ReadFrom.Configuration(hostBuilderCtx.Configuration);

});


// Add services to the container.
const string CONNECTIONNAME = "DefaultConnection";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<DataSeed>();

builder.Services.AddJwtTokenServices(builder.Configuration);
builder.Services.AddScoped<IAppointmentsService, AppointmentsService>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("role", "Admin"));
});


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {

        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme",


    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
             new OpenApiSecurityScheme
             {
                 Reference= new OpenApiReference
                 {
                     Type=ReferenceType.SecurityScheme,
                     Id="Bearer"
                 }
             },
              new string[]{}

        }


    });

});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();

    });

});


builder.Services.AddIdentity<User, IdentityRole>()
                             .AddEntityFrameworkStores<DataContext>()
                             .AddRoles<IdentityRole>()
                             .AddDefaultTokenProviders();




var app = builder.Build();

// SeedDb
SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeed>();
        service.SeedAsync().Wait();
    }
}
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
