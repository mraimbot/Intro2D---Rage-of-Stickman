﻿using System;
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

			index_max = -1;
			foreach (WindowComponent component in this.components)
			{
				if (component.Markable())
				{
					index_max++;
					component.SetID(index_max);
				}
			}

			index = (index_max >= 0) ? (0) : (-1);
		}

		public override void Update()
		{
			if (active)
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
						component.Update(index);
					}
				}
			}

			base.Update();

			// TODO DEBUG A
			Console.WriteLine("index_max = " + index_max + "; index = " + index);
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
