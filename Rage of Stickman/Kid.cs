using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class Kid : Enemy
	{
		public Kid(Vector2 startPosition) : base(EEnemy.kid, startPosition, 5, 0.6f)
		{
			width = Game.Content.tileSize;
			height = Game.Content.tileSize;
			localBounds = new Rectangle(0, 0, Game.Content.tileSize, Game.Content.tileSize * 2);
		}
	}
}
