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
    class Level : SceneComponent
    {
		private AnimatedTexture2D level_background;
		private AnimatedTexture2D level_foreground;

		public Level(AnimatedTexture2D background, Vector2 position, Vector2 size, bool active = true, bool visible = true)
			: base(background, position, size, active, visible)
		{
			Initialize();
		}

		public void Initialize()
		{
			// TODO Level.InitializeLevel0() : Make better level creating (maybe with files)
			// ----- Map -----
			Game.Content.tileMap = new TileMap(Game.Content.contentManager.Load<Texture2D>("Graphics/RageMap"));
			level_background = new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Background") } );
			level_foreground = null;
			// ----- Player -----
			Game.Content.player = new Player(new Vector2(9 * Game.Content.tileSize, 20 * Game.Content.tileSize), EDirection.right, 1.5f, 0.7f, 100);
			// ----- Enemies -----
			Game.Content.enemies.Add(new Kid(new Vector2(11 * Game.Content.tileSize, 27 * Game.Content.tileSize)));
			Game.Content.enemies.Add(new Oma(new Vector2(26 * Game.Content.tileSize, 25 * Game.Content.tileSize)));
			Game.Content.enemies.Add(new Zombie(new Vector2(33 * Game.Content.tileSize, 25 * Game.Content.tileSize)));
		}

		public override void Update()
		{
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

			base.Update();
		}

		public override void Draw()
		{
			base.Draw();

			if (level_background != null)
			{
				level_background.Draw(position);
			}

			Game.Content.tileMap.Draw();

			foreach (Enemy enemy in Game.Content.enemies)
			{
				enemy.Draw();
			}

			Game.Content.player.Draw();

			if (level_foreground != null)
			{
				level_foreground.Draw(position);
			}
		}
	}
}
