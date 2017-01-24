using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rage_of_Stickman
{
	public class Main : Microsoft.Xna.Framework.Game
	{
		private GraphicsDeviceManager graphics;

		private int gameLevel;
		private Level[] gameLevels;

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
			Game.Content.camera = new Camera2D(GraphicsDevice.Viewport);

			gameLevel = Game.Content.gameLevel_first;
			gameLevels = new Level[Game.Content.gameLevel_max];

			base.Initialize();
		}

		protected override void LoadContent() {}
		protected override void UnloadContent(){}

		protected override void Update(GameTime gameTime)
		{
			Game.Content.gameTime = gameTime; // TODO Main.Update => Do I need this?

			Input();
			UpdateLevel();
			UpdateCamera();

			base.Update(gameTime);
		}

		private void Input()
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			if (Keyboard.GetState().IsKeyDown(Keys.F4) && !Game.Content.previousKeyState.IsKeyDown(Keys.F4))
			{
				graphics.ToggleFullScreen();
				graphics.ApplyChanges();
			}
		}

		private void UpdateLevel()
		{
			if (gameLevels[gameLevel] == null)
			{
				gameLevels[gameLevel] = new Level();
				switch (gameLevel)
				{
					case 0:
						gameLevels[gameLevel].InitializeLevel0(Content.Load<Texture2D>("Graphics/RageMap"), Content.Load<Texture2D>("Graphics/Background"), new Vector2(9, 20), new Vector2(248, 27));
						break;
				}
			}
			gameLevels[gameLevel].Update();
		}

		protected void UpdateCamera()
		{
			Game.Content.camera.position = Game.Content.player.Position();

			if (Game.Content.camera.position.X - Game.Content.camera.origin.X < 0)
			{
				Game.Content.camera.position.X = Game.Content.camera.origin.X;
			}
			else if (Game.Content.camera.position.X + Game.Content.camera.origin.X > Game.Content.tileMap.Size().X * Game.Content.tileSize)
			{
				Game.Content.camera.position.X = Game.Content.tileMap.Size().X * Game.Content.tileSize - Game.Content.camera.origin.X;
			}

			if (Game.Content.camera.position.Y - Game.Content.camera.origin.Y < 0)
			{
				Game.Content.camera.position.Y = Game.Content.camera.origin.Y;
			}
			else if (Game.Content.camera.position.Y + Game.Content.camera.origin.Y > Game.Content.tileMap.Size().Y * Game.Content.tileSize)
			{
				Game.Content.camera.position.Y = Game.Content.tileMap.Size().Y * Game.Content.tileSize - Game.Content.camera.origin.Y;
			}
            
			Game.Content.previousKeyState = Keyboard.GetState();
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

            Game.Content.spriteBatch.Begin(transformMatrix: Game.Content.camera.GetViewMatrix());
			{
				gameLevels[gameLevel].Draw();
			}
			Game.Content.spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
