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

		public Tile(AnimatedTexture2D animatedTexture, ECollision collision_type, Vector2 position, Vector2 size)
			: base(position, size, 0, isImmortal: true)
		{
			if (animatedTexture != null)
			{
				LoadAnimations(animatedTexture, false);
			}

			this.collision_type = collision_type;
		}

		public ECollision getCollisionType()
		{
			return this.collision_type;
		}

		public override void Update(bool isPaused)
		{
			if (isActive)
			{
				if (!isPaused)
				{
					Logic();
				}
			}

			base.Update(isPaused);
		}

		private void Logic()
		{
			// TODO Tile.Logic()
		}

		public new void Draw()
		{
			if (isVisible)
			{
				if (animation != null)
				{
					animation.Update();
					animation.Draw(position, rotation: rotation);
				}
			}
		}
	}
}
