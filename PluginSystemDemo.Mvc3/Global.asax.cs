﻿using Autofac;
using Autofac.Integration.Mvc;
using Griffin.MvcContrib.VirtualPathProvider;
using PluginSystemDemo.Mvc3.Controllers;
using PluginSystemDemo.Mvc3.Infrastructure;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

namespace PluginSystemDemo.Mvc3 {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode,
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication {
		private PluginService _pluginServicee;
		private IContainer _container;

		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
				new[] { typeof(HomeController).Namespace }
			);
		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			_pluginServicee = new PluginService();
			RegisterContainer();
			HostingEnvironment.RegisterVirtualPathProvider(GriffinVirtualPathProvider.Current);
			_pluginServicee.Integrate(_container);
		}

		private void RegisterContainer() {
			var builder = new ContainerBuilder();
			builder.RegisterControllers(Assembly.GetExecutingAssembly());
			_pluginServicee.Startup(builder);
			_container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
		}
	}
}