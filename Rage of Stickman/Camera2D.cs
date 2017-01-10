using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class Camera2D
	{
		public Vector2 origin;
		public Vector2 position;
		public float rotation;
		public float zoom;

		public Camera2D(Viewport viewport)
		{
			origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
			position = Vector2.Zero;
			rotation = 0;
			zoom = 1;
		}

		public Matrix GetViewMatrix()
		{
			return
				Matrix.CreateTranslation(new Vector3(-position, 0.0f)) *
				Matrix.CreateTranslation(new Vector3(origin, 0.0f)) *
				Matrix.CreateRotationZ(rotation) *
				Matrix.CreateScale(zoom, zoom, 1);
		}
	}
}

