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
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Rage_of_Stickman
{
    class Player
    {
		private AnimatedTexture2D animation_idle;
		private Vector2 position;

		private int width;
		private int height;

		private List<Vector2> forces;
		private List<Vector2> impulses;
		private Vector2 accel;
		private Vector2 velocity;

        private Rectangle localBounds;

        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(position.X)*Game.Content.tileSize + localBounds.X;
                int top = (int)Math.Round(position.Y) * Game.Content.tileSize + localBounds.Y;

                return new Rectangle(left, top, localBounds.Width, localBounds.Height);
            }
        }

        private int health;
		private int speed;
		private float mass;

		private bool midair;
        SpriteEffects s = SpriteEffects.None;
        EDirection direction = EDirection.right;

		private Vector2 jump_force = new Vector2(0.0f, -1.0f);

		public Player()
        {
			this.width = Game.Content.tileSize;
			this.height = Game.Content.tileSize;
            localBounds = new Rectangle(0, 0, Game.Content.tileSize, 2*Game.Content.tileSize);

			forces = new List<Vector2>();
			impulses = new List<Vector2>();

			Initialize();
        }

		public void Initialize()
		{
			forces.Clear();
			forces.Add(Game.Content.gravity);
			impulses.Clear();

			position = Game.Content.player_startposition;
			health = 100;
			speed = 1;
			mass = 2;
			midair = false;
		}

		public void LoadAnimations(AnimatedTexture2D animation_idle)
		{
			this.animation_idle = animation_idle;
			this.width = (int)animation_idle.Size().X;
			this.height = (int)animation_idle.Size().Y;
		}

		public Vector2 Position()
		{
			return position;
		}

		public Vector2 Size()
		{
			return new Vector2(width, height);
		}

        private void Input()
        {
            KeyboardState ks = Keyboard.GetState();
            Keys[] currentlyPressedKeys = ks.GetPressedKeys();

            foreach (Keys key in currentlyPressedKeys)
                switch (key)
                {
                    case Keys.W:
                    case Keys.Up:
                        impulses.Add(-2*Game.Content.gravity);
                        break;
                    case Keys.A:
                    case Keys.Left:
                        impulses.Add(new Vector2(-speed, 0.0f));
                        direction = EDirection.left;
                        break;
                    case Keys.D:
                    case Keys.Right:
                        impulses.Add(new Vector2(speed, 0.0f));
                        direction = EDirection.right;
                        break;
                }
        }

		private void Logic()
		{
		}

        private void HandleCollision(EDirectionAxis directionAxis)
        {
            Rectangle bounds = BoundingRectangle;

            int leftTile = bounds.Left / Game.Content.tileSize;
            int topTile = bounds.Top / Game.Content.tileSize;
            int rightTile = (int)Math.Ceiling((float)bounds.Right / Game.Content.tileSize) - 1;
            int bottomTile = (int)Math.Ceiling((float)bounds.Bottom / Game.Content.tileSize) - 1;

            for (int y = topTile; y <= bottomTile; y++)
                for (int x = leftTile; x <= rightTile; x++)
                {
                    Vector2 depth;
                    if (Game.Content.tileMap.getCollisionTypeAt(x, y) == ECollision.impassable &&
                        TileIntersectsPlayer(bounds, new Rectangle(x * Game.Content.tileSize, y * Game.Content.tileSize, Game.Content.tileSize, Game.Content.tileSize), directionAxis, out depth))
                    {
                        Console.WriteLine(position);
                        position += (depth/Game.Content.tileSize/5);
                        bounds = BoundingRectangle;
                    }
                }
        }

        public enum EDirectionAxis
        {
            Horizontal,
            Vertical
        }

        private static bool TileIntersectsPlayer(Rectangle player, Rectangle block, EDirectionAxis directionAxis,
            out Vector2 depth)
        {
            //depth = player.GetIntersectionDepth(block);
            
            depth = directionAxis == EDirectionAxis.Horizontal
                ? new Vector2(0,player.GetVerticalIntersectionDepth(block))
                : new Vector2(player.GetHorizontalIntersectionDepth(block),0);
            return depth.X != 0 || depth.Y != 0;
        }
    

    private void Physic()
		{
			Vector2 force_max = Vector2.Zero;

			foreach (Vector2 force in forces)
				force_max += force;

            foreach (Vector2 impulse in impulses)
				force_max += impulse;

            impulses.Clear();

			accel = force_max / mass;
			velocity = accel * Game.Content.gameTime.ElapsedGameTime.Milliseconds/Game.Content.tileSize;

            Vector2 newPosition;
            newPosition.X= position.X + velocity.X;
            HandleCollision(EDirectionAxis.Horizontal);
            newPosition.Y = position.Y + velocity.Y;
            HandleCollision(EDirectionAxis.Vertical);
            newPosition = Vector2.Clamp(newPosition, new Vector2(0, 0), Game.Content.tileMap.Size() * Game.Content.tileSize);

			Vector2 tilePositionVec = newPosition / Game.Content.tileSize;
            int tilePositionId = (int)(tilePositionVec.X + tilePositionVec.Y * Game.Content.tileMap.Size().X);

            //if (Game.Content.tileMap.getCollisionTypeAt(tilePositionId) == ECollision.passable)
                position = newPosition;
		}

		public void Update()
        {
			Input();
			Logic();
			Physic();
        }

        public void Draw()
        {

            if (direction == EDirection.left)
                s = SpriteEffects.FlipHorizontally;
			animation_idle.Draw(position*Game.Content.tileSize,s);
		}
    }
}
