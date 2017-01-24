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
		public Oma(Vector2 startPosition)
			: base(startPosition, new Vector2(1, 1), 2, 0.2f, 500)
		{
			this.LoadAnimations(Game.Content.animations[(int)EAnimation.enemie_oma_move]);
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
			// TODO Oma.Logic()
		}

		public new void Draw()
		{
			base.Draw();
		}
	}
}
