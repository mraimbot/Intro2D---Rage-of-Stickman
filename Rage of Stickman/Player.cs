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
		private AnimatedTexture2D animation_idle;
		private Vector2 position;

		private int width;
		private int height;

		private List<Vector2> forces;
		private List<Vector2> impulses;
		private Vector2 accel;
		private Vector2 velocity;

		private int health;
		private int speed;
		private float mass;

		private bool midair;

		private bool move_left;
		private bool move_right;
		private bool move_jump;

		private Vector2 jump_force = new Vector2(0.0f, 1.0f);

		public Player()
        {
			this.width = Game.Content.tileSize;
			this.height = Game.Content.tileSize;

			forces = new List<Vector2>();
			impulses = new List<Vector2>();

			Initialize();
        }

		public void Initialize()
		{
			forces.Clear();
			// forces.Add(Game.Content.gravity);
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
			move_left = (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)) ? true : false;
			move_right = (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D)) ? true : false;
			move_jump = (Keyboard.GetState().IsKeyDown(Keys.Space)) ? true : false;

			// for testing
			position.Y += (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W)) ? -10 : 0;
			position.Y += (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S)) ? 10 : 0;
		}

		private void Logic()
		{
			if (move_left)
			{
				impulses.Add(new Vector2(-speed, 0.0f));
			}

			if (move_right)
			{
				impulses.Add(new Vector2(speed, 0.0f));
			}

			if (move_jump)
			{
				if (!midair)
				{
					impulses.Add(-jump_force);
				}
			}
		}

		private void Physic()
		{
			Vector2 force_max = Vector2.Zero;

			foreach (Vector2 force in forces)
			{
				force_max += force;
			}
			foreach (Vector2 impulse in impulses)
			{
				force_max += impulse;
			}
			impulses.Clear();

			accel = force_max / mass;
			velocity = accel * Game.Content.gameTime.ElapsedGameTime.Milliseconds;

			Vector2 newPosition = position + velocity;

			if (newPosition.X < 0.0f)
			{
				newPosition.X = 1.0f;
			}
			else if (newPosition.X > Game.Content.tileMap.Size().X * Game.Content.tileSize - Game.Content.tileSize)
			{
				newPosition.X = Game.Content.tileMap.Size().X * Game.Content.tileSize - 1;
			}

			if (newPosition.Y < 0.0f)
			{
				newPosition.Y = 0.0f;
			}
			else if (newPosition.Y > Game.Content.tileMap.Size().Y * Game.Content.tileSize)
			{
				newPosition.Y = Game.Content.tileMap.Size().Y * Game.Content.tileSize - 1;
			}

			// collision
			// collision bestenfalls über Rectangle rect; rect.Intersects(rect2); prüfen
			int tileID = (int)(newPosition.Y / Game.Content.tileSize) * (int)(Game.Content.tileMap.Size().X) + (int)(newPosition.X / Game.Content.tileSize);
			// int tileIDX = (int)(newPosition.Y / Game.Content.tileSize) * (int)(Game.Content.tileMap.Size().X);
			// int tileIDY = (int)(newPosition.X / Game.Content.tileSize);

			switch (Game.Content.tileMap.getCollisionTypeAt(tileID))
			{
				case ECollision.passable:
					break;

				case ECollision.impassable:
					break;
			}

			position.X = newPosition.X;
			position.Y = newPosition.Y;
		}

		public void Update()
        {
			Input();
			Logic();
			Physic();
        }

        public void Draw()
        {
			// lässt sich sicher optimieren mit einem weiteren Attribut und einer Anpassung in der Logic()
			if (move_left)
			{
				animation_idle.Draw(position);
			}

			else if (move_right)
			{
				animation_idle.Draw(position);
			}

			else if (move_jump)
			{
				if (!midair)
				{
					animation_idle.Draw(position);
				}
			}
			else
			{
				animation_idle.Draw(position);
			}
		}
    }
}
