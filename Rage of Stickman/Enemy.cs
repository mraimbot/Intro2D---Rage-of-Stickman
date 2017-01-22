using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class Enemy : Entity
	{
		private AnimatedTexture2D animation_move;

		private Vector2 position;

		protected int width;
		protected int height;

		private bool isFlipped;

		private List<Vector2> forces;
		private List<Vector2> impulses;
		private Vector2 accel;
		private Vector2 velocity;

		protected Rectangle localBounds;

		public Rectangle BoundingRectangle
		{
			get
			{
				int left = (int)Math.Round(position.X * Game.Content.tileSize + localBounds.X);
				int top = (int)Math.Round(position.Y * Game.Content.tileSize + localBounds.Y);

				return new Rectangle(left, top, localBounds.Width, localBounds.Height);
			}
		}

		private EEnemy type;
		private int health;
		private float speed;
		private float mass;

		SpriteEffects s = SpriteEffects.None;
		EDirection direction = EDirection.right;

		public Enemy(EEnemy type, Vector2 startPosition, int HP, float Speed)
		{
			this.type = type;
			forces = new List<Vector2>();
			impulses = new List<Vector2>();

			Initialize(startPosition, HP, Speed);
		}

		public void Initialize(Vector2 startPosition, int HP, float Speed)
		{
			forces.Clear();
			forces.Add(Game.Content.gravity);
			impulses.Clear();

			position = startPosition;
			health = HP;
			speed = Speed;
			mass = 1.5f;

			isFlipped = false;
		}

		public void LoadAnimations(EEnemy type)
		{
			if (type == EEnemy.kid)
			{
				animation_move = Game.Content.animations[(int)EAnimation.enemie_kid_move];
			}
			else if (type == EEnemy.oma)
			{
				animation_move = Game.Content.animations[(int)EAnimation.enemie_oma_move];
			}
			else if (type == EEnemy.zombie)
			{
				animation_move = Game.Content.animations[(int)EAnimation.enemie_zombie_move];
			}
		}

		public Vector2 Position()
		{
			return position;
		}

		public Vector2 Size()
		{
			return new Vector2(width, height);
		}

		private void Logic()
		{
			if (animation_move != null)
			{
				if (Game.Content.player.Position().X + 0.1 < this.position.X)
				{
					animation_move.Update();
					impulses.Add(new Vector2(-speed, 0.0f));
					direction = EDirection.left;
				}
				else if (Game.Content.player.Position().X - 0.1 > this.position.X)
				{
					animation_move.Update();
					impulses.Add(new Vector2(speed, 0.0f));
					direction = EDirection.right;
				}
			}
			else
			{
				this.LoadAnimations(type);
			}
		}

		public bool IsDead()
		{
			if (health <= 0)
			{
				return true;
			}
			return false;
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
						position += (depth / Game.Content.tileSize / 5);
						bounds = BoundingRectangle;
					}
				}
		}

		public enum EDirectionAxis
		{
			Horizontal,
			Vertical
		}

		private static bool TileIntersectsPlayer(Rectangle enemy, Rectangle block, EDirectionAxis directionAxis,
			out Vector2 depth)
		{
			depth = directionAxis == EDirectionAxis.Horizontal
				? new Vector2(0, enemy.GetVerticalIntersectionDepth(block))
				: new Vector2(enemy.GetHorizontalIntersectionDepth(block), 0);
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

			animation_move.Draw(position * Game.Content.tileSize, s);
		}
	}
}
