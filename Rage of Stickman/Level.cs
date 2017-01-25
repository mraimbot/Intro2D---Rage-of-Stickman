using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rage_of_Stickman
{
    class Level : SceneComponent
    {
		private AnimatedTexture2D foreground;

		private bool onBackToMenu;

		public Level(AnimatedTexture2D background, AnimatedTexture2D foreground, Vector2 position, Vector2 size, bool active = true, bool visible = true)
			: base(background, position, size, active, visible)
		{
			this.foreground = foreground;
		}

		public override void Update()
		{
			if (active)
			{
				Input();
				Logic();

				Game.Content.tileMap.Update();
				Game.Content.player.Update();

				// TODO Level.Update : Check player for goal-position

				for (int i = Game.Content.enemies.Count - 1; i >= 0; i--)
				{
					Game.Content.enemies[i].Update();
					if (Game.Content.enemies[i].isDead())
					{
						Game.Content.enemies.RemoveAt(i);
					}
				}
			}

			base.Update();
		}

		private void Input()
		{
			onBackToMenu = false;

			foreach (Keys key in Keyboard.GetState().GetPressedKeys())
			{
				if (!Game.Content.previousKeyState.IsKeyDown(key))
				{
					switch (key)
					{
						case Keys.Escape:
							onBackToMenu = true;
							break;
					}
				}
			}
		}

		private void Logic()
		{
			if (onBackToMenu)
			{
				Game.Content.gameEvents.Add(new GameEvent(ETarget.Main, EGameEvent.Open_Mainmenu));
			}
		}

		public override void Draw()
		{
			base.Draw();

			if (background != null)
			{
				background.Draw(position);
			}

			Game.Content.tileMap.Draw();

			foreach (Enemy enemy in Game.Content.enemies)
			{
				enemy.Draw();
			}

			Game.Content.player.Draw();

			if (foreground != null)
			{
				foreground.Draw(position);
			}
		}
	}
}
