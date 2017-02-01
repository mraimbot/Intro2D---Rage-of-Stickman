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
		protected float rotation;

		protected Color color;

		protected bool isActive;
		protected bool isVisible;
		private Func<Color> color1;

		public GameObject(Vector2 position, Vector2 size, float rotation, Color color, bool isActive = true, bool isVisible = true)
		{
			this.position = position;
			this.size = size;
			this.rotation = rotation;
			this.isActive = isActive;
			this.isVisible = isVisible;
			this.color = color;
		}

		public GameObject(Vector2 position_start, Vector2 size, Func<Color> color1)
		{
			this.size = size;
			this.color1 = color1;
		}

		public Vector2 Position()
		{
			return position;
		}

		public virtual void MoveTo(Vector2 position)
		{
			this.position = position;
		}

		public Vector2 Size()
		{
			return size;
		}

		public void Size(Vector2 size)
		{
			this.size = size;
		}

		public void Rotation(float rotation)
		{
			this.rotation = rotation;
		}

		public float Rotation()
		{
			return rotation;
		}

		public Color getColor()
		{
			return color;
		}

		public void setColor(Color color)
		{
			this.color = color;
		}

		public void toggleActive()
		{
			isActive = !isActive;
		}

		public bool Active()
		{
			return isActive;
		}

		public void Active(bool active)
		{
			this.isActive = active;
		}

		public void toggleVisible()
		{
			isVisible = !isVisible;
		}

		public bool Visible()
		{
			return isVisible;
		}

		public void Visible(bool isVisible)
		{
			this.isVisible = isVisible;
		}

		public virtual void Update(bool isPaused)
		{
			if (isActive)
			{
				if (!isPaused)
				{
					// TODO GameObject.Update()
				}
			}
		}

		public virtual void Draw()
		{
			if (isVisible)
			{
				DrawPrimitive.Rectangle(position, color, (int)size.X, (int)size.Y, rotation);
			}
		}
	}
}
