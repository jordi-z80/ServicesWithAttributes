using System;
using Microsoft.Extensions.DependencyInjection;

//=============================================================================
/// <summary></summary>
[AttributeUsage (AttributeTargets.Class, Inherited = false)]
public class ServiceAttribute : Attribute
{
	public ServiceLifetime Lifetime { get; }
	public Type? ServiceInterface { get; }

	public ServiceAttribute (ServiceLifetime lifetime)
	{
		Lifetime = lifetime;
	}

	public ServiceAttribute (ServiceLifetime lifetime, Type interfaceType)
	{
		Lifetime = lifetime;
		ServiceInterface = interfaceType;
	}
}

