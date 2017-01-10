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

	class Tile
	{
		private AnimatedTexture2D animatedTexture2D;
		private ECollision collision_type;

		public Tile(AnimatedTexture2D animatedTexture2D, ECollision collision_type)
		{
			this.animatedTexture2D = animatedTexture2D;
			this.collision_type = collision_type;
		}

		public ECollision getCollisionType()
		{
			return collision_type;
		}

		public void Update()
		{
			animatedTexture2D.Update();
		}

		public void Draw(Vector2 position)
		{
			if (animatedTexture2D != null)
			{
				animatedTexture2D.Draw(position);
			}
		}
	}
}
