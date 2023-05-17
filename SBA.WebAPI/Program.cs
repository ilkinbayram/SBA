using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using SBA.Business.DependencyResolvers.Autofac;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });


string sqlLocalDb = builder.Configuration.GetConnectionString("LocalDB");
string sqlPromoDb = builder.Configuration.GetConnectionString("SbaPromoDB");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(sqlLocalDb);
});

builder.Services.AddDbContext<ExternalAppDbContext>(options =>
{
    options.UseSqlServer(sqlPromoDb);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//                           /\
//________________________  //\\
//     UP IS Services    |   ||
//------------------------   ||
//                           --


//                           --
//________________________   ||
//  Down IS MiddleWares  |   ||
//------------------------  \\//
//                           \/



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
