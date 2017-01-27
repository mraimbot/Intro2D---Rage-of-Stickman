using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class DrawPrimitive
	{
		public static void Point(Vector2 position, Color color)
		{
			Rectangle(position, color: color);
		}

		public static void Rectangle(Vector2 position, Color color, int width = 1, int height = 1, float angle = 0)
		{
			if (Game.Content.textures[(int)ETexture.Pixel] == null)
			{
				Game.Content.textures[(int)ETexture.Pixel] = Game.Content.contentManager.Load<Texture2D>("Graphics/Pixel");
			}

			Game.Content.spriteBatch.Draw(Game.Content.textures[(int)ETexture.Pixel], null, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle(0, 0, width, height), null, angle, null, color, SpriteEffects.None, 0);
		}
	}
}
