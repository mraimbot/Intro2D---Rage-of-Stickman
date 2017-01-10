﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class AnimatedTexture2D
	{
		private readonly Texture2D[] textures;

		private int height;
		private int width;

		private int frameIndex;

		private float time;
		private float frameTime;

		private bool active;

		public AnimatedTexture2D(Texture2D[] textures, int height, int width)
		{
			this.textures = textures;
			this.height = height;
			this.width = width;

			frameTime = 0.1f;

			active = true;
		}

		public void resetAnimation()
		{
			frameIndex = 0;
		}

		public void toggleActiveState()
		{
			active = !active;
		}

		public Vector2 Size()
		{
			return new Vector2(width, height);
		}

		public void Update()
		{
			if (active)
			{
				time += (float)Game.Content.gameTime.ElapsedGameTime.Milliseconds;
				while (time > frameTime)
				{
					frameIndex++;
					if (frameIndex > textures.Length)
					{ 
						frameIndex = 1;
					}
					time = 0.0f;
				}
			}
		}

		public void Draw(Vector2 position)
		{
			Game.Content.spriteBatch.Draw(textures[frameIndex], position, Color.White);
		}
	}
}