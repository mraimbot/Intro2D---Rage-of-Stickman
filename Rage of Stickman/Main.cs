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
			Game.Content.animations[(int)EAnimation.asphalt] = new AnimatedTexture2D(asphalt, Game.Content.tileSize, Game.Content.tileSize);
			Texture2D[] stone = { Game.Content.textures[(int)ETexture.stone] };
			Game.Content.animations[(int)EAnimation.stone] = new AnimatedTexture2D(stone, Game.Content.tileSize, Game.Content.tileSize);
			Texture2D[] gras = { Game.Content.textures[(int)ETexture.gras] };
			Game.Content.animations[(int)EAnimation.gras] = new AnimatedTexture2D(gras, Game.Content.tileSize, Game.Content.tileSize);
			Texture2D[] wall = { Game.Content.textures[(int)ETexture.wall] };
			Game.Content.animations[(int)EAnimation.wall] = new AnimatedTexture2D(wall, Game.Content.tileSize, Game.Content.tileSize);
			// Player
			// Idle
			Texture2D[] player_idle = { Game.Content.textures[(int)ETexture.player_idle_0] };
			Game.Content.animations[(int)EAnimation.player_idle] = new AnimatedTexture2D(player_idle, (int)Game.Content.player.Size().X, (int)Game.Content.player.Size().Y);
			// Move
			Texture2D[] player_move = { Game.Content.textures[(int)ETexture.player_move_0], Game.Content.textures[(int)ETexture.player_move_1], Game.Content.textures[(int)ETexture.player_move_2] };
			Game.Content.animations[(int)EAnimation.player_move] = new AnimatedTexture2D(player_move, (int)Game.Content.player.Size().X, (int)Game.Content.player.Size().Y);
			// Punch
			Texture2D[] player_punch = { Game.Content.textures[(int)ETexture.player_punch_0], Game.Content.textures[(int)ETexture.player_punch_1], Game.Content.textures[(int)ETexture.player_punch_2] };
			Game.Content.animations[(int)EAnimation.player_punch] = new AnimatedTexture2D(player_punch, (int)Game.Content.player.Size().X, (int)Game.Content.player.Size().Y);
			// Kick
			Texture2D[] player_kick = { Game.Content.textures[(int)ETexture.player_kick_0], Game.Content.textures[(int)ETexture.player_kick_1], Game.Content.textures[(int)ETexture.player_kick_2], Game.Content.textures[(int)ETexture.player_kick_3] };
			Game.Content.animations[(int)EAnimation.player_kick] = new AnimatedTexture2D(player_kick, (int)Game.Content.player.Size().X, (int)Game.Content.player.Size().Y);

			AnimatedTexture2D[] animationlist = { Game.Content.animations[(int)EAnimation.player_idle], Game.Content.animations[(int)EAnimation.player_move], Game.Content.animations[(int)EAnimation.player_punch], Game.Content.animations[(int)EAnimation.player_kick] };
			Game.Content.player.LoadAnimations(animationlist);

			// Enemies
			// Kid
			Texture2D[] kid_move = { Game.Content.textures[(int)ETexture.enemy_kid_0], Game.Content.textures[(int)ETexture.enemy_kid_1], Game.Content.textures[(int)ETexture.enemy_kid_2], Game.Content.textures[(int)ETexture.enemy_kid_3] };
			Game.Content.animations[(int)EAnimation.enemie_kid_move] = new AnimatedTexture2D(kid_move, Game.Content.tileSize, Game.Content.tileSize);

			// Oma
			Texture2D[] oma_move = { Game.Content.textures[(int)ETexture.enemy_oma_0], Game.Content.textures[(int)ETexture.enemy_oma_1], Game.Content.textures[(int)ETexture.enemy_oma_2], Game.Content.textures[(int)ETexture.enemy_oma_3] };
			Game.Content.animations[(int)EAnimation.enemie_oma_move] = new AnimatedTexture2D(oma_move, Game.Content.tileSize, Game.Content.tileSize * 2);

			// Zombie
			Texture2D[] zombie_move = { Game.Content.textures[(int)ETexture.enemy_zombie_0], Game.Content.textures[(int)ETexture.enemy_zombie_1], Game.Content.textures[(int)ETexture.enemy_zombie_2] };
			Game.Content.animations[(int)EAnimation.enemie_zombie_move] = new AnimatedTexture2D(zombie_move, Game.Content.tileSize, Game.Content.tileSize * 2);

			// Others
			level.LoadBackground(Game.Content.textures[(int)ETexture.background]);
			Game.Content.tileMap.BuildTileMap(Content.Load<Texture2D>("Graphics/RageMap"));
		}

		protected override void UnloadContent(){}

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
