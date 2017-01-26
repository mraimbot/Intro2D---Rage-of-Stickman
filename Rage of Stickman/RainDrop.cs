using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rage_of_Stickman
{
	class Raindrop : Entity
	{
		private float angle;

		public Raindrop(Vector2 position_start)
			: base(position_start, new Vector2(1, 10), EDirection.right, 800, 1, true, 1)
		{
			Initialize();
			size.Y += RandomGenerator.NextFloat(min: 1, max: 1);
		}

		public override void Update()
		{
			base.Update();
			if (active)
			{
				Logic();
			}
		}

		private void Logic()
		{
			if (isGrounded)
			{
				health = 0;
			}

			if (!isDead())
			{
				angle = (float)Math.Acos(Vector2.Dot(Vector2.UnitY, Vector2.Normalize(Game.Content.force_wind + Game.Content.force_gravity)));
				if (Game.Content.force_wind.X > 0)
				{
					angle = 0 - angle;
				}
			}
		}

		public override void Draw()
		{
			base.Draw();

			DrawPrimitive.Line(position, Color.LightSlateGray, (int)size.X, (int)size.Y, angle);
		}
	}
}
