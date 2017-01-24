/*
 * File: Program.cs
 * Description: Main-function where the application starts to work. 
 */

using System;

namespace Rage_of_Stickman
{
	#if WINDOWS || LINUX
		public static class Program
		{
			[STAThread]
			static void Main()
			{
				using (var game = new Main())
				{
					game.Run();
				}
			}
		}
	#endif
}
