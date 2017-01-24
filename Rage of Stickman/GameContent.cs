using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	enum ETexture
	{
		// Debug
		pixel,

		// Tiles
		asphalt,
		stone,
		gras,
		wall,

		// Player
		player_idle_0,
		player_move_0,
		player_move_1,
		player_move_2,
		player_punch_0,
		player_punch_1,
		player_punch_2,
		player_kick_0,
		player_kick_1,
		player_kick_2,
		player_kick_3,
		player_jump_0,
		player_land_0,
		player_land_1,
		player_midair_0,

		// Enemies
		// Kid
		enemy_kid_0,
		enemy_kid_1,
		enemy_kid_2,
		enemy_kid_3,
		// Oma
		enemy_oma_0,
		enemy_oma_1,
		enemy_oma_2,
		enemy_oma_3,
		// Zombie
		enemy_zombie_0,
		enemy_zombie_1,
		enemy_zombie_2,

		no_texture
	}

	enum EAnimation
	{
		// Tiles
		asphalt,
		stone,
		gras,
		wall,

		// Player
		player_idle,
		player_move,
		player_punch,
		player_kick,
		player_jump,
		player_midair,
		player_land,

		// Enemies
		enemie_kid_move,
		enemie_oma_move,
		enemie_zombie_move,

		no_animation
	}

	enum EFont
	{
		no_font
	}

	enum EEnemy
	{
		kid,
		oma,
		zombie
	}

	class Game
	{
		public Viewport viewport;
		public Camera2D camera;
		// public Camera2D camera;

		public SpriteBatch spriteBatch;

		public Texture2D[] textures;
		public AnimatedTexture2D[] animations;
		public SpriteFont[] fonts;

		public GameTime gameTime;
		public KeyboardState previousKeyState;

		public TileMap tileMap;
		public int tileSize = 32;

		public Vector2 force_gravity = new Vector2(0.0f, 0.1f);

		public Player player;

		public List<Enemy> enemies;

		private static Game content;
		public static Game Content
		{
			get
			{
				if (content == null)
				{
					content = new Game();

					content.textures = new Texture2D[(int)ETexture.no_texture];
					content.animations = new AnimatedTexture2D[(int)EAnimation.no_animation];
					content.fonts = new SpriteFont[(int)EFont.no_font];

					content.enemies = new List<Enemy>();
				}
				return content;
			}
		}
	}
}
