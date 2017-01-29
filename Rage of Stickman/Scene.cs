using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
			// ----- Windows -----
			WindowButton play = new WindowButton(true, new GameEvent(ETarget.Main, EGameEvent.Open_Level1), new AnimatedTexture2D[] { new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Button_Play_notMarked") }), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Button_Play_marked") }) }, new Vector2(180, 375), Vector2.Zero);
			WindowButton exit = new WindowButton(true, new GameEvent(ETarget.Main, EGameEvent.Game_Exit), new AnimatedTexture2D[] { new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Button_Exit_notMarked") }), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Button_Exit_marked") }) }, new Vector2(870, 670), Vector2.Zero);
			WindowText credits = new WindowText(true, new GameEvent(ETarget.Main, EGameEvent.Open_Credits), "CREDITS", Color.Red, Color.Green, new Vector2(200, 500), ETextFormate.Left, 0, 2);
			WindowText text = new WindowText(false, null, "Press F4 to toggle fullscreen.", Color.Red, Color.Red, new Vector2(332, 600), ETextFormate.Left, 0, 1);
			WindowText text2 = new WindowText(false, null, "Move: AD or Arrow-Keys.", Color.Green, Color.Green, new Vector2(344, 632), ETextFormate.Left, 0, 1);
			WindowText text3 = new WindowText(false, null, "Jump: W, Space or Arrow-Keys.", Color.Green, Color.Green, new Vector2(356, 664), ETextFormate.Left, 0, 1);
			WindowText text4 = new WindowText(false, null, "Attack: EF or XY.", Color.Green, Color.Green, new Vector2(368, 696), ETextFormate.Left, 0, 1);
			List<WindowComponent> windowComponents = new List<WindowComponent>();
			windowComponents.Add(play);
			windowComponents.Add(credits);
			windowComponents.Add(exit);
			windowComponents.Add(text);
			windowComponents.Add(text2);
			windowComponents.Add(text3);
			windowComponents.Add(text4);
			Window window = new Window(windowComponents, new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Background_Mainmenu") }), new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			// ----- Music -----
			SceneMusic background_music = new SceneMusic(Game.Content.contentManager.Load<Song>("Music/get-started-intro-loop-7414"));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(window);
			components.Add(background_music);
			Scene scene = new Scene(components);

			return scene;
		}

		public static Scene CreateCredits()
		{
			// ----- Create Credits -----
			// ----- Camera -----
			Game.Content.camera = new Camera2D(Vector2.Zero, Vector2.Zero);
			// ----- Scene -----
			// ----- Windows -----
			WindowText back = new WindowText(true, new GameEvent(ETarget.Main, EGameEvent.Open_Mainmenu), "BACK", Color.Red, Color.Green, new Vector2(870, 670), ETextFormate.Left, 0.5f, 3);
			WindowText text = new WindowText(false, null, "Fonts", Color.Red, Color.Red, new Vector2(100, 300), ETextFormate.Left, 0, 2);
			WindowText text2 = new WindowText(false, null, "Anarchy: http://www.dafont.com/de/anarchy2.font?l[]=10&l[]=1", Color.Green, Color.Green, new Vector2(132, 332), ETextFormate.Left, 0, 1);
			WindowText text3 = new WindowText(false, null, "Music", Color.Red, Color.Red, new Vector2(100, 388), ETextFormate.Left, 0, 2);
			WindowText text4 = new WindowText(false, null, "Title-Music: http://www.audiyou.de/beitrag/get-started-intro-loop-7414.html", Color.Green, Color.Green, new Vector2(132, 420), ETextFormate.Left, 0, 1);
			WindowText text5 = new WindowText(false, null, "Background-Music: http://www.audiyou.de/beitrag/backbeat-db-110bpm-01-6414.html", Color.Green, Color.Green, new Vector2(132, 436), ETextFormate.Left, 0, 1);
			WindowText text6 = new WindowText(false, null, "Soundeffects", Color.Red, Color.Red, new Vector2(100, 484), ETextFormate.Left, 0, 2);
			WindowText text7 = new WindowText(false, null, "Player-Kick: http://freesound.org/people/newagesoup/sounds/348244/", Color.Green, Color.Green, new Vector2(132, 516), ETextFormate.Left, 0, 1);
			WindowText text8 = new WindowText(false, null, "Player-Punch: http://freesound.org/people/RSilveira_88/sounds/216197/", Color.Green, Color.Green, new Vector2(132, 532), ETextFormate.Left, 0, 1);
			WindowText text9 = new WindowText(false, null, "Player-Jump: http://freesound.org/people/jeremysykes/sounds/341247/", Color.Green, Color.Green, new Vector2(132, 548), ETextFormate.Left, 0, 1);

			List<WindowComponent> windowComponents = new List<WindowComponent>();
			windowComponents.Add(back);
			windowComponents.Add(text);
			windowComponents.Add(text2);
			windowComponents.Add(text3);
			windowComponents.Add(text4);
			windowComponents.Add(text5);
			windowComponents.Add(text6);
			windowComponents.Add(text7);
			windowComponents.Add(text8);
			windowComponents.Add(text9);
			Window window = new Window(windowComponents, new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Background_Mainmenu") }), new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			// ----- Music -----
			SceneMusic background_music = new SceneMusic(Game.Content.contentManager.Load<Song>("Music/get-started-intro-loop-7414"));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(window);
			components.Add(background_music);
			Scene scene = new Scene(components);

			return scene;
		}

		public static Scene CreateLevel1()
		{
			// ----- Create level one -----
			// ----- Camera -----
			Game.Content.camera = new Camera2D(new Vector2(Game.Content.viewport.Width / 2.0f, Game.Content.viewport.Height / 2.0f), Vector2.Zero);
			// ----- Player -----
			Game.Content.player = new Player(new Vector2(4, 25) * Game.Content.tileSize, EDirection.right);
			// ----- Map -----
			Game.Content.tileMap = new TileMap(Game.Content.contentManager.Load<Texture2D>("Graphics/TileMaps/Level1"));
			Level level = new Level(new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Background") }), null, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			RainSimulation rain = new RainSimulation(500, 20, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width * 2, 1), Game.Content.player);
			// ----- Music -----
			SceneMusic background_music = new SceneMusic(Game.Content.contentManager.Load<Song>("Music/backbeat-db-110bpm-01-6414"));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(level);
			components.Add(rain);
			components.Add(background_music);
			Scene scene = new Scene(components);
			// ----- Physics -----
			Game.Content.force_gravity = new Vector2(0, 9.807f);
			Game.Content.force_wind = new Vector2(-5f, 0);
			// ----- Enemies -----
			Game.Content.enemies.Clear();
			Game.Content.enemies.Add(new Kid(new Vector2(50, 19) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Oma(new Vector2(26, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(33, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(48, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(38, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(43, 20) * Game.Content.tileSize));
			// ----- Trigger -----
			Game.Content.triggers.Clear();
			Game.Content.triggers.Add(new Trigger(new GameEvent(ETarget.Main, EGameEvent.Open_Level2), new Vector2(251, 25) * Game.Content.tileSize, new Vector2(1,1) * Game.Content.tileSize));
			return scene;
		}

		public static Scene CreateLevel2()
		{
			// ----- Create level two -----
			// ----- Camera -----
			Game.Content.camera = new Camera2D(new Vector2(Game.Content.viewport.Width / 2.0f, Game.Content.viewport.Height / 2.0f), Vector2.Zero);
			// ----- Player -----
			Game.Content.player = new Player(new Vector2(4, 25) * Game.Content.tileSize, EDirection.right);
			// ----- Map -----
			Game.Content.tileMap = new TileMap(Game.Content.contentManager.Load<Texture2D>("Graphics/TileMaps/Level2"));
			Level level = new Level(new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Background") }), null, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			RainSimulation rain = new RainSimulation(500, 20, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width * 2, 1), Game.Content.player);
			// ----- Music -----
			SceneMusic background_music = new SceneMusic(Game.Content.contentManager.Load<Song>("Music/backbeat-db-110bpm-01-6414"));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(level);
			components.Add(rain);
			components.Add(background_music);
			Scene scene = new Scene(components);
			// ----- Physics -----
			Game.Content.force_gravity = new Vector2(0, 9.807f);
			Game.Content.force_wind = new Vector2(-5f, 0);
			// ----- Enemies -----
			Game.Content.enemies.Clear();
			Game.Content.enemies.Add(new Kid(new Vector2(50, 19) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Oma(new Vector2(26, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(33, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(48, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(38, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(43, 20) * Game.Content.tileSize));
			// ----- Trigger -----
			Game.Content.triggers.Clear();
			Game.Content.triggers.Add(new Trigger(new GameEvent(ETarget.Main, EGameEvent.Open_Level3), new Vector2(251, 25) * Game.Content.tileSize, new Vector2(1, 1) * Game.Content.tileSize));
			return scene;
		}

		public static Scene CreateLevel3()
		{
			// ----- Create level three -----
			// ----- Camera -----
			Game.Content.camera = new Camera2D(new Vector2(Game.Content.viewport.Width / 2.0f, Game.Content.viewport.Height / 2.0f), Vector2.Zero);
			// ----- Player -----
			Game.Content.player = new Player(new Vector2(4, 25) * Game.Content.tileSize, EDirection.right);
			// ----- Map -----
			Game.Content.tileMap = new TileMap(Game.Content.contentManager.Load<Texture2D>("Graphics/TileMaps/Level3"));
			Level level = new Level(new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Background") }), null, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			RainSimulation rain = new RainSimulation(500, 20, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width * 2, 1), Game.Content.player);
			// ----- Music -----
			SceneMusic background_music = new SceneMusic(Game.Content.contentManager.Load<Song>("Music/backbeat-db-110bpm-01-6414"));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(level);
			components.Add(rain);
			components.Add(background_music);
			Scene scene = new Scene(components);
			// ----- Physics -----
			Game.Content.force_gravity = new Vector2(0, 9.807f);
			Game.Content.force_wind = new Vector2(-5f, 0);
			// ----- Enemies -----
			Game.Content.enemies.Clear();
			Game.Content.enemies.Add(new Kid(new Vector2(50, 19) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Oma(new Vector2(26, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(33, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(48, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(38, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(43, 20) * Game.Content.tileSize));
			// ----- Trigger -----
			Game.Content.triggers.Clear();
			Game.Content.triggers.Add(new Trigger(new GameEvent(ETarget.Main, EGameEvent.Open_Mainmenu), new Vector2(251, 25) * Game.Content.tileSize, new Vector2(1, 1) * Game.Content.tileSize));
			return scene;
		}
	}
}
