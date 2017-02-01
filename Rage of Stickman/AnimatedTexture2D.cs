using Microsoft.Xna.Framework;
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
		private Texture2D[] frames;
		private bool useDynamicSize;

		private Vector2 size;

		private int frameIndex;

		private float time;
		private float frameTime;

		private bool active;

		public AnimatedTexture2D(Texture2D[] frames, float frameTime = 100.0f, int width = 0, int height = 0)
		{
			this.frames = frames;
			if (width == 0 || height == 0)
			{
				useDynamicSize = true;
				this.size.X = this.frames[0].Width;
				this.size.Y = this.frames[0].Height;
			}
			else
			{
				useDynamicSize = false;
				this.size.X = width;
				this.size.Y = height;
			}

			this.frameTime = frameTime;

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
			return size;
		}

		public void Update()
		{
			if (active)
			{
				time += (float)Game.Content.gameTime.ElapsedGameTime.Milliseconds;
				if (time > frameTime)
				{
					frameIndex++;
					if (frameIndex >= frames.Length)
					{ 
						frameIndex = 0;
					}

					if (useDynamicSize)
					{
						size.X = frames[frameIndex].Width;
						size.Y = frames[frameIndex].Height;
					}

					time = 0.0f;
				}
			}
		}

		public void Draw(Vector2 position, SpriteEffects s = SpriteEffects.None, float rotation = 0)
		{
			Game.Content.spriteBatch.Draw(frames[frameIndex], position, null, null, null, rotation, Vector2.One, Color.White, s, 0);
		}
	}
}
