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
		// Background
		background,

		// Tiles
		asphalt,
		stone,

		// Player
		player,

		// Enemies


		no_texture
	}

	enum EAnimation
	{
		// Tiles
		asphalt,
		stone,

		// Player
		player_idle,

		// Enemies


		no_animation
	}

	enum EFont
	{
		no_font
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

		public Vector2 gravity = new Vector2(0.0f, 1.0f);

		public Player player;
		public Vector2 player_startposition = new Vector2(0, 0);

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
