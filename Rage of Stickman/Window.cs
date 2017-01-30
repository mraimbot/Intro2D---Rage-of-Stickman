using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rage_of_Stickman
{
	class Window : SceneComponent
	{
		private List<WindowComponent> components;
		private int index_max;
		private int index;

		private bool onUp;
		private bool onDown;

		public Window(List<WindowComponent> components, AnimatedTexture2D background, Vector2 position, Vector2 size, bool active = true, bool visible = true)
			: base(background, position, size, active, visible)
		{
			if (components != null)
			{
				this.components = components;

				index_max = -1;
				foreach (WindowComponent component in components)
				{
					component.MoveTo(component.Position() + position);

					if (component.Markable())
					{
						index_max++;
						component.SetID(index_max);
					}
				}

				index = (index_max >= 0) ? (0) : (-1);
			}
		}

		public override void EventHandler()
		{
			base.EventHandler();
		}

		public override void MoveTo(Vector2 position)
		{
			base.MoveTo(position);

			foreach (WindowComponent component in components)
			{
				component.MoveTo(component.StartPosition() + position);
			}
		}

		public override void Update(bool isPaused)
		{
			if (active)
			{
				if (!isPaused)
				{
					Input();
					Logic();

					if (components == null)
					{
						components = new List<WindowComponent>();
					}

					if (components.Count > 0)
					{
						foreach (WindowComponent component in components)
						{
							component.Update(index, isPaused);
						}
					}
				}
			}

			base.Update(isPaused);
		}

		private void Input()
		{
			onUp = false;
			onDown = false;

			foreach (Keys key in Keyboard.GetState().GetPressedKeys())
			{
				if (!Game.Content.previousKeyState.IsKeyDown(key))
				{
					switch (key)
					{
						case Keys.W:
						case Keys.Up:
							onUp = true;
							break;

						case Keys.S:
						case Keys.Down:
							onDown = true;
							break;
					}
				}
			}
		}

		private void Logic()
		{
			if (onUp)
			{
				index--;
			}

			if (onDown)
			{
				index++;
			}

			if (index < 0 && index_max >= 0)
			{
				index = 0;
			}
			else if (index > index_max)
			{
				index = index_max;
			}
		}

		public override void Draw()
		{
			base.Draw();

			if (visible)
			{
				if (components == null)
				{
					components = new List<WindowComponent>();
				}

				if (components.Count > 0)
				{
					foreach (WindowComponent component in components)
					{
						component.Draw();
					}
				}
			}
		}
	}
}
