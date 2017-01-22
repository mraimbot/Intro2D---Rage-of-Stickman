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
		private AnimatedTexture2D animation_move;
		private AnimatedTexture2D animation_punch;
		private AnimatedTexture2D animation_kick;

		private Vector2 position;

		private int width;
		private int height;

		private bool move_left;
		private bool move_right;
		private bool move_punch;
		private bool move_kick;
		private bool move_jump;
		private bool isFlipped;

		private List<Vector2> forces;
		private List<Vector2> impulses;
		private Vector2 accel;
		private Vector2 velocity;

        private Rectangle localBounds;

        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(position.X*Game.Content.tileSize + localBounds.X);
                int top = (int)Math.Round(position.Y * Game.Content.tileSize + localBounds.Y);

                return new Rectangle(left, top, localBounds.Width, localBounds.Height);
            }
        }

        private int health;
		private float speed;
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
			speed = 0.7f;
			mass = 1.5f;
			midair = false;

			isFlipped = false;
		}

		public void LoadAnimations(AnimatedTexture2D[] animationList)
		{
			this.animation_idle = animationList[0];
			this.width = (int)animation_idle.Size().X;
			this.height = (int)animation_idle.Size().Y;

			this.animation_move = animationList[1];
			this.animation_punch = animationList[2];
			this.animation_kick = animationList[3];
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

			move_jump = false;
			move_left = false;
			move_right = false;
			move_punch = false;
			move_kick = false;

            foreach (Keys key in currentlyPressedKeys)
                switch (key)
                {
                    case Keys.W:
                    case Keys.Up:
					case Keys.Space:
						move_jump = true;
                        break;
                    case Keys.A:
                    case Keys.Left:
						move_left = true;                        
                        break;
                    case Keys.D:
                    case Keys.Right:
						move_right = true;
                        break;
					case Keys.E:
					case Keys.Y:
						move_punch = true;
						break;
					case Keys.F:
					case Keys.X:
						move_kick = true;
						break;
                }
        }

		private void Logic()
		{
			if (move_jump && !midair)
			{
				animation_kick.Update();
				impulses.Add(-2 * Game.Content.gravity);
				// midair = true;
			}

			if (move_left)
			{
				animation_move.Update();
				impulses.Add(new Vector2(-speed, 0.0f));
				direction = EDirection.left;
			}
			
			if (move_right)
			{
				animation_move.Update();
				impulses.Add(new Vector2(speed, 0.0f));
				direction = EDirection.right;
			}

			if (move_punch)
			{
				animation_punch.Update();
				// TODO move_punch
			}

			if (move_kick)
			{
				animation_kick.Update();
				// TODO move_kick
			}
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
            velocity = accel * Game.Content.gameTime.ElapsedGameTime.Milliseconds / Game.Content.tileSize;

            position += velocity;
            HandleCollision(EDirectionAxis.Horizontal);
            HandleCollision(EDirectionAxis.Vertical);
            position = Vector2.Clamp(position, new Vector2(0, 0), Game.Content.tileMap.Size() * Game.Content.tileSize);

            Vector2 tilePositionVec = position / Game.Content.tileSize;
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
			{
				isFlipped = true;
				s = SpriteEffects.FlipHorizontally;
			}
			else if (isFlipped)
			{
				isFlipped = false;
				s = SpriteEffects.None;
			}

			if (move_left || move_right)
			{
				animation_move.Draw(position * Game.Content.tileSize, s);
			}
			else if (move_punch)
			{
				animation_punch.Draw(position * Game.Content.tileSize, s);
			}
			else if (move_kick)
			{
				animation_kick.Draw(position * Game.Content.tileSize, s);
			}
			else if (move_jump)
			{
				animation_kick.Draw(position * Game.Content.tileSize, s);
			}
			else
			{
				animation_idle.Draw(position * Game.Content.tileSize, s);
			}
		}
    }
}
