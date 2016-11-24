using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rage_of_Stickman
{
	class TileMap
	{
		private Tile[] tileMap;
		private int width;
		private int height;

		public TileMap()
		{

		}

		public void BuildTileMap(Texture2D bitMap)
		{
			this.width = bitMap.Width;
			this.height = bitMap.Height;

			if (tileMap == null)
			{
				tileMap = new Tile[width * height];
			}

			Color[] colorMap = new Color[width * height];
			bitMap.GetData(colorMap);

			for (int i = 0; i < width * height; i++)
			{
				if (colorMap[i] == Color.Black)
				{
					tileMap[i] = new Tile(Game.Content.animations[(int)EAnimation.asphalt], ECollision.impassable);
				}
				/* else if (colorMap[i] == Color.Xxxx)
				 * {
				 *		// add new colors here ...
				 * }
				 */
				else
				{
					tileMap[i] = new Tile(null, ECollision.passable);
				}
			}	
		}

		public Vector2 Size()
		{
			return new Vector2(width, height);
		}

		public ECollision getCollisionTypeAt(int ID)
		{
			return tileMap[ID].getCollisionType();
		}

		public void Update()
		{

		}

		public void Draw()
		{
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					tileMap[y * width + x].Draw(new Vector2(x * Game.Content.tileSize, y  * Game.Content.tileSize));
				}
			}
		}
	}
}
