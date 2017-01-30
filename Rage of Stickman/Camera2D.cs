using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class Camera2D
	{
		private Vector2 origin;
		private Vector2 position;
		private float rotation;
		private float zoom;

		public Camera2D(Vector2 origin, Vector2 position, float rotation = 0, float zoom = 1)
		{
			this.origin = origin;
			this.position = position;
			this.rotation = rotation;
			this.zoom = 1; // zoom;
		}

		public Vector2 Position()
		{
			return position;
		}

		public Vector2 Origin()
		{
			return origin;
		}

		public Matrix GetViewMatrix()
		{
			return
				Matrix.CreateTranslation(new Vector3(-position, 0.0f)) *
				Matrix.CreateTranslation(new Vector3(origin, 0.0f)) *
				Matrix.CreateRotationZ(rotation) *
				Matrix.CreateScale(zoom, zoom, 1) *
				1;
		}

		public void Update(Vector2 newPosition)
		{
			position = newPosition;
		}
	}
}

