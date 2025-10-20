using Autofac;
using Autofac.Integration.Mvc;
using CloudinaryDotNet;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyEcommerce.Application.Interfaces;
using MyEcommerce.Domain.Interfaces;
using MyEcommerce.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MyEcommerce.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterContainer()
        {
            
            var builder = new ContainerBuilder();

            builder.RegisterType<AppDbContext>()
                .As<IDbContext>()
                .SingleInstance();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var assemblies = new Assembly[] { typeof(MvcApplication).Assembly };

            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
               .AsClosedTypesOf(typeof(INotificationHandler<>))
               .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            //cloudinary config
            builder.Register(c =>
            {
                var cloudName = System.Configuration.ConfigurationManager.AppSettings["CloudinaryCloudName"];
                var apiKey = System.Configuration.ConfigurationManager.AppSettings["CloudinaryApiKey"];
                var apiSecret = System.Configuration.ConfigurationManager.AppSettings["CloudinaryApiSecret"];
                return new Account(cloudName, apiKey, apiSecret);
            }).SingleInstance();

            builder.RegisterType<CloudinaryService>()
                .As<ICloudinaryService>()
                .SingleInstance();
               
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
    public class AutofacServiceProvider : IServiceProvider
    {
        private readonly IComponentContext _context;
        public AutofacServiceProvider(IComponentContext context)
        {
            _context = context;
        }

        public object GetService(Type serviceType)
        {
            return _context.ResolveOptional(serviceType);
        }
    }
}