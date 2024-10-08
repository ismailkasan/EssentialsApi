using AutoMapper;
using System.Text;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Essentials.Data;
using Essentials.Domain;
using Scrutor;
using Essentials.Application;
using System.Net.Http.Headers;
using Polly;
using Polly.Extensions.Http;
using Essentials.Api.Middlewares;
using Essentials.Api;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();

// Add JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtBearerOptions =>
{
    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Issuer"],
        ValidAudience = builder.Configuration["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SigningKey"]))
    };
});

// Add DbContext
builder.Services.AddDbContext<EssentialsDbContext>();

// Add Validators
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();

// Add Redis
builder.Services.AddDistributedRedisCache(option =>
{
    option.Configuration = "localhost:6379";
});

// Add HttpClientFactory
builder.Services.AddHttpClient("jsonPlaceHolder", client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
    client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.Timeout = TimeSpan.FromSeconds(60);
})
.SetHandlerLifetime(TimeSpan.FromMinutes(5)) // Lifetime config
.AddPolicyHandler(GetRetryPolicy());  // Retry config


// Add services dependencies by Scrutor...
var aa = builder.Services.Scan(selector => selector
.FromAssemblies(typeof(ApplicationEmptyClass).Assembly, // The Assembly that contains this type
                typeof(DataEmptyClass).Assembly)
.AddClasses(publicOnly: false)
.UsingRegistrationStrategy(RegistrationStrategy.Skip)
.AsMatchingInterface()
.WithScopedLifetime());

//Add response cache service
builder.Services.AddResponseCaching(options =>
{
    options.UseCaseSensitivePaths = true;
    options.MaximumBodySize = 1024;
});

// Add Swagger config
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Essentials.Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
});


// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add AutoMapper Config
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AllowNullCollections = true;
    mc.AddProfile(new MapperProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>(); // Add Exception Middleware

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Essentials.Api v1"));

app.UseStaticFiles();
app.UseAuthentication();

app.UseResponseCaching();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();


static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                    retryAttempt)));
}