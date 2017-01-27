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
		private Color color;
		private float angle;
		private Timer lifeTime;

		public Raindrop(Vector2 position_start)
			: base(position_start, new Vector2(1, 10), EDirection.right, 800, 1, true, 1)
		{
			Initialize();
			color = Color.LightSlateGray;
			size.Y += RandomGenerator.NextFloat(min: 1, max: 1);
			lifeTime = new Timer(0.3f);
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
			if (lifeTime.IsTimeUp())
			{
				health = 0;
			}

			if (isGrounded)
			{
				lifeTime.Update();
				size = Vector2.One;
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

			DrawPrimitive.Rectangle(position, color, (int)size.X, (int)size.Y, angle);
		}
	}
}
