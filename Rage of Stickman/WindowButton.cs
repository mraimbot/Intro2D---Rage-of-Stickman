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
		private AnimatedTexture2D background_notMarked;
		private AnimatedTexture2D background_marked;

		private bool onClick;

		private GameEvent gameEvent;


		public WindowButton(bool markable, GameEvent gameEvent, AnimatedTexture2D[] backgrounds, Vector2 position, Vector2 size, float rotation = 0, bool active = true, bool visible = true)
			: base(markable, position, size, rotation, active, visible)
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

		public override void Update(int index, bool isPaused)
		{
			base.Update(index, isPaused);

			if (isActive)
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
			if (isVisible)
			{
				if (marked)
				{
					if (background_marked != null)
					{
						background_marked.Update();
						background_marked.Draw(position, rotation: rotation);
					}
				}
				else
				{
					if (background_notMarked != null)
					{
						background_notMarked.Update();
						background_notMarked.Draw(position, rotation: rotation);
					}
				}
			}
		}
	}
}
