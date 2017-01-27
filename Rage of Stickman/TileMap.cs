using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Rage_of_Stickman
{
	class TileMap
	{
		private int width;
		private int height;

		private Tile[] tileMap;

		public TileMap(Texture2D bitMap)
		{
			this.BuildTileMap(bitMap);
		}

		public void BuildTileMap(Texture2D bitMap)
		{
			this.width = bitMap.Width;
			this.height = bitMap.Height;

			this.tileMap = new Tile[width * height];

			Color[] colorMap = new Color[width * height];

			bitMap.GetData(colorMap);

			for (int h = 0; h < this.height; h++)
			{
				for (int w = 0; w < this.width; w++)
				{
					if (colorMap[h * this.width + w] == Color.Green)
					{
						if (Game.Content.animations[(int)EAnimation.grass] == null)
						{
							Game.Content.textures[(int)ETexture.grass] = Game.Content.contentManager.Load<Texture2D>("Graphics/Tiles/Gras");
							Texture2D[] grass = { Game.Content.textures[(int)ETexture.grass] };
							Game.Content.animations[(int)EAnimation.grass] = new AnimatedTexture2D(grass, Game.Content.tileSize, Game.Content.tileSize, 999.0f);
						}
						tileMap[h * this.width + w] = new Tile(Game.Content.animations[(int)EAnimation.grass], ECollision.impassable, new Vector2(w * Game.Content.tileSize, h * Game.Content.tileSize), new Vector2(Game.Content.tileSize));
					}
					else if (colorMap[h * this.width + w] == Color.Gray)
					{
						if (Game.Content.animations[(int)EAnimation.stone] == null)
						{
							Game.Content.textures[(int)ETexture.stone] = Game.Content.contentManager.Load<Texture2D>("Graphics/Tiles/Stone");
							Texture2D[] stone = { Game.Content.textures[(int)ETexture.stone] };
							Game.Content.animations[(int)EAnimation.stone] = new AnimatedTexture2D(stone, Game.Content.tileSize, Game.Content.tileSize, 999.0f);
						}
						tileMap[h * this.width + w] = new Tile(Game.Content.animations[(int)EAnimation.stone], ECollision.impassable, new Vector2(w * Game.Content.tileSize, h * Game.Content.tileSize), new Vector2(Game.Content.tileSize));
					}
					else if (colorMap[h * this.width + w] == Color.Black)
					{
						if (Game.Content.animations[(int)EAnimation.asphalt] == null)
						{
							Game.Content.textures[(int)ETexture.asphalt] = Game.Content.contentManager.Load<Texture2D>("Graphics/Tiles/Asphalt");
							Texture2D[] asphalt = { Game.Content.textures[(int)ETexture.asphalt] };
							Game.Content.animations[(int)EAnimation.asphalt] = new AnimatedTexture2D(asphalt, Game.Content.tileSize, Game.Content.tileSize, 999.0f);
						}
						tileMap[h * this.width + w] = new Tile(Game.Content.animations[(int)EAnimation.asphalt], ECollision.impassable, new Vector2(w * Game.Content.tileSize, h * Game.Content.tileSize), new Vector2(Game.Content.tileSize));
					}
					else if (colorMap[h * this.width + w] == Color.Purple)
					{
						if (Game.Content.animations[(int)EAnimation.brick] == null)
						{
							Game.Content.textures[(int)ETexture.brick] = Game.Content.contentManager.Load<Texture2D>("Graphics/Tiles/Wall");
							Texture2D[] wall = { Game.Content.textures[(int)ETexture.brick] };
							Game.Content.animations[(int)EAnimation.brick] = new AnimatedTexture2D(wall, Game.Content.tileSize, Game.Content.tileSize, 999.0f);
						}
						tileMap[h * this.width + w] = new Tile(Game.Content.animations[(int)EAnimation.brick], ECollision.impassable, new Vector2(w * Game.Content.tileSize, h * Game.Content.tileSize), new Vector2(Game.Content.tileSize));
					}
					else
					{
						tileMap[h * this.width + w] = new Tile(null, ECollision.passable, new Vector2(w * Game.Content.tileSize, h * Game.Content.tileSize), new Vector2(Game.Content.tileSize));
					}
				}
			}
		}

		public Vector2 Size()
		{
			return new Vector2(width, height);
		}

		public ECollision getCollisionTypeAt(int w, int h)
		{
			return getCollisionTypeAtID(w + h * this.width);
		}

		public ECollision getCollisionTypeAtID(int ID)
		{
			if (ID < width * height && ID > 0)
			{
				return tileMap[ID].getCollisionType();
			}
			return ECollision.passable;
		}

		public bool CheckCollision(Vector2 point)
		{
			if (getCollisionTypeAt((int)(point.X / Game.Content.tileSize), (int)(point.Y / Game.Content.tileSize)) == ECollision.impassable)
			{
				return true;
			}
			return false;
		}

		public bool CheckCollisionYRay(Vector2 start, Vector2 end)
		{
			int xStartID = (int)(start.X / Game.Content.tileSize);
			int yStartID = (int)(start.Y / Game.Content.tileSize);
			int yEndID = (int)(end.Y / Game.Content.tileSize);
			
			for (int yID = yStartID; yID != yEndID; yID = (yStartID > yEndID) ? yID - 1 : yID + 1)
			{
				if (getCollisionTypeAt(xStartID, yID) == ECollision.impassable)
				{
					return true;
				}
			}

			if (getCollisionTypeAt(xStartID, yEndID) == ECollision.impassable)
			{
				return true;
			}

			return false;
		}

		public void Update()
		{
			for (int h = 0; h < this.height; h++)
			{
				for (int w = 0; w < this.width; w++)
				{
					tileMap[h * this.width + w].Update();
				}
			}
		}

		public void Draw()
		{
			for (int h = 0; h < this.height; h++)
			{
				for (int w = 0; w < this.width; w++)
				{
					tileMap[h * this.width + w].Draw();
				}
			}
		}
	}
}
