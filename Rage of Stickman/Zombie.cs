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
		public Zombie(Vector2 startPosition)
			: base(startPosition, new Vector2(1, 1), 2, 0.3f, 20)
		{
			this.LoadAnimations(Game.Content.animations[(int)EAnimation.enemie_zombie_move]);
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
			// TODO Zombie.Logic()
		}

		public new void Draw()
		{
			base.Draw();
		}
	}
}
