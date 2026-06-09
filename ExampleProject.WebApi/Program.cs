using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.WebApi;
using ExampleProject.Repository;
using ExampleProject.Repository.Common;
using ExampleProject.Service;
using ExampleProject.Service.Common;
using ExampleProjest.Service.Common;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        //containerBuilder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
        //containerBuilder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
        //containerBuilder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
        //containerBuilder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();

        containerBuilder.RegisterAssemblyTypes(Assembly.Load("ExampleProject.Service"))
        .As(type => type.GetInterfaces().FirstOrDefault(iface => iface.Name == "I" + type.Name)).InstancePerLifetimeScope();
        containerBuilder.RegisterAssemblyTypes(Assembly.Load("ExampleProject.Repository"))
        .As(type => type.GetInterfaces().FirstOrDefault(iface => iface.Name == "I" + type.Name)).InstancePerLifetimeScope();
    });

var app = builder.Build();

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
