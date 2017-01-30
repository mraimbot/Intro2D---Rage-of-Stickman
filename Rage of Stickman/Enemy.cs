using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class Enemy : Entity
	{
		protected Timer claim_timer;
		protected bool isClaiming;
		protected List<string> claims;
		protected int claim_ID;
		protected Color claim_color;
		protected Entity target;
		protected float range; // The Enemy is only moving to his target, if the target is in this range

		public Enemy(Entity target, Vector2 startPosition, Vector2 size, float mass, float speed, int health)
			: base(startPosition, size, EDirection.left, mass, speed, true, health)
		{
			Initialize();
			claim_timer = new Timer(RandomGenerator.NextInt(min: 3, max: 20));
			claims = new List<string>();
			isClaiming = false;
			claim_color = Color.White;
			useWind = false;
			this.target = target;
		}

		public override void addDamage(Entity attacker, Vector2 attack_force, int damage)
		{
			base.addDamage(attacker, attack_force, damage);
		}

		public override void Update(bool isPaused)
		{
			if (active)
			{
				if (!isPaused)
				{
					Logic(isPaused);
				}
			}

			base.Update(isPaused);
		}

		private void Logic(bool isPaused)
		{
			claim_timer.Update(isPaused);

			if (claim_timer.IsTimeUp())
			{
				isClaiming = !isClaiming;
				if (isClaiming)
				{
					claim_ID = RandomGenerator.NextInt(min: 0, max: claims.Count);
					claim_timer.Reset(claims.ElementAt(claim_ID).Length / 2);
				}
				else
				{
					claim_timer.Reset(RandomGenerator.NextInt(min: 3, max: 20));
				}
			}
		}

		public override void Draw()
		{
			SpriteEffects s = SpriteEffects.None;

			if (lookAtDirection == EDirection.left)
			{
				s = SpriteEffects.FlipHorizontally;
			}
			else
			{
				s = SpriteEffects.None;
			}

			if (animations == null || animations.Length == 0)
			{
				base.Draw();
			}
			else
			{
				this.animations[0].Update();
				this.animations[0].Draw(position, s);
			}

			if (isClaiming && claims.Count > 0)
			{
				ShowText.Text(new Vector2(position.X + size.X / 2, position.Y - 32), claims.ElementAt(claim_ID), claim_color, 0, 1, ETextAlign.Center);
			}
		}
	}
}
