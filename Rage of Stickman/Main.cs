using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rage_of_Stickman
{
	public class Main : Microsoft.Xna.Framework.Game
	{
		private GraphicsDeviceManager graphics;

		private Scene scene;

		public Main()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			graphics.PreferredBackBufferWidth = 1366;
			graphics.PreferredBackBufferHeight = 768;
			// graphics.ToggleFullScreen();
			graphics.ApplyChanges();

			Game.Content.contentManager = Content;
			Game.Content.spriteBatch = new SpriteBatch(GraphicsDevice);
			Game.Content.viewport = GraphicsDevice.Viewport;

			Game.Content.sceneState = EScenes.Mainmenu;

			Game.Content.flag_newScene = true;

			base.Initialize();
		}

		protected override void LoadContent() {}
		protected override void UnloadContent(){}

		protected override void Update(GameTime gameTime)
		{
			Game.Content.gameTime = gameTime;

			InputHandler();
			SceneHandler();
			GameEventHandler();

			base.Update(gameTime);

			Game.Content.previousKeyState = Keyboard.GetState();
		}

		private void InputHandler()
		{
			// ----- Main-Inputs -----
			if (Keyboard.GetState().IsKeyDown(Keys.F4) && !Game.Content.previousKeyState.IsKeyDown(Keys.F4))
			{
				graphics.ToggleFullScreen();
				graphics.ApplyChanges();
			}
		}

		private void SceneHandler()
		{
			if (scene == null || Game.Content.flag_newScene)
			{
				switch (Game.Content.sceneState)
				{
					case EScenes.Exit:
						Exit();
						break;

					case EScenes.Mainmenu:
						scene = Scene.CreateMainmenu();
						break;

					case EScenes.Credits:
						scene = Scene.CreateCredits();
						break;

					case EScenes.Level1:
						scene = Scene.CreateLevel1();
						break;

					case EScenes.Level2:
						scene = Scene.CreateLevel2();
						break;

					case EScenes.Level3:
						scene = Scene.CreateLevel3();
						break;
				}

				Game.Content.flag_newScene = false;
			}

			scene.Update();
		}

		private void GameEventHandler()
		{
			if (Game.Content.gameEvents.Count > 0)
			{
				foreach (GameEvent gameEvent in Game.Content.gameEvents)
				{
					if (gameEvent.Target() == ETarget.Main)
					{
						switch (gameEvent.Event())
						{
							case EGameEvent.Game_Exit:
								Game.Content.sceneState = EScenes.Exit;
								Game.Content.flag_newScene = true;
								break;

							case EGameEvent.Open_Mainmenu:
								Game.Content.sceneState = EScenes.Mainmenu;
								Game.Content.flag_newScene = true;
								break;

							case EGameEvent.Open_Credits:
								Game.Content.sceneState = EScenes.Credits;
								Game.Content.flag_newScene = true;
								break;

							case EGameEvent.Open_Level1:
								Game.Content.sceneState = EScenes.Level1;
								Game.Content.flag_newScene = true;
								break;

							case EGameEvent.Open_Level2:
								Game.Content.sceneState = EScenes.Level2;
								Game.Content.flag_newScene = true;
								break;

							case EGameEvent.Open_Level3:
								Game.Content.sceneState = EScenes.Level3;
								Game.Content.flag_newScene = true;
								break;
						}
					}
				}
				Game.Content.gameEvents.Clear();
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			Game.Content.spriteBatch.Begin(transformMatrix: Game.Content.camera.GetViewMatrix());
			{
				if (scene != null)
				{
					scene.Draw();
				}
			}
			Game.Content.spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
