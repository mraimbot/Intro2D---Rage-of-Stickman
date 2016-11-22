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
            Tile[,] tileMap;
            int tileSize;
		public TileMap(Texture2D[] textures, Texture2D bitMap, int _tileSize)
		{
            // TODO TileMap - TileMap() - load picture
            tileSize = _tileSize;
            tileMap = new Tile[bitMap.Width, bitMap.Height];
            BuildMap(textures, bitMap);
		}

        private void BuildMap(Texture2D[] textures, Texture2D bitMap)
        {
            Color[] colores = new Color[bitMap.Width * bitMap.Height];
            bitMap.GetData(colores);
            for(int y=0; y<tileMap.GetLength(1);y++)
            {
                for (int x = 0; x<tileMap.GetLength(0); x++)
                {
                    if (colores[y * tileMap.GetLength(0) + x] == Color.Black)
                        tileMap[x, y] = new Tile(textures[0], new Vector2(x * tileSize, y * tileSize), 1);
                    else tileMap[x, y] = new Tile(textures[1], new Vector2(x * tileSize, y * tileSize), 0);
                }
            }
        }

		public void Update(GameTime gameTime)
		{

		}

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int y =0; y < tileMap.GetLength(1); y++)
            {
                for(int x = 0; x<tileMap.GetLength(0);x++)
                {
                    tileMap[x, y].Draw(spriteBatch);
                }
            }
        }
	}
}
