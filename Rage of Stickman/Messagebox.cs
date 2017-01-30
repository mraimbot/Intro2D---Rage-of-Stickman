using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class Messagebox
	{
		private Vector2 position;
		private Vector2 size;
		private Color color_background;
		private Color color_text;
		private string text;
		private bool onClose;


		public Messagebox(Vector2 position, Vector2 size, Color color_background, string text, Color color_text)
		{
			this.position = position;
			this.size = size;
			this.color_background = color_background;
			this.color_text = color_text;
			this.text = text;
		}

		public bool Update()
		{
			Input();

			return onClose;
		}

		private void Input()
		{
			onClose = false;

			foreach (Keys key in Keyboard.GetState().GetPressedKeys())
			{
				if (!Game.Content.previousKeyState.IsKeyDown(key))
				{
					switch (key)
					{
						case Keys.Enter:
							onClose = true;
							break;
					}
				}
			}
		}

		public void Draw()
		{
			DrawPrimitive.Rectangle(position, color_background, (int)size.X, (int)size.Y);
			ShowText.Text(new Vector2(position.X + 10, position.Y + 10), text, color_text, 0, 1, ETextAlign.Left);
		}
	}
}
