﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

		private Messagebox messagebox;
		private Window window_gameover;
		private Window window_paused;

		private bool isGameover;
		private bool isPaused;
		private bool ShowMessagebox;

		public Scene(List<SceneComponent> components)
		{
			this.components = components;
			Initialize();
		}

		public void Initialize()
		{
			isGameover = false;
			isPaused = false;
			ShowMessagebox = false;
		}

		public void EventHandler()
		{
			if (Game.Content.gameEvents.Count > 0)
			{
				for (int ID = Game.Content.gameEvents.Count - 1; ID >= 0; ID--)
				{
					if (Game.Content.gameEvents[ID].Target() == ETarget.Scene)
					{
						switch (Game.Content.gameEvents[ID].Event())
						{
							case EGameEvent.ShowMessagebox:
								isPaused = true;
								ShowMessagebox = true;
								messagebox = new Messagebox(new Vector2(Game.Content.camera.Position().X - Game.Content.camera.Origin().X, Game.Content.camera.Position().Y - Game.Content.camera.Origin().Y + Game.Content.viewport.Height - 150), new Vector2(Game.Content.viewport.Width, 150), new Color(0, 0, 0, 200), Game.Content.gameEvents[ID].Text(), Color.White);
								break;

							case EGameEvent.TogglePause:
								isPaused = !isPaused;
								break;

							case EGameEvent.Gameover:
								isGameover = true;
								break;
						}
						Game.Content.gameEvents.RemoveAt(ID);
					}
				}

				if (components != null)
				{
					foreach (SceneComponent component in components)
					{
						component.EventHandler();
					}
				}
			}
		}

		public void Update()
		{
			Input();

			if (ShowMessagebox)
			{
				if (messagebox.Update())
				{
					ShowMessagebox = false;
					isPaused = false;
				}
			}

			if (isGameover)
			{
				if (window_gameover == null)
				{
					window_gameover = CreateGameoverWindow();
				}
				window_gameover.MoveTo(new Vector2(Game.Content.camera.Position().X - 100, Game.Content.camera.Position().Y - 200));
				window_gameover.Update(false);
			}

			else if (isPaused && !ShowMessagebox)
			{
				if (window_paused == null)
				{
					window_paused = CreatePausedWindow();
				}
				window_paused.MoveTo(new Vector2(Game.Content.camera.Position().X - 100, Game.Content.camera.Position().Y - 200));
				window_paused.Update(false);
			}

			if (components == null)
			{
				components = new List<SceneComponent>();
			}

			if (components.Count > 0)
			{
				foreach (SceneComponent component in components)
				{
					component.Update(isPaused);
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
						case Keys.Escape:
						case Keys.P:
							isPaused = !isPaused;
							break;
					}
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

			if (isGameover && window_gameover != null)
			{
				window_gameover.Draw();
			}

			else if (ShowMessagebox && messagebox != null)
			{
				messagebox.Draw();
			}

			else if (isPaused && !ShowMessagebox)
			{
				window_paused.Draw();
			}
		}

		public static Window CreatePausedWindow()
		{
			List<WindowComponent> window_components = new List<WindowComponent>();
			window_components.Add(new WindowText(true, new GameEvent(ETarget.Scene, EGameEvent.TogglePause), "Continue", Color.Red, Color.Green, new Vector2(100, 32), ETextAlign.Center));
			window_components.Add(new WindowText(true, new GameEvent(ETarget.Main, EGameEvent.Open_Mainmenu), "Back to Menu", Color.Red, Color.Green, new Vector2(100, 64), ETextAlign.Center));
			return new Window(window_components, null, new Color(16, 0, 0, 16), new Vector2(Game.Content.camera.Position().X - 100, Game.Content.camera.Position().Y - 100), new Vector2(200, 128));
		}

		public static Window CreateGameoverWindow()
		{
			List<WindowComponent> window_components = new List<WindowComponent>();
			window_components.Add(new WindowText(false, null, "Game Over", Color.Red, Color.Green, new Vector2(100, 32), ETextAlign.Center));
			window_components.Add(new WindowText(false, null, "You are dead!", Color.Red, Color.Green, new Vector2(100, 64), ETextAlign.Center));
			window_components.Add(new WindowText(true, new GameEvent(ETarget.Main, EGameEvent.Open_Mainmenu), "Back to Menu", Color.Red, Color.Green, new Vector2(100, 96), ETextAlign.Center));
			return new Window(window_components, null, new Color(16, 0, 0, 16), new Vector2(Game.Content.camera.Position().X - 100, Game.Content.camera.Position().Y - 100), new Vector2(200, 128));
		}

		public static Scene CreateMainmenu()
		{
			// ----- Create mainmenu -----
			// ----- Camera -----
			Game.Content.camera = new Camera2D(Vector2.Zero, Vector2.Zero);
			// ----- Scene -----
			// ----- Windows -----
			WindowButton title = new WindowButton(false, null, new AnimatedTexture2D[] { new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Images/RageOfStickman_red"), Game.Content.contentManager.Load<Texture2D>("Graphics/Images/RageOfStickman_red_100") }, frameTime: 1000), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Pixel") }) }, new Vector2(200, 50), Vector2.Zero);
			WindowButton play = new WindowButton(true, new GameEvent(ETarget.Main, EGameEvent.Open_Intro), new AnimatedTexture2D[] { new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Button_Play_notMarked") }), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Button_Play_marked") }) }, new Vector2(180, 260), Vector2.Zero);
			WindowButton credits = new WindowButton(true, new GameEvent(ETarget.Main, EGameEvent.Open_Credits), new AnimatedTexture2D[] { new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Credits_red") }), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Credits_green") }) }, new Vector2(225, 395), Vector2.Zero);
			WindowButton exit = new WindowButton(true, new GameEvent(ETarget.Main, EGameEvent.Game_Exit), new AnimatedTexture2D[] { new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Exit_red") }), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Exit_green") }) }, new Vector2(263, 465), Vector2.Zero);
			WindowText text = new WindowText(false, null, "Press F4 to toggle fullscreen.", Color.Red, Color.Red, new Vector2(332, 550), ETextAlign.Left, 0, 1);
			WindowText text2 = new WindowText(false, null, "Move: AD or Arrow-Keys.", Color.Green, Color.Green, new Vector2(344, 582), ETextAlign.Left, 0, 1);
			WindowText text3 = new WindowText(false, null, "Jump: W, Space or Arrow-Keys.", Color.Green, Color.Green, new Vector2(356, 614), ETextAlign.Left, 0, 1);
			WindowText text4 = new WindowText(false, null, "Attack: EF or XY.", Color.Green, Color.Green, new Vector2(368, 646), ETextAlign.Left, 0, 1);
			WindowText text5 = new WindowText(false, null, "Pause: Escape, P.", Color.Green, Color.Green, new Vector2(380, 678), ETextAlign.Left, 0, 1);
			List<WindowComponent> windowComponents = new List<WindowComponent>();
			windowComponents.Add(title);
			windowComponents.Add(play);
			windowComponents.Add(credits);
			windowComponents.Add(exit);
			windowComponents.Add(text);
			windowComponents.Add(text2);
			windowComponents.Add(text3);
			windowComponents.Add(text4);
			windowComponents.Add(text5);
			Window window = new Window(windowComponents, new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Backgrounds/Background_Title") }), Color.White, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
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
			WindowButton title = new WindowButton(false, null, new AnimatedTexture2D[] { new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Images/RageOfStickman_red"), Game.Content.contentManager.Load<Texture2D>("Graphics/Images/RageOfStickman_red_100") }, frameTime: 1000), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Pixel") }) }, new Vector2(200, 50), Vector2.Zero);
			WindowButton back = new WindowButton(true, new GameEvent(ETarget.Main, EGameEvent.Open_Mainmenu), new AnimatedTexture2D[] { new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Back_red") }), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Back_green") }) }, new Vector2(475, 680), Vector2.Zero);
			WindowButton credits = new WindowButton(false, null, new AnimatedTexture2D[] { new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Window/Button/Credits_red") }), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Pixel") }) }, new Vector2(310, 220), Vector2.Zero);
			WindowText text = new WindowText(false, null, "Fonts", Color.Red, Color.Red, new Vector2(100, 280), ETextAlign.Left, 0, 2);
			WindowText text2 = new WindowText(false, null, "Anarchy: http://www.dafont.com/de/anarchy2.font", Color.Green, Color.Green, new Vector2(132, 328), ETextAlign.Left, 0, 1);
			WindowText text10 = new WindowText(false, null, "Algerian: Microsoft", Color.Green, Color.Green, new Vector2(132, 360), ETextAlign.Left, 0, 1);
			WindowText text3 = new WindowText(false, null, "Music", Color.Red, Color.Red, new Vector2(100, 392), ETextAlign.Left, 0, 2);
			WindowText text4 = new WindowText(false, null, "Title-Music: http://www.audiyou.de/beitrag/get-started-intro-loop-7414.html", Color.Green, Color.Green, new Vector2(132, 440), ETextAlign.Left, 0, 1);
			WindowText text5 = new WindowText(false, null, "Background-Music: http://www.audiyou.de/beitrag/backbeat-db-110bpm-01-6414.html", Color.Green, Color.Green, new Vector2(132, 472), ETextAlign.Left, 0, 1);
			WindowText text6 = new WindowText(false, null, "Soundeffects", Color.Red, Color.Red, new Vector2(100, 520), ETextAlign.Left, 0, 2);
			WindowText text7 = new WindowText(false, null, "Player-Kick: http://freesound.org/people/newagesoup/sounds/348244/", Color.Green, Color.Green, new Vector2(132, 568), ETextAlign.Left, 0, 1);
			WindowText text8 = new WindowText(false, null, "Player-Punch: http://freesound.org/people/RSilveira_88/sounds/216197/", Color.Green, Color.Green, new Vector2(132, 600), ETextAlign.Left, 0, 1);
			WindowText text9 = new WindowText(false, null, "Player-Jump: http://freesound.org/people/jeremysykes/sounds/341247/", Color.Green, Color.Green, new Vector2(132, 632), ETextAlign.Left, 0, 1);
			List<WindowComponent> windowComponents = new List<WindowComponent>();
			windowComponents.Add(title);
			windowComponents.Add(back);
			windowComponents.Add(credits);
			windowComponents.Add(text);
			windowComponents.Add(text2);
			windowComponents.Add(text10);
			windowComponents.Add(text3);
			windowComponents.Add(text4);
			windowComponents.Add(text5);
			windowComponents.Add(text6);
			windowComponents.Add(text7);
			windowComponents.Add(text8);
			windowComponents.Add(text9);
			Window window = new Window(windowComponents, new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Backgrounds/Background_Title") }), Color.White, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			// ----- Music -----
			SceneMusic background_music = new SceneMusic(Game.Content.contentManager.Load<Song>("Music/get-started-intro-loop-7414"));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(window);
			components.Add(background_music);
			Scene scene = new Scene(components);

			return scene;
		}

		public static Scene CreateIntro()
		{
			// ----- Create intro -----
			// ----- Camera -----
			Game.Content.camera = new Camera2D(Vector2.Zero, Vector2.Zero);
			// ----- Window -----
			Window window = new Window(null, new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Backgrounds/Background_Boss") }), Color.White, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			// ----- Eventlist -----
			List<GameEvent> gameevents = new List<GameEvent>();
			gameevents.Add(new GameEvent(ETarget.Scene, EGameEvent.ShowMessagebox, text: "Boss: You are fired!"));
			gameevents.Add(new GameEvent(ETarget.Scene, EGameEvent.ShowMessagebox, text: "Nooooo!!!"));
			gameevents.Add(new GameEvent(ETarget.SceneComponent, EGameEvent.NewBackground, text: "Graphics/Backgrounds/Background_Girlfriend"));
			gameevents.Add(new GameEvent(ETarget.Scene, EGameEvent.ShowMessagebox, text: "Phone: I break up with you!"));
			gameevents.Add(new GameEvent(ETarget.Scene, EGameEvent.ShowMessagebox, text: "Nooooo!!!"));
			gameevents.Add(new GameEvent(ETarget.Main, EGameEvent.Open_Level1));
			SceneEventbox eventbox = new SceneEventbox(gameevents);
			// ----- Music -----
			SceneMusic background_music = new SceneMusic(Game.Content.contentManager.Load<Song>("Music/backbeat-db-110bpm-01-6414"));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(window);
			components.Add(eventbox);
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
			Game.Content.player = new Player(new Vector2(8, 25) * Game.Content.tileSize);
			// ----- Map -----
			Game.Content.tileMap = new TileMap(Game.Content.contentManager.Load<Texture2D>("Graphics/TileMaps/Level1"));
			SceneLevel level = new SceneLevel(new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Backgrounds/Background_Level1") }), new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Backgrounds/Foreground_Level1") }), Vector2.Zero, new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			SceneRainSimulation rain = new SceneRainSimulation(1000, 20, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width * 2, 1), Game.Content.player);
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
			Game.Content.enemies.Add(new Oma(new Vector2(30, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(33, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(48, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(38, 20) * Game.Content.tileSize));
			Game.Content.enemies.Add(new Zombie(new Vector2(43, 20) * Game.Content.tileSize));
			// ----- Trigger -----
			Game.Content.triggers.Clear();
			Game.Content.triggers.Add(new Trigger(null, new GameEvent(ETarget.Main, EGameEvent.Open_Level2), new Vector2(251, 25) * Game.Content.tileSize, new Vector2(1,1) * Game.Content.tileSize, false, false, true));
			Game.Content.triggers.Add(new Trigger(new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Images/MediPack") }, 999), new GameEvent(ETarget.Level, EGameEvent.Player_Heal, 50), new Vector2(120, 26) * Game.Content.tileSize, new Vector2(32, 32), true, true, true));
			return scene;
		}

		public static Scene CreateLevel2()
		{
			// ----- Create level two -----
			// ----- Camera -----
			Game.Content.camera = new Camera2D(new Vector2(Game.Content.viewport.Width / 2.0f, Game.Content.viewport.Height / 2.0f), Vector2.Zero);
			// ----- Player -----
			Game.Content.player = new Player(new Vector2(4, 25) * Game.Content.tileSize);
			// ----- Map -----
			Game.Content.tileMap = new TileMap(Game.Content.contentManager.Load<Texture2D>("Graphics/TileMaps/Level2"));
			SceneLevel level = new SceneLevel(new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Background") }), null, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			SceneRainSimulation rain = new SceneRainSimulation(500, 20, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width * 2, 1), Game.Content.player);
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
			Game.Content.triggers.Add(new Trigger(null, new GameEvent(ETarget.Main, EGameEvent.Open_Level3), new Vector2(251, 25) * Game.Content.tileSize, new Vector2(1, 1) * Game.Content.tileSize, false, false, true));
			return scene;
		}

		public static Scene CreateLevel3()
		{
			// ----- Create level three -----
			// ----- Camera -----
			Game.Content.camera = new Camera2D(new Vector2(Game.Content.viewport.Width / 2.0f, Game.Content.viewport.Height / 2.0f), Vector2.Zero);
			// ----- Player -----
			Game.Content.player = new Player(new Vector2(4, 25) * Game.Content.tileSize);
			// ----- Map -----
			Game.Content.tileMap = new TileMap(Game.Content.contentManager.Load<Texture2D>("Graphics/TileMaps/Level3"));
			SceneLevel level = new SceneLevel(new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Background") }), null, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			SceneRainSimulation rain = new SceneRainSimulation(500, 20, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width * 2, 1), Game.Content.player);
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
			Game.Content.triggers.Add(new Trigger(null, new GameEvent(ETarget.Main, EGameEvent.Open_Outro), new Vector2(251, 25) * Game.Content.tileSize, new Vector2(1, 1) * Game.Content.tileSize, false, false, true));
			return scene;
		}

		public static Scene CreateOutro()
		{
			// ----- Create Outro -----
			// ----- Camera -----
			Game.Content.camera = new Camera2D(Vector2.Zero, Vector2.Zero);
			// ----- Eventlist -----
			List<GameEvent> gameevents = new List<GameEvent>();
			gameevents.Add(new GameEvent(ETarget.Scene, EGameEvent.ShowMessagebox, text: "And so he could fall into his sweet bed."));
			gameevents.Add(new GameEvent(ETarget.Scene, EGameEvent.ShowMessagebox, text: "In the next morning, there were people from the road construction company."));
			gameevents.Add(new GameEvent(ETarget.Scene, EGameEvent.ShowMessagebox, text: "But this is another story ..."));
			gameevents.Add(new GameEvent(ETarget.Scene, EGameEvent.ShowMessagebox, text: "FIN"));
			gameevents.Add(new GameEvent(ETarget.Main, EGameEvent.Open_Mainmenu));
			SceneEventbox eventbox = new SceneEventbox(gameevents);
			// ----- Music -----
			SceneMusic background_music = new SceneMusic(Game.Content.contentManager.Load<Song>("Music/backbeat-db-110bpm-01-6414"));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(eventbox);
			components.Add(background_music);
			Scene scene = new Scene(components);
			return scene;
		}
	}
}
