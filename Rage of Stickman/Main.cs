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
			graphics.ApplyChanges();

			Game.Content.contentManager = Content;
			Game.Content.spriteBatch = new SpriteBatch(GraphicsDevice);
			Game.Content.viewport = GraphicsDevice.Viewport;

			Game.Content.gameLevel = Game.Content.gameLevel_first;

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

			base.Update(gameTime);
		}

		private void InputHandler()
		{
			// ----- Main-Inputs -----
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Game.Content.sceneState = EScenes.Exit;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.F4) && !Game.Content.previousKeyState.IsKeyDown(Keys.F4))
			{
				graphics.ToggleFullScreen();
				graphics.ApplyChanges();
			}
		}

		private void SceneHandler()
		{
			switch (Game.Content.sceneState)
			{
				case EScenes.Mainmenu:
					MenuHandler();
					break;

				case EScenes.Exit:
					Exit();
					break;

				default:
					GameHandler();
					break;
			}
		}

		private void MenuHandler()
		{
			if (scene == null || Game.Content.flag_newScene)
			{
				scene = Scene.CreateMainmenu();
			}

			scene.Update();
		}

		private void GameHandler()
		{
			if (scene == null || Game.Content.flag_newScene)
			{
				switch (Game.Content.gameLevel)
				{
					case 1:
						scene = Scene.CreateLevel1();
						break;

					case 2:
						scene = Scene.CreateLevel2();
						break;

					case 3:
						scene = Scene.CreateLevel3();
						break;
				}

				Game.Content.flag_newScene = false;
			}

			scene.Update();
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
