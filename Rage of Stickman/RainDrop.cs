﻿using System;
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
		private Timer lifeTime;

		public Raindrop(Vector2 position_start)
			: base(position_start, new Vector2(1, 7), 0, 1, 800, false, true, true, false, true, true)
		{
			color = Color.LightSlateGray;
			size.Y += RandomGenerator.NextFloat(min: -3, max: 3);
			lifeTime = new Timer(0.5f);
		}

		public override void Update(bool isPaused)
		{
			base.Update(false);

			if (isActive)
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
				lifeTime.Update(false);
				size = Vector2.One;
			}

			if (!isDead())
			{
				angle = (float)Math.Acos(Vector2.Dot(Vector2.UnitY, Vector2.Normalize(Game.Content.force_wind + Game.Content.force_gravity)));
				if (Game.Content.force_wind.X > 0)
				{
					angle *= -1;
				}
			}
		}

		public override void Draw()
		{
			DrawPrimitive.Rectangle(position, color, (int)size.X, (int)size.Y,angle: angle);
		}
	}
}
