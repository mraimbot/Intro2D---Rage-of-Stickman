using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class Zombie : Enemy
	{
		public Zombie(Vector2 startPosition) : base(EEnemy.zombie, startPosition, 100, 0.5f)
		{
			width = Game.Content.tileSize;
			height = Game.Content.tileSize * 2;
			localBounds = new Rectangle(0, 0, Game.Content.tileSize, Game.Content.tileSize * 2);
		}
	}
}
