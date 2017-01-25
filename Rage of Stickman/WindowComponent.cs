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
		protected int ID;
		protected bool markable;
		protected bool marked;

		public WindowComponent(bool markable, Vector2 position, Vector2 size, bool active = true, bool visible = true)
			: base(position, size, active, visible)
		{
			ID = -1;
			this.markable = markable;
		}

		public void SetID(int ID)
		{
			this.ID = ID;
		}

		public bool Markable()
		{
			return markable;
		}

		public virtual void Update(int index)
		{
			base.Update();
			if (active)
			{
				marked = (index == ID) ? (true) : (false);
			}
		}

		public override void Draw()
		{
			base.Draw();

			//if (visible)
			//{

			//}
		}
	}
}
