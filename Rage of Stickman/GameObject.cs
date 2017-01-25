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

		public GameObject(Vector2 position, Vector2 size, bool active = true, bool visible = true)
		{
			this.position = position;
			this.size = size;
			this.active = active;
			this.visible = visible;
		}

		public Vector2 Position()
		{
			return position;
		}

		public void MoveTo(Vector2 position)
		{
			this.position = position;
		}

		public Vector2 Size()
		{
			return size;
		}

		public void SizeTo(Vector2 size)
		{
			this.size = size;
		}

		public void toggleActive()
		{
			active = !active;
		}

		public bool Active()
		{
			return active;
		}

		public void Active(bool active)
		{
			this.active = active;
		}

		public void toggleVisible()
		{
			visible = !visible;
		}

		public bool Visible()
		{
			return visible;
		}

		public void Visible(bool visible)
		{
			this.visible = visible;
		}

		public virtual void Update()
		{
			// TODO GameObject.Update()
		}

		public virtual void Draw()
		{
			// TODO GameObject.Draw()
		}
	}
}
