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

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.ConfigureCustomExceptionMiddleware();

app.UseCors(options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
