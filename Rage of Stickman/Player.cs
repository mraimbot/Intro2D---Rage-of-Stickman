using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rage_of_Stickman
{
    class Player
    {
        public Texture2D texture;
        public Vector2 position;

        private float jumpSpeed = 1.0f;

        private bool jumping;
        private bool falling;
        private float health;

        public Player(Texture2D playerTexture,Vector2 playerPosition)
        {
            texture = playerTexture;
            position = playerPosition;
            health = 100;
            jumping = false;
            falling = false;
        }


        public void Update()
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
                position.X -= 1;

            if (ks.IsKeyDown(Keys.Right))
                position.X += 1;

            if (jumping)
            {
                position.Y += jumpSpeed;
                jumpSpeed += 1;
                if (position.Y >= 300)
                {
                    position.Y = 300;
                    jumping = false;
                }
            }
            else
                if (ks.IsKeyDown(Keys.Space))
                {
                    
                    jumping = true;
                    jumpSpeed = -14;
                }
            
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture,position,Color.White);
        }
    }
}
