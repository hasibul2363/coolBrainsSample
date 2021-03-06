﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using AutoMapper;
using CommonServiceLocator;
using DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuitSupply.Infrastructure.Bootstrapper;
using SuitSupply.Infrastructure.Bus;
using SuitSupply.Infrastructure.Bus.Command;
using SuitSupply.Infrastructure.Bus.Contracts;
using SuitSupply.Infrastructure.Bus.Query;
using SuitSupply.Infrastructure.Logger.Contracts;
using SuitSupply.Infrastructure.Logger.Serilog;
using SuitSupply.Infrastructure.Repository;
using SuitSupply.Infrastructure.Repository.Contracts;
using SuitSupply.Infrastructure.SLAdapter.MsDependency;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.ProductCatalog.CommandHandlers;
using SuitSupply.ProductCatalog.Commands;
using SuitSupply.ProductCatalog.DomainModels;
using SuitSupply.ProductCatalog.Queries;
using SuitSupply.ProductCatalog.QueryHandlers;
using SuitSupply.ProductCatalog.Validators;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddTransient<ISuitValidator<ProductQuery>, ProductQueryValidator>();
            services.AddTransient<SuitQueryHandler<List<Product>,ProductQuery>,ProductQueryHandler>();
            services.AddTransient<SuitQueryHandler<MemoryStream, ExcelExportQuery>, ExcelExportQueryHandler>();

            services.AddSingleton<ISuitLog, SuitLogUsingSerilog>();
            services.AddTransient<ISuitBus, SuitInmemoryBus>();
            services.AddTransient<ISuitValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddTransient<SuitCommandHandler<CreateProductCommand>, CreateProductCommandHandler>();

            services.AddTransient<ISuitValidator<DeleteProductCommand>, DeleteProductCommandValidator>();
            services.AddTransient<SuitCommandHandler<DeleteProductCommand>, DeleteProductCommandHandler>();

            services.AddTransient<ISuitValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
            services.AddTransient<SuitCommandHandler<UpdateProductCommand>, UpdateProductCommandHandler>();



            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IReadOnlyRepository, Repository>();

            var contextBuilder = new DbContextOptionsBuilder();
            contextBuilder.UseSqlServer(@"Password=Start777;Persist Security Info=True;User ID=sa;Initial Catalog=PC;Data Source=localhost\MSSQLSERVER_2017");
            services.AddSingleton(contextBuilder.Options);
            services.AddTransient<BaseContext,ProductCatalogDbContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        




            var adapter = new MsServiceLocatorAdapter(services);
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(()=> adapter);


            Mapper.Initialize(cfg => {
                cfg.CreateMap<CreateProductCommand, Product>();
                cfg.CreateMap<UpdateProductCommand, Product>();
            });
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();


            //app.UseSwagger();
            //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCatalogV1"); });


            //app.UseSwagger(p =>
            //{
            //    p.RouteTemplate = "swagger/{documentName}/help.json";
            //});
            //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/help.json", "ProductCatalogV1"); });


            SuitWebApiBootstrapper.Use(app);



        }
    }
}
