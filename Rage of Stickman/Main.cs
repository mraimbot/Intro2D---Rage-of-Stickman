using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rage_of_Stickman
{
	public class Main : Microsoft.Xna.Framework.Game
	{
		private GraphicsDeviceManager graphics;

		private bool texturesLoaded;

		private int level;
		private Level level1;

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

			texturesLoaded = false;

			Game.Content.viewport = GraphicsDevice.Viewport;
			Game.Content.camera = new Camera2D(GraphicsDevice.Viewport);

			level = 1;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			Game.Content.spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO Main.cs - LoadContent() : Load fonts here.
			// Fonts
			// Example: Game.Content.fonts[(int)EFont.no_font] = Content.Load<SpriteFont>("consolas");

			// Textures
			// Debug
			Game.Content.textures[(int)ETexture.pixel] = Content.Load<Texture2D>("Graphics/Pixel");
			// Tiles
			Game.Content.textures[(int)ETexture.asphalt] = Content.Load<Texture2D>("Graphics/Tiles/Asphalt");
			Game.Content.textures[(int)ETexture.stone] = Content.Load<Texture2D>("Graphics/Tiles/Stone");
			Game.Content.textures[(int)ETexture.gras] = Content.Load<Texture2D>("Graphics/Tiles/Gras");
			Game.Content.textures[(int)ETexture.wall] = Content.Load<Texture2D>("Graphics/Tiles/Wall");
			// Player
			Game.Content.textures[(int)ETexture.player_idle_0] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Stehen");
			Game.Content.textures[(int)ETexture.player_move_0] = Content.Load<Texture2D>("Graphics/PlayerAnimation/PlayerLauf/Player_Move_0");
			Game.Content.textures[(int)ETexture.player_move_1] = Content.Load<Texture2D>("Graphics/PlayerAnimation/PlayerLauf/Player_Move_1");
			Game.Content.textures[(int)ETexture.player_move_2] = Content.Load<Texture2D>("Graphics/PlayerAnimation/PlayerLauf/Player_Move_2");
			Game.Content.textures[(int)ETexture.player_punch_0] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Schlag/Schlag0");
			Game.Content.textures[(int)ETexture.player_punch_1] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Schlag/Schlag1");
			Game.Content.textures[(int)ETexture.player_punch_2] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Schlag/Schlag2");
			Game.Content.textures[(int)ETexture.player_kick_0] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick0");
			Game.Content.textures[(int)ETexture.player_kick_1] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick1");
			Game.Content.textures[(int)ETexture.player_kick_2] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick2");
			Game.Content.textures[(int)ETexture.player_kick_3] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick3");
			Game.Content.textures[(int)ETexture.player_jump_0] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/Sprung0");
			Game.Content.textures[(int)ETexture.player_land_0] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/SprungLanding0");
			Game.Content.textures[(int)ETexture.player_land_1] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/SprungLanding1");
			Game.Content.textures[(int)ETexture.player_midair_0] = Content.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/SprungMidair");

			// Enemies
			// Kid
			Game.Content.textures[(int)ETexture.enemy_kid_0] = Content.Load<Texture2D>("Graphics/Enemies/Kid/Kid1");
			Game.Content.textures[(int)ETexture.enemy_kid_1] = Content.Load<Texture2D>("Graphics/Enemies/Kid/Kid2");
			Game.Content.textures[(int)ETexture.enemy_kid_2] = Content.Load<Texture2D>("Graphics/Enemies/Kid/Kid3");
			Game.Content.textures[(int)ETexture.enemy_kid_3] = Content.Load<Texture2D>("Graphics/Enemies/Kid/Kid4");
			// Oma
			Game.Content.textures[(int)ETexture.enemy_oma_0] = Content.Load<Texture2D>("Graphics/Enemies/Oma/Oma0");
			Game.Content.textures[(int)ETexture.enemy_oma_1] = Content.Load<Texture2D>("Graphics/Enemies/Oma/Oma1");
			Game.Content.textures[(int)ETexture.enemy_oma_2] = Content.Load<Texture2D>("Graphics/Enemies/Oma/Oma2");
			Game.Content.textures[(int)ETexture.enemy_oma_3] = Content.Load<Texture2D>("Graphics/Enemies/Oma/Oma3");
			// Zombie
			Game.Content.textures[(int)ETexture.enemy_zombie_0] = Content.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie0");
			Game.Content.textures[(int)ETexture.enemy_zombie_1] = Content.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie1");
			Game.Content.textures[(int)ETexture.enemy_zombie_2] = Content.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie2");

			// Animation
			// Tiles
			Texture2D[] asphalt = { Game.Content.textures[(int)ETexture.asphalt] };
			Game.Content.animations[(int)EAnimation.asphalt] = new AnimatedTexture2D(asphalt, Game.Content.tileSize, Game.Content.tileSize, 999.0f);
			Texture2D[] stone = { Game.Content.textures[(int)ETexture.stone] };
			Game.Content.animations[(int)EAnimation.stone] = new AnimatedTexture2D(stone, Game.Content.tileSize, Game.Content.tileSize, 999.0f);
			Texture2D[] gras = { Game.Content.textures[(int)ETexture.gras] };
			Game.Content.animations[(int)EAnimation.gras] = new AnimatedTexture2D(gras, Game.Content.tileSize, Game.Content.tileSize, 999.0f);
			Texture2D[] wall = { Game.Content.textures[(int)ETexture.wall] };
			Game.Content.animations[(int)EAnimation.wall] = new AnimatedTexture2D(wall, Game.Content.tileSize, Game.Content.tileSize, 999.0f);
			// Player
			// Idle
			Texture2D[] player_idle = { Game.Content.textures[(int)ETexture.player_idle_0] };
			Game.Content.animations[(int)EAnimation.player_idle] = new AnimatedTexture2D(player_idle, Game.Content.textures[(int)ETexture.player_idle_0].Width, Game.Content.textures[(int)ETexture.player_idle_0].Height, 100.0f);
			// Move
			Texture2D[] player_move = { Game.Content.textures[(int)ETexture.player_move_0], Game.Content.textures[(int)ETexture.player_move_1], Game.Content.textures[(int)ETexture.player_move_2] };
			Game.Content.animations[(int)EAnimation.player_move] = new AnimatedTexture2D(player_move, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);
			// Punch
			Texture2D[] player_punch = { Game.Content.textures[(int)ETexture.player_punch_0], Game.Content.textures[(int)ETexture.player_punch_1], Game.Content.textures[(int)ETexture.player_punch_2] };
			Game.Content.animations[(int)EAnimation.player_punch] = new AnimatedTexture2D(player_punch, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);
			// Kick
			Texture2D[] player_kick = { Game.Content.textures[(int)ETexture.player_kick_0], Game.Content.textures[(int)ETexture.player_kick_1], Game.Content.textures[(int)ETexture.player_kick_2], Game.Content.textures[(int)ETexture.player_kick_3] };
			Game.Content.animations[(int)EAnimation.player_kick] = new AnimatedTexture2D(player_kick, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);
			// jumping
			Texture2D[] player_jump = { Game.Content.textures[(int)ETexture.player_jump_0] };
			Game.Content.animations[(int)EAnimation.player_jump] = new AnimatedTexture2D(player_jump, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);
			// midair
			Texture2D[] player_midair = { Game.Content.textures[(int)ETexture.player_midair_0] };
			Game.Content.animations[(int)EAnimation.player_midair] = new AnimatedTexture2D(player_midair, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);
			// landing
			Texture2D[] player_land = { Game.Content.textures[(int)ETexture.player_land_0], Game.Content.textures[(int)ETexture.player_land_1] };
			Game.Content.animations[(int)EAnimation.player_land] = new AnimatedTexture2D(player_land, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);

			// Enemies
			// Kid
			Texture2D[] kid_move = { Game.Content.textures[(int)ETexture.enemy_kid_0], Game.Content.textures[(int)ETexture.enemy_kid_1], Game.Content.textures[(int)ETexture.enemy_kid_2], Game.Content.textures[(int)ETexture.enemy_kid_3] };
			Game.Content.animations[(int)EAnimation.enemie_kid_move] = new AnimatedTexture2D(kid_move, Game.Content.textures[(int)ETexture.enemy_kid_0].Width, Game.Content.textures[(int)ETexture.enemy_kid_0].Height, 100.0f);

			// Oma
			Texture2D[] oma_move = { Game.Content.textures[(int)ETexture.enemy_oma_0], Game.Content.textures[(int)ETexture.enemy_oma_1], Game.Content.textures[(int)ETexture.enemy_oma_2], Game.Content.textures[(int)ETexture.enemy_oma_3] };
			Game.Content.animations[(int)EAnimation.enemie_oma_move] = new AnimatedTexture2D(oma_move, Game.Content.textures[(int)ETexture.enemy_oma_0].Width, Game.Content.textures[(int)ETexture.enemy_oma_0].Height, 300.0f);

			// Zombie
			Texture2D[] zombie_move = { Game.Content.textures[(int)ETexture.enemy_zombie_0], Game.Content.textures[(int)ETexture.enemy_zombie_1], Game.Content.textures[(int)ETexture.enemy_zombie_2] };
			Game.Content.animations[(int)EAnimation.enemie_zombie_move] = new AnimatedTexture2D(zombie_move, Game.Content.textures[(int)ETexture.enemy_zombie_0].Width, Game.Content.textures[(int)ETexture.enemy_zombie_0].Height, 200.0f);

			texturesLoaded = true;
		}

		protected override void UnloadContent(){}

		protected override void Update(GameTime gameTime)
		{
			if (texturesLoaded)
			{
				Game.Content.gameTime = gameTime;

				Input();

				switch (level)
				{
					case 1:
						if (level1 == null)
						{
							level1 = new Level(Content.Load<Texture2D>("Graphics/RageMap"), Content.Load<Texture2D>("Graphics/Background"), new Vector2(9, 20), new Vector2(248, 27));
						}
						level1.Update();
						break;
				}

				UpdateCamera();
			}

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
				if (this.texturesLoaded)
				{
					level1.Draw();
				}
			}
			Game.Content.spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
