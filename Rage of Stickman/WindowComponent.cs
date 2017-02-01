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
		protected Vector2 position_start;

		protected int ID;
		protected bool markable;
		protected bool marked;

		public WindowComponent(bool markable, Vector2 position, Vector2 size, float rotation, bool active = true, bool visible = true)
			: base(position, size, rotation, Color.CornflowerBlue, active, visible)
		{
			position_start = position;
			ID = -1;
			this.markable = markable;
		}

		public Vector2 Position_Start()
		{
			return position_start;
		}

		public void SetID(int ID)
		{
			this.ID = ID;
		}

		public bool Markable()
		{
			return markable;
		}

		public virtual void Update(int index, bool isPaused)
		{
			base.Update(isPaused);

			if (isActive)
			{
				marked = (index == ID) ? (true) : (false);
			}
		}

		public override void Draw() {}
	}
}
