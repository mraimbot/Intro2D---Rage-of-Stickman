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
		public Enemy(Vector2 startPosition, Vector2 size, float mass, float speed, int health)
			: base(startPosition, size, EDirection.left, mass, speed, true, health)
		{
			Initialize();
		}

		public void Initialize()
		{
			this.position = this.position_start;
			this.health = this.health_start;
		}

		public new void Update()
		{
			if (this.active)
			{
				Logic();
			}

			base.Update();
		}

		private void Logic()
		{
			if (!this.isDead())
			{
				if (Game.Content.player.Position().X + 0.1 < this.position.X)
				{
					impulses.Add(new Vector2(-speed, 0.0f));
					lookAtDirection = EDirection.left;
				}
				else if (Game.Content.player.Position().X - 0.1 > this.position.X)
				{
					impulses.Add(new Vector2(speed, 0.0f));
					lookAtDirection = EDirection.right;
				}
			}
		}

		public new void Draw()
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
		}
	}
}
