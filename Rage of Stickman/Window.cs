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
			this.components = components;

			index_max = 0;
			foreach (WindowComponent component in this.components)
			{
				if (component.Markable())
				{
					index_max++;
				}
			}

			index = 0;
		}

		public override void Update()
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
					component.Update(true);
				}
			}
		}

		private void Input()
		{
			foreach (Keys key in Keyboard.GetState().GetPressedKeys())
			{
				if (!Game.Content.previousKeyState.IsKeyDown(key))
				{
					switch (key)
					{
						case Keys.W:
						case Keys.Up:
							onClick = true;
							break;
					}
				}
			}
		}

		private void Logic()
		{

		}

		public override void Draw()
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
