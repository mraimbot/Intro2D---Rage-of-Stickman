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

		private bool onClick;

		private GameEvent gameEvent;


		public WindowButton(bool markable, GameEvent gameEvent, AnimatedTexture2D[] backgrounds, Vector2 position, Vector2 size, bool active = true, bool visible = true)
			: base(markable, position, size, active, visible)
		{
			marked = false;

			this.gameEvent = gameEvent;
			background_notMarked = backgrounds[0];
			background_marked = backgrounds[1];

			if (size == Vector2.Zero)
			{
				size = background_notMarked.Size();
			}
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
}
