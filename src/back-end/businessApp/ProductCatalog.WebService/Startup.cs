using System;
using AutoMapper;
using CommonServiceLocator;
using DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuitSupply.Infrastructure.Bus;
using SuitSupply.Infrastructure.Bus.Command;
using SuitSupply.Infrastructure.Bus.Contracts;
using SuitSupply.Infrastructure.Logger.Contracts;
using SuitSupply.Infrastructure.Logger.Serilog;
using SuitSupply.Infrastructure.Repository;
using SuitSupply.Infrastructure.Repository.Contracts;
using SuitSupply.Infrastructure.SLAdapter.MsDependency;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.ProductCatalog.CommandHandlers;
using SuitSupply.ProductCatalog.Commands;
using SuitSupply.ProductCatalog.DomainModels;
using SuitSupply.ProductCatalog.Validators;

namespace SuitSupply.ProductCatalog.WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            SeriLogConfiguration.Configure();
            services.AddSingleton<ISuitLog, SuitLogUsingSerilog>();
            services.AddTransient<ISuitBus, SuitInmemoryBus>();
            services.AddTransient<ISuitValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddTransient<SuitCommandHandler<CreateProductCommand>, CreateProductCommandHandler>();
            services.AddTransient<IRepository, Repository>();

            var contextBuilder = new DbContextOptionsBuilder();
            contextBuilder.UseSqlServer(@"Password=Start777;Persist Security Info=True;User ID=sa;Initial Catalog=PC;Data Source=localhost\MSSQLSERVER_2017");
            services.AddSingleton(contextBuilder.Options);
            services.AddTransient<BaseContext,ProductCatalogDbContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var adapter = new MsServiceLocatorAdapter(services);
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(()=> adapter);


            Mapper.Initialize(cfg => {
                cfg.CreateMap<CreateProductCommand, Product>();
            });
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            app.UseMvc();



        }
    }
}
