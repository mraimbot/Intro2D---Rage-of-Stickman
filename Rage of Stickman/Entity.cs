using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	enum EDirection
	{
		left,
		right
	}

	class Entity : GameObject
	{
		protected AnimatedTexture2D[] animations;

		protected Vector2 startPosition;

		protected EDirection lookAtDirection;

		protected float mass;
		protected float speed;

		protected List<Vector2> forces;
		protected List<Vector2> impulses;
		protected Vector2 force_input;

		protected float minGroundDistance = Game.Content.tileSize / 10;
		protected bool isGrounded;

		protected int health;
		protected int health_max;

		protected List<int> damages;

		public Entity(Vector2 startPosition, Vector2 size, EDirection lookAtDirection, float mass, float speed, bool enableGravity, int health)
			: base()
		{
			this.startPosition = startPosition;
			this.position = this.startPosition;
			this.size = size;
			this.lookAtDirection = EDirection.right;
			this.mass = mass;
			this.speed = speed;
			this.forces = new List<Vector2>();
			this.impulses = new List<Vector2>();

			this.isGrounded = false;

			if (enableGravity)
			{
				this.forces.Add(Game.Content.force_gravity);
			}

			this.health_max = health;
			this.health = this.health_max;

			this.damages = new List<int>();
		}

		public void LoadAnimations(AnimatedTexture2D[] animationList)
		{
			this.animations = new AnimatedTexture2D[animationList.Length];
			for (int ID = 0; ID < animations.Length; ID++)
			{
				this.animations[ID] = animationList[ID];
			}

			this.size.X = (int)animations[0].Size().X;
			this.size.Y = (int)animations[0].Size().Y;
		}

		public void LoadAnimations(AnimatedTexture2D animation)
		{
			this.animations = new AnimatedTexture2D[1];
			this.animations[0] = animation;

			this.size.X = (int)animations[0].Size().X;
			this.size.Y = (int)animations[0].Size().Y;
		}

		public void addDamage(int damage)
		{
			this.damages.Add(damage);
		}

		public new void Update()
		{
			if (this.active)
			{
				this.Logic();
				this.Physic();
			}

			base.Update();
		}

		private void Logic()
		{
			int damage = 0;
			foreach (int damage_input in damages)
				damage += damage_input;

			if (health > 0)
			{
				health -= damage;
				if (health < 0) // <healt := -1> is for immortality
				{
					health = 0;
				}
			}

			if (health == 0)
			{
				this.visible = false;
				this.active = false;
			}
		}

		public bool isDead()
		{
			return (health == 0) ? true : false;
		}

		private void Physic()
		{
			if (isGrounded)
			{
				force_input.X = 0.0f;
				force_input.Y = 0.0f;
			}

			foreach (Vector2 force in forces)
				force_input += force;

			foreach (Vector2 impulse in impulses)
				force_input += impulse;

			impulses.Clear();

			if (force_input != Vector2.Zero)
			{
				Vector2 accel = force_input / mass;
				Vector2 velocity = accel * Game.Content.gameTime.ElapsedGameTime.Milliseconds;

				HandleTransformation(velocity);
			}
		}

		private void HandleTransformation(Vector2 velocity)
		{
			if (!(isGrounded && velocity.Y > 0))
			{
				if (!Game.Content.tileMap.CheckCollisionYRay(new Vector2(position.X, position.Y), new Vector2(position.X, position.Y + velocity.Y))
					&& !Game.Content.tileMap.CheckCollisionYRay(new Vector2(position.X, position.Y + size.Y), new Vector2(position.X, position.Y + size.Y + velocity.Y))
					&& !Game.Content.tileMap.CheckCollisionYRay(new Vector2(position.X + size.X, position.Y), new Vector2(position.X + size.X, position.Y + velocity.Y))
					&& !Game.Content.tileMap.CheckCollisionYRay(new Vector2(position.X + size.X, position.Y + size.Y), new Vector2(position.X + size.X, position.Y + size.Y + velocity.Y)))
				{
					position.Y += velocity.Y;
				}
				else
				{
					if (velocity.Y > 0) // falling down
					{
						isGrounded = true;
					}
				}
			}

			if (!Game.Content.tileMap.CheckCollision(new Vector2(position.X + velocity.X, position.Y))
				&& !Game.Content.tileMap.CheckCollision(new Vector2(position.X + size.X + velocity.X, position.Y))
				&& !Game.Content.tileMap.CheckCollision(new Vector2(position.X + velocity.X, position.Y + size.Y))
				&& !Game.Content.tileMap.CheckCollision(new Vector2(position.X + size.X + velocity.X, position.Y + size.Y)))
			{
				position.X += velocity.X;
			}

			//if (calcDistanceToGround() <= minGroundDistance)
			//{
			//	position.Y = position.Y + calcDistanceToGround() - minGroundDistance;
			//	isGrounded = true;
			//}
			//else
			if ((isGrounded || calcDistanceToGround() <= minGroundDistance) && velocity.Y >= 0)
			{
				position.Y = position.Y + calcDistanceToGround() - minGroundDistance;
			}

			if (calcDistanceToGround() > minGroundDistance)
			{
				isGrounded = false;
			}
			else
			{
				isGrounded = true;
			}
		}

		protected float calcDistanceToGround()
		{
			float distance = ((int)((position.Y + size.Y) / Game.Content.tileSize) + 1) * Game.Content.tileSize - (position.Y + size.Y);
			int tileBelow = (int)((position.Y + size.Y) / Game.Content.tileSize) + 1;

			while (Game.Content.tileMap.getCollisionTypeAt((int)(position.X / Game.Content.tileSize), tileBelow) == ECollision.passable
				&& Game.Content.tileMap.getCollisionTypeAt((int)((position.X + size.X) / Game.Content.tileSize), tileBelow) == ECollision.passable)
			{
				tileBelow++;
				distance += Game.Content.tileSize;
			}
			return distance;
		}

		public void Draw()
		{
			if (!(animations == null) && !(animations.Length == 0))
			{
				this.animations[0].Draw(position);
			}
		}
	}
}
