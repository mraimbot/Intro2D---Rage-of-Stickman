/*
 * File: Main.cs
 * Description: This is the main file where all important data is created and initialized.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rage_of_Stickman
{
	public class Main : Microsoft.Xna.Framework.Game
	{
		private GraphicsDeviceManager graphics;

		private Level level;

		public Main()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			// graphics.PreferredBackBufferWidth = 1366;
			// graphics.PreferredBackBufferHeight = 768;
			// graphics.ApplyChanges();

			Game.Content.viewport = GraphicsDevice.Viewport;
			Game.Content.camera = new Camera2D(GraphicsDevice.Viewport);

			level = new Level();

			Game.Content.tileMap = new TileMap();
			Game.Content.player = new Player();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			Game.Content.spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO Main.cs - LoadContent() : Load fonts here.
			// Fonts
			// Example: Game.Content.fonts[(int)EFont.no_font] = Content.Load<SpriteFont>("consolas");

			// Textures
			// Backgrounds
			Game.Content.textures[(int)ETexture.background] = Content.Load<Texture2D>("Graphics/Background");
			// Tiles
			Game.Content.textures[(int)ETexture.asphalt] = Content.Load<Texture2D>("Graphics/Tiles/Asphalt");
			Game.Content.textures[(int)ETexture.stone] = Content.Load<Texture2D>("Graphics/Tiles/Stone");
			Game.Content.textures[(int)ETexture.gras] = Content.Load<Texture2D>("Graphics/Tiles/Gras");
			Game.Content.textures[(int)ETexture.wall] = Content.Load<Texture2D>("Graphics/Tiles/Wall");
			// Player
			Game.Content.textures[(int)ETexture.player] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Stickman01");
			// Enemies
			// here ...			

			// Animation
			// Tiles
			Texture2D[] asphalt = { Game.Content.textures[(int)ETexture.asphalt] };
			Game.Content.animations[(int)EAnimation.asphalt] = new AnimatedTexture2D(asphalt, Game.Content.tileSize, Game.Content.tileSize);
			Texture2D[] stone = { Game.Content.textures[(int)ETexture.stone] };
			Game.Content.animations[(int)EAnimation.stone] = new AnimatedTexture2D(stone, Game.Content.tileSize, Game.Content.tileSize);
			Texture2D[] gras = { Game.Content.textures[(int)ETexture.gras] };
			Game.Content.animations[(int)EAnimation.gras] = new AnimatedTexture2D(gras, Game.Content.tileSize, Game.Content.tileSize);
			Texture2D[] wall = { Game.Content.textures[(int)ETexture.wall] };
			Game.Content.animations[(int)EAnimation.wall] = new AnimatedTexture2D(wall, Game.Content.tileSize, Game.Content.tileSize);
			// Player
			// Idle
			Texture2D[] player_idle = { Game.Content.textures[(int)ETexture.player] };
			Game.Content.animations[(int)EAnimation.player_idle] = new AnimatedTexture2D(player_idle, (int)Game.Content.player.Size().X, (int)Game.Content.player.Size().Y);

			level.LoadBackground(Game.Content.textures[(int)ETexture.background]);
			Game.Content.player.LoadAnimations(Game.Content.animations[(int)EAnimation.player_idle]);

			Game.Content.tileMap.BuildTileMap(Content.Load<Texture2D>("Graphics/RageMap"));
		}

		protected override void UnloadContent(){}

		private void Input()
		{
			// TODO Main.cs - Update() : Delete Exit() through Escape-Key.

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

		protected void UpdateCamera()
		{
			Game.Content.camera.position = Game.Content.player.Position()*Game.Content.tileSize;

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

		protected override void Update(GameTime gameTime)
		{
			Game.Content.gameTime = gameTime;

			Input();
			level.Update();
			UpdateCamera();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

            Game.Content.spriteBatch.Begin(transformMatrix: Game.Content.camera.GetViewMatrix());
			{
				level.Draw();
			}
			Game.Content.spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
