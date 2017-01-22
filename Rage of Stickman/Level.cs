using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rage_of_Stickman
{
	enum EGameState
	{
		paused,
		run
	}
    class Level
    {
		Texture2D background;

        private Point exit = InvalidPosition;
        private static readonly Point InvalidPosition = new Point(-1,-1);

        private bool ReachedExit => reachedExit;
        private bool reachedExit;

		public Level()
		{
			Initialize();
		}

		public void Initialize()
		{
			// Enemies
			Game.Content.enemies.Add(new Kid(new Vector2(11, 27)));
			Game.Content.enemies.Add(new Oma(new Vector2(26, 25)));
			Game.Content.enemies.Add(new Zombie(new Vector2(33, 25)));
		}

		public void LoadBackground(Texture2D background)
		{
			this.background = background;
		}

		public void Update()
		{
			Game.Content.tileMap.Update();
			Game.Content.player.Update();

			for (int i = Game.Content.enemies.Count - 1; i >= 0; i--)
			{
				Game.Content.enemies[i].Update();
				if (Game.Content.enemies[i].IsDead())
				{
					Game.Content.enemies.RemoveAt(i);
				}
			}
		}

		public void Draw()
		{
			Game.Content.spriteBatch.Draw(background, new Vector2(0.0f, 0.0f), Color.White);

			Game.Content.tileMap.Draw();
			Game.Content.player.Draw();

			for (int i = 0; i < Game.Content.enemies.Count; i++)
			{
				Game.Content.enemies[i].Draw();
			}
		}
    }
}
