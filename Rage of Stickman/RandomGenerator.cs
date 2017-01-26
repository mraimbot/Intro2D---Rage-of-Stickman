using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class RandomGenerator
	{
		private static Random random;

		public static float NextFloat(int seed = 1337, float min = 0, float max = 1)
		{
			if (random == null)
			{
				random = new Random(seed);
			}

			return (float)random.NextDouble() * (max - min) + min;
		}

		public static int NextInt(int seed = 1337, int min = 0, int max = 1)
		{
			if (random == null)
			{
				random = new Random(seed);
			}
			return (int)random.Next() * (max - min) + min;
		}
	}
}
