using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class Oma : Enemy
	{
		public Oma(Vector2 startPosition) : base(EEnemy.oma, startPosition, 100, 0.1f)
		{
			width = Game.Content.tileSize;
			height = Game.Content.tileSize * 2;
			localBounds = new Rectangle(0, 0, Game.Content.tileSize, Game.Content.tileSize * 2);
		}
	}
}
