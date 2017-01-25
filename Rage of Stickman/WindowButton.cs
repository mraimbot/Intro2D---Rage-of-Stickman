using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rage_of_Stickman
{
	class WindowButton : WindowComponent
	{
		AnimatedTexture2D background_notMarked;
		AnimatedTexture2D background_marked;

		private bool marked;
		private int ID;

		private bool onClick;

		private GameEvent gameEvent;


		public WindowButton(int ID, GameEvent gameEvent, AnimatedTexture2D[] backgrounds, Vector2 position, Vector2 size, bool active = true, bool visible = true)
			: base(position, size, active, visible)
		{
			marked = false;
			this.ID = ID;

			this.gameEvent = gameEvent;
		}

		public void Update(int index)
		{
			if (active)
			{
				Input();
				Logic(index);
			}

			base.Update();
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

		private void Logic(int index)
		{
			marked = (index == ID) ? (true) : (false);

			if (onClick)
			{
				Game.Content.gameEvents.Add(gameEvent);
			}
		}

		public override void Draw()
		{
			base.Draw();

			if (marked)
			{
				if (background_marked != null)
				{
					background_marked.Draw(position);
				}
			}
			else
			{
				if (background_notMarked != null)
				{
					background_notMarked.Draw(position);
				}
			}
		}
	}
}
