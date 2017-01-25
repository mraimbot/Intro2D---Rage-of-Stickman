using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rage_of_Stickman
{
	class WindowComponent : GameObject
	{
		protected bool markable;

		public WindowComponent(bool markable, Vector2 position, Vector2 size, bool active = true, bool visible = true)
			: base(position, size, active, visible)
		{
			this.markable = markable;
		}

		public bool Markable()
		{
			return markable;
		}

		public override void Update()
		{
			// TODO SceneComponent.Update()
			base.Update();
		}

		public override void Draw()
		{
			base.Draw();
		}
	}
}
