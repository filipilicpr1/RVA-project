using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.DataInitializers;
using Server.Infrastructure;
using Server.Interfaces.DataInitializerInterfaces;
using Server.Interfaces.RepositoryInterfaces;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.TokenMakerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Interfaces.ValidationInterfaces;
using Server.Mapping;
using Server.Models;
using Server.Repositories;
using Server.Services;
using Server.TokenMakers;
using Server.UnitOfWork;
using Server.Validations;
using System.Text;

string _cors = "cors";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "rva-projekat", Version = "v1" });
    //Ovo dodajemo kako bi mogli da unesemo token u swagger prilikom testiranja
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SystemUser", policy => policy.RequireClaim("Sys_user")); //Ovde mozemo kreirati pravilo za validaciju nekog naseg claima
});

//Dodajemo semu autentifikacije i podesavamo da se radi o JWT beareru
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
   var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"]));
   options.TokenValidationParameters = new TokenValidationParameters //Podesavamo parametre za validaciju pristiglih tokena
   {
       ValidateIssuer = true, //Validira izdavaoca tokena
       ValidateAudience = false, //Kazemo da ne validira primaoce tokena
       ValidateLifetime = true,//Validira trajanje tokena
       ValidateIssuerSigningKey = true, //validira potpis token, ovo je jako vazno!
       ValidIssuer = "http://localhost:44386", //odredjujemo koji server je validni izdavalac
       IssuerSigningKey = key//navodimo privatni kljuc kojim su potpisani nasi tokeni
   };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _cors, builder => {
        builder.WithOrigins("http://localhost:3000")//Ovde navodimo koje sve aplikacije smeju kontaktirati nasu,u ovom slucaju nas Angular front
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});


//services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IBusLineService, BusLineService>();
builder.Services.AddScoped<ICityService, CityService>();

//validations
builder.Services.AddScoped<IValidation<User>, UserValidation>();
builder.Services.AddScoped<IValidation<BusLine>, BusLineValidation>();
builder.Services.AddScoped<IValidation<City>, CityValidation>();

//data initializers
builder.Services.AddScoped<IUserDataInitializer, UserDataInitializer>();
builder.Services.AddScoped<ICountryDataInitializer, CountryDataInitializer>();
builder.Services.AddScoped<IManufacturerDataInitializer, ManufacturerDataInitializer>();
builder.Services.AddScoped<ICityDataInitializer, CityDataInitializer>();
builder.Services.AddScoped<IBusLineDataInitializer, BusLineDataInitializer>();
builder.Services.AddScoped<IBusDataInitializer, BusDataInitializer>();
builder.Services.AddScoped<IDataInitializer, DataInitializer>();

//factories
builder.Services.AddScoped<ITokenMakerFactory, TokenMakerFactory>();

//registracija za svaki Repository, UnitOfWork i DbContext, sve je scoped(ista instanca u okviru jednog HTTP zahteva)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IBusLineRepository, BusLineRepository>();
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<BusLineDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BusLineDatabase")));

//Registracija mapera u kontejneru, zivotni vek singleton
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserMappingProfile());
    mc.AddProfile(new CountryMappingProfile());
    mc.AddProfile(new ManufacturerMappingProfile());
    mc.AddProfile(new BusLineMappingProfile());
    mc.AddProfile(new BusMappingProfile());
    mc.AddProfile(new CityMappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    // koristi se za inicijalizaciju podataka
    scope.ServiceProvider.GetRequiredService<IDataInitializer>().InitializeData();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "rva-projekat v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(_cors);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();