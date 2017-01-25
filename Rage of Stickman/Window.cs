using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rage_of_Stickman
{
	class Window : SceneComponent
	{
		public Window(AnimatedTexture2D background, Vector2 position, Vector2 size, bool active = true, bool visible = true)
			: base(background, position, size, active, visible)
		{

		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw()
		{
			base.Draw();
		}
	}
}
