using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace ServicesWithAttributes
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddAutoRegisterServices (this IServiceCollection services)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies ();
			foreach (var assembly in assemblies)
			{
				var typesWithAttributes = assembly.DefinedTypes
					.Where (type => type.GetCustomAttributes (typeof (ServiceAttribute), true).Any ())
					.ToList ();

				foreach (var type in typesWithAttributes)
				{
					var attributeList = type.GetCustomAttributes<ServiceAttribute> ();

					foreach (var attribute in attributeList)
					{
						var lifetime = attribute.Lifetime;

						Type baseType = type.AsType ();

						Type serviceType = attribute.ServiceInterface;
						if (serviceType == null) serviceType = baseType;

						switch (lifetime)
						{
							case ServiceLifetime.Singleton:
								services.AddSingleton (serviceType, baseType);
								break;
							case ServiceLifetime.Scoped:
								services.AddScoped (serviceType, baseType);
								break;
							case ServiceLifetime.Transient:
								services.AddTransient (serviceType, baseType);
								break;
						}
					}
				}
			}

			return services;
		}
	}
}

