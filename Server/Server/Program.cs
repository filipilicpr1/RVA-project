using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.Mapping;
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
    //options.AddPolicy("SamoOdabrani", policy => policy.RequireClaim("Neki_moj_claim")); //Ovde mozemo kreirati pravilo za validaciju nekog naseg claima
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


//builder.Services.AddScoped<IStudentService, StudentService>();
//builder.Services.AddScoped<IDataInitializer, DataInitializer>();
//services.AddScoped<IStudentService, StudentService>();

//registracija db contexta u kontejneru zavisnosti, njegov zivotni vek je Scoped
//builder.Services.AddDbContext<TestDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestDatabase")));

//Registracija mapera u kontejneru, zivotni vek singleton
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//builder.Services.BuildServiceProvider().GetRequiredService<IDataInitializer>().InitializeData();
//var myService = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<IDataInitializer>().InitializeData();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    // koristi se za inicijalizaciju podataka
    //scope.ServiceProvider.GetRequiredService<IDataInitializer>().InitializeData();
}
//app.Services.GetService<IDataInitializer>().InitializeData();
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