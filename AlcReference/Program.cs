using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace AlcReference
{
    sealed class Program : AssemblyLoadContext
    {
		static readonly string basePath = Path.GetFullPath("lib");

		static void Main(string[] args) => new Program().Run();

		Program()
		{
			Resolving += Program_Resolving;
		}

		Assembly Program_Resolving(AssemblyLoadContext context, AssemblyName assemblyName)
		{
			if (assemblyName.Name.EndsWith("resources", StringComparison.Ordinal))
				return null;

			var foundDll = Directory.GetFileSystemEntries(basePath, assemblyName.Name + ".dll", SearchOption.AllDirectories).FirstOrDefault();
			if (foundDll != default)
				return context.LoadFromAssemblyPath(foundDll);
			return context.LoadFromAssemblyName(assemblyName);
		}

		protected override Assembly Load(AssemblyName assemblyName) => null;

		void Run()
		{
			try
			{
				var path = Path.Combine(basePath, "BaseClass.dll");
				if(!File.Exists(path))
				{
					Console.WriteLine("Couldn't find BaseClass.dll! Did you forget to dotnet publish it to " + basePath + "?");
					return;
				}
				var assembly = LoadFromAssemblyPath(path);
				var type = assembly.GetType("BaseClass.BaseClass");
				var instance = Activator.CreateInstance(type);
				type.GetMethod("Test").Invoke(instance, Array.Empty<object>());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
    }
}
