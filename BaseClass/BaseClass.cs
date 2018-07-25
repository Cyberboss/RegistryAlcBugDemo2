using System;
using Microsoft.Win32;

namespace BaseClass
{
    sealed public class BaseClass
    {
		public void Test()
		{
			if (Registry.LocalMachine == null)
				Console.WriteLine("LocalMachine is null!");
			else
				Console.WriteLine("LocalMachine is not null!");

			try
			{
				var tryAgain = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
				if(tryAgain != null)
					Console.WriteLine("Successfully opened registry key manually!");
				else
					Console.WriteLine("Failed to open registry key manually!");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
