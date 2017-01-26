using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rage_of_Stickman
{
	class RainSimulation : SceneComponent
	{
		private List<Raindrop> raindrops;
		private int raindrops_max;
		private Timer spawn_next;
		private int density;
		private int counter;

		private Entity target;

		public RainSimulation(int raindrops_max, int density, Vector2 position, Vector2 size, Entity target = null, bool active = true, bool visible = true)
			: base(null, position, size, active, visible)
		{
			raindrops = new List<Raindrop>();
			this.raindrops_max = raindrops_max;
			spawn_next = new Timer(0.2f);
			this.density = density;
			counter = this.density;
			this.target = target;
		}

		public override void Update()
		{
			base.Update();

			if (target != null)
			{
				position.X = target.Position().X - size.X / 2;
			}

			for (int ID = raindrops.Count - 1; ID >= 0; ID--)
			{
				raindrops.ElementAt(ID).Update();
				if (raindrops.ElementAt(ID).isDead())
				{
					raindrops.RemoveAt(ID);
				}
			}

			if (raindrops.Count < raindrops_max)
			{
				if (counter > 0)
				{
					raindrops.Add(new Raindrop(new Vector2(RandomGenerator.NextFloat(min: position.X, max: position.X + size.X), position.Y)));
					counter--;

					if (counter == 0)
					{
						spawn_next.Reset();
					}
				}
				else
				{
					spawn_next.Update();
					if (spawn_next.IsTimeUp())
					{
						counter = density;
					}
				}
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
