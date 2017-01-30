using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rage_of_Stickman
{
	class SceneRainSimulation : SceneComponent
	{
		// TODO RainSimulation : Make clear zone
		// TODO RainSimulation : Y-Position of rain zone (save distance to target)
		// TODO RainSimulation : position of Rain that middle raindrop hits target
		private List<Raindrop> raindrops;
		private int raindrops_max;
		private Timer spawn_next;
		private int density;
		private int max_falldepth;

		private Entity target;

		public SceneRainSimulation(int raindrops_max, int density, Vector2 position, Vector2 size, Entity target = null, bool active = true, bool visible = true)
			: base(null, position, size, active, visible)
		{
			raindrops = new List<Raindrop>();
			this.raindrops_max = raindrops_max;
			spawn_next = new Timer(0.05f);
			this.density = density;
			this.target = target;
			max_falldepth = 1000;
		}

		public override void Update(bool isPaused)
		{
			base.Update(false);

			if (target != null)
			{
				position.X = target.Position().X - size.X / 2;
			}

			for (int ID = raindrops.Count - 1; ID >= 0; ID--)
			{
				raindrops.ElementAt(ID).Update(false);
				if (raindrops.ElementAt(ID).isDead() || raindrops.ElementAt(ID).Position().Y > position.Y + max_falldepth)
				{
					raindrops.RemoveAt(ID);
				}
			}

			spawn_next.Update(false);
			if (spawn_next.IsTimeUp())
			{
				for (int ID = 0; ID < density; ID++)
				{
					if (raindrops.Count < raindrops_max)
					{
						raindrops.Add(new Raindrop(new Vector2(RandomGenerator.NextFloat(min: position.X, max: position.X + size.X), position.Y)));
					}
				}
				spawn_next.Reset();
			}
		}

		public override void Draw()
		{
			base.Draw();

			foreach(Raindrop raindrop in raindrops)
			{
				raindrop.Draw();
			}
		}
	}
}
