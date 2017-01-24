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

		private Vector2 goal;

		public Level(Texture2D bitMap, Texture2D background, Vector2 playerStartPosition, Vector2 goal)
		{
			Initialize(bitMap, background, playerStartPosition, goal);
		}

		public void Initialize(Texture2D bitMap, Texture2D background, Vector2 playerStartPosition, Vector2 goal)
		{
			// Map
			Game.Content.tileMap = new TileMap(bitMap);
			this.background = background;
			// Player
			Game.Content.player = new Player(new Vector2(playerStartPosition.X * Game.Content.tileSize, playerStartPosition.Y * Game.Content.tileSize), EDirection.right, 1.5f, 0.7f, 100);
			// Enemies
			Game.Content.enemies.Add(new Kid(new Vector2(11 * Game.Content.tileSize, 27 * Game.Content.tileSize)));
			Game.Content.enemies.Add(new Oma(new Vector2(26 * Game.Content.tileSize, 25 * Game.Content.tileSize)));
			Game.Content.enemies.Add(new Zombie(new Vector2(33 * Game.Content.tileSize, 25 * Game.Content.tileSize)));
			// Goal
			this.goal = goal;
		}

		public void Update()
		{
			Game.Content.tileMap.Update();
			Game.Content.player.Update();

			// TODO Check player for goal-position

			for (int i = Game.Content.enemies.Count - 1; i >= 0; i--)
			{
				Game.Content.enemies[i].Update();
				if (Game.Content.enemies[i].isDead())
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
