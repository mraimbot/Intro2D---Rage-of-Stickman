using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class GameObject
	{
		protected Vector2 position;
		protected Vector2 size;

		protected bool active;
		protected bool visible;

		public GameObject()
		{
			this.position = new Vector2(0, 0);
			this.size = new Vector2(1, 1);
			this.active = true;
			this.visible = true;
		}

		public Vector2 Position()
		{
			return this.position;
		}

		public Vector2 Size()
		{
			return this.size;
		}

		public void toggleActive()
		{
			this.active = !this.active;
		}

		public bool Active()
		{
			return this.active;
		}

		public void Active(bool active)
		{
			this.active = active;
		}

		public void toggleVisible()
		{
			this.visible = !this.visible;
		}

		public bool Visible()
		{
			return this.visible;
		}

		public void Visible(bool visible)
		{
			this.visible = visible;
		}

		public void Update()
		{
			// TODO GameObject.Update()
		}
	}
}
