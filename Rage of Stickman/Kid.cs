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
		public Kid(Vector2 startPosition)
			: base(startPosition, new Vector2(1, 1), 1, 0.6f, 5)
		{
			this.LoadAnimations(Game.Content.animations[(int)EAnimation.enemie_kid_move]);
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
			// TODO Kid.Logic()
		}

		public new void Draw()
		{
			base.Draw();
		}
	}
}
