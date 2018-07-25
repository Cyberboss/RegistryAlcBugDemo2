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
		}
    }
}
