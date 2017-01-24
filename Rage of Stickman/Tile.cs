using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rage_of_Stickman
{
	public enum ECollision
	{
		passable = 0,
		impassable = 1
	}

	class Tile : Entity
	{
		private ECollision collision_type = ECollision.impassable;

		public Tile(AnimatedTexture2D animatedTexture, ECollision collision_type, Vector2 startPosition, Vector2 size)
			: base(startPosition, size, EDirection.right, 1, 0, false, -1)
		{
			if (animatedTexture != null)
			{
				this.LoadAnimations(animatedTexture);
			}

			this.collision_type = collision_type;
		}

		public ECollision getCollisionType()
		{
			return this.collision_type;
		}

		public new void Update()
		{
			if (active)
			{
				this.Logic();
			}
			base.Update();
		}

		private void Logic()
		{
			// TODO Tile.Logic()
		}

		public new void Draw()
		{
			if (animations == null || animations.Length == 0)
			{
				base.Draw();
			}
			else
			{
				this.animations[0].Update();
				this.animations[0].Draw(this.position);
			}
		}
	}
}
