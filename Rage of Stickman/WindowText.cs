using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class WindowText : WindowComponent
	{

		private string text;
		private GameEvent gameEvent;
		private bool onClick;
		private Color color_notMarked;
		private Color color_marked;
		private ETextFormate align;
		private float rotation;
		private float scale;


		public WindowText(bool markable, GameEvent gameEvent, string text, Color color_notMarked, Color color_marked, Vector2 position, ETextFormate align, float rotation = 0, float scale = 1, bool active = true, bool visible = true)
			: base(markable, position, Vector2.One, active, visible)
		{
			marked = false;

			this.gameEvent = gameEvent;
			this.text = text;

			this.color_marked = color_marked;
			this.color_notMarked = color_notMarked;
			this.align = align;
			this.rotation = rotation;
			this.scale = scale;
		}

		public override void Update(int index)
		{
			base.Update(index);

			if (active)
			{
				Input();
				Logic();
			}
		}

		private void Input()
		{
			onClick = false;

			foreach (Keys key in Keyboard.GetState().GetPressedKeys())
			{
				if (!Game.Content.previousKeyState.IsKeyDown(key))
				{
					switch (key)
					{
						case Keys.Enter:
						case Keys.Space:
							onClick = true;
							break;
					}
				}
			}
		}

		private void Logic()
		{
			if (marked)
			{
				if (onClick)
				{
					Game.Content.gameEvents.Add(gameEvent);
				}
			}
		}

		public override void Draw()
		{
			base.Draw();

			if (visible)
			{
				if (marked)
				{
					ShowText.Text(position, text, color_marked, rotation, scale, align);
				}
				else
				{
					ShowText.Text(position, text, color_notMarked, rotation, scale, align);
				}
			}
		}
	}
}
