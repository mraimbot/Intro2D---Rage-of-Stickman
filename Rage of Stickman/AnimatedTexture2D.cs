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
		private Texture2D[] textures;

		private int height;
		private int width;

		private int frameIndex;

		private float time;
		private float frameTime;

		private bool active;

		public AnimatedTexture2D(Texture2D[] textures, int width = 0, int height = 0, float frameTime = 100.0f)
		{
			this.textures = textures;
			if (width == 0)
			{
				this.width = this.textures[0].Width;
			}
			else
			{
				this.width = width;
			}

			if (height == 0)
			{
				this.height = this.textures[0].Height;
			}
			else
			{
				this.height = height;
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
			return new Vector2(width, height);
		}

		public void Update()
		{
			if (active)
			{
				time += (float)Game.Content.gameTime.ElapsedGameTime.Milliseconds;
				if (time > frameTime)
				{
					frameIndex++;
					if (frameIndex >= textures.Length)
					{ 
						frameIndex = 0;
					}
					time = 0.0f;
				}
			}
		}

		public void Draw(Vector2 position, SpriteEffects s = SpriteEffects.None)
		{
			Game.Content.spriteBatch.Draw(textures[frameIndex], position, null, null, null, 0, Vector2.One, Color.White, s, 0);
		}
	}
}
