using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	enum ETextFormate
	{
		Left,
		Right,
		Center
	}

	class ShowText
	{
		public static void Text(Vector2 position, string text, Color color, ETextFormate formate = ETextFormate.Left)
		{
			if (Game.Content.fonts[(int)EFont.Anarchy] == null)
			{
				Game.Content.fonts[(int)EFont.Anarchy] = Game.Content.contentManager.Load<SpriteFont>("Fonts/Anarchy");
			}

			if (formate == ETextFormate.Right)
			{
				position.X -= Game.Content.fonts[(int)EFont.Anarchy].MeasureString(text).X;
			}
			else if (formate == ETextFormate.Center)
			{
				position.X -= Game.Content.fonts[(int)EFont.Anarchy].MeasureString(text).X / 2;
			}

			Game.Content.spriteBatch.DrawString(Game.Content.fonts[(int)EFont.Anarchy], text, position, color);
		}
	}
}
