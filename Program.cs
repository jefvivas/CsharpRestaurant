using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Restaurant.Models;
using Restaurant.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.Configure<MongoDBSettings>(configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoCollection<Product>>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;

    var mongoClient = new MongoClient(settings.ConnectionString);
    var database = mongoClient.GetDatabase(settings.DatabaseName);
    return database.GetCollection<Product>("Products");
});

builder.Services.AddSingleton<IMongoCollection<Table>>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;

    var mongoClient = new MongoClient(settings.ConnectionString);
    var database = mongoClient.GetDatabase(settings.DatabaseName);
    return database.GetCollection<Table>("Tables");
});

builder.Services.AddSingleton<IMongoCollection<Admin>>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;

    var mongoClient = new MongoClient(settings.ConnectionString);
    var database = mongoClient.GetDatabase(settings.DatabaseName);
    return database.GetCollection<Admin>("Admin");
});

builder.Services.AddTransient<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "localhostUser",
        ValidAudience = "localhostUser",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("08D856F45E32C98D0AA162BBD99E99D5"))
    };
})

.AddJwtBearer("adminJWT", options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = false,
         ValidateAudience = false,
         ValidateIssuerSigningKey = true,
         ValidIssuer = "localhostAdmin",
         ValidAudience = "localhostAdmin",
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3A9F041FD4B9E0C12D0B8F008F5E1B76D8DCA1CEBB36E5E586A81D5B936F276"))
     };
 });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


builder.Services.AddScoped<ProductServices>();
builder.Services.AddScoped<TableServices>();
builder.Services.AddScoped<JwtServices>();
builder.Services.AddScoped<HashServices>();
builder.Services.AddScoped<AdminServices>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactFrontend");

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();


app.MapControllers();


app.Run();
