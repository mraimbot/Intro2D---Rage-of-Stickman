using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class Scene
	{
		/// <summary>
		/// Scene is a Screen were other stuff goes on.
		/// Multiple scenecomponents can be placed into one scene.
		/// </summary>

		private List<SceneComponent> components;

		public Scene(List<SceneComponent> components)
		{
			this.components = components;
		}

		public void Update()
		{
			if (components == null)
			{
				components = new List<SceneComponent>();
			}

			if (components.Count > 0)
			{
				foreach (SceneComponent component in components)
				{
					component.Update();
				}
			}
		}

		public void Draw()
		{
			if (components == null)
			{
				components = new List<SceneComponent>();
			}

			if (components.Count > 0)
			{
				foreach (SceneComponent component in components)
				{
					component.Draw();
				}
			}
		}

		public static Scene CreateMainmenu()
		{
			// ----- Create mainmenu -----
			// ----- Camera -----
			Game.Content.camera = new Camera2D(Vector2.Zero, Vector2.Zero);
			// ----- Scene -----
			WindowButton play = new WindowButton(true, new GameEvent(ETarget.Main, EGameEvent.Open_Level1), new AnimatedTexture2D[] { new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Button_Play_notMarked") }), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Button_Play_marked") }) }, new Vector2(180, 375), Vector2.Zero);
			WindowButton exit = new WindowButton(true, new GameEvent(ETarget.Main, EGameEvent.Game_Exit), new AnimatedTexture2D[] { new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Button_Exit_notMarked") }), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Button_Exit_marked") }) }, new Vector2(870, 670), Vector2.Zero);
			List<WindowComponent> windowComponents = new List<WindowComponent>();
			windowComponents.Add(play);
			windowComponents.Add(exit);
			Window window = new Window(windowComponents, new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Background_Mainmenu") }), new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(window);
			Scene scene = new Scene(components);

			return scene;
		}

		public static Scene CreateLevel1()
		{
			// ----- Create level one -----
			// ----- Camera -----
			Game.Content.camera = new Camera2D(new Vector2(Game.Content.viewport.Width / 2.0f, Game.Content.viewport.Height / 2.0f), Vector2.Zero);
			// ----- Player -----
			Game.Content.player = new Player(new Vector2(9 * Game.Content.tileSize, 20 * Game.Content.tileSize), EDirection.right);
			// ----- Map -----
			Game.Content.tileMap = new TileMap(Game.Content.contentManager.Load<Texture2D>("Graphics/RageMap"));
			Level level = new Level(new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Background") }), null, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			RainSimulation rain = new RainSimulation(500, 20, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width * 2, 1), Game.Content.player);
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(level);
			components.Add(rain);
			Scene scene = new Scene(components);
			// ----- Physics -----
			Game.Content.force_gravity = new Vector2(0, 9.807f);
			Game.Content.force_wind = new Vector2(-5f, 0);
			// ----- Enemies -----
			Game.Content.enemies.Clear();
			Game.Content.enemies.Add(new Kid(new Vector2(11 * Game.Content.tileSize, 27 * Game.Content.tileSize)));
			Game.Content.enemies.Add(new Oma(new Vector2(26 * Game.Content.tileSize, 25 * Game.Content.tileSize)));
			Game.Content.enemies.Add(new Zombie(new Vector2(33 * Game.Content.tileSize, 26 * Game.Content.tileSize)));
			Game.Content.enemies.Add(new Zombie(new Vector2(48 * Game.Content.tileSize, 26 * Game.Content.tileSize)));
			Game.Content.enemies.Add(new Zombie(new Vector2(38 * Game.Content.tileSize, 26 * Game.Content.tileSize)));
			Game.Content.enemies.Add(new Zombie(new Vector2(43 * Game.Content.tileSize, 26 * Game.Content.tileSize)));

			return scene;
		}
	}
}
