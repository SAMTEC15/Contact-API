using ContactAppAPI.Application.Implementation.Interface;
using ContactAppAPI.Application.Implementation.Services;
using ContactAppAPI.Domain.Model;
using ContactAppAPI.Persistence.DataContext;
using ContactAppAPI.Persistence.Repository.Interface;
using ContactAppAPI.Persistence.Repository.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContactUserDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));


//Add swagger gen
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Authorization", Version = "v1" });
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "Jwt",
        Scheme = "Bearer"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }

                });

});

builder.Services.AddIdentity<ContactUser, IdentityRole>()
               .AddEntityFrameworkStores<ContactUserDbContext>()
               .AddDefaultTokenProviders();


//Injecting dependencies

builder.Services.AddScoped<IRegisterServices, RegisterServices>();
builder.Services.AddScoped<IContactDtoMapper, ContactDtoMapper>();
builder.Services.AddScoped<ILoginServices, LoginServices>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactServices, ContactServices>();
builder.Services.AddScoped<ICloudRep,  CloudRep>();

// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})


// Adding Jwt Bearer  
            .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     // ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
                     // ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
                     //  ClockSkew = TimeSpan.Zero,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
                 };
             });

// Add the ServiceProvider to the container
builder.Services.AddSingleton<IServiceProvider>(provider => provider.CreateScope().ServiceProvider);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    Initializer.SeedRoles(serviceProvider);
}

app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
