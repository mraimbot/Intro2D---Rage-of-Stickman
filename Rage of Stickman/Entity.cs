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

		protected float minGroundDistance = 0.1f;
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

				if (!isGrounded && calcDistanceToGround(new Vector2(position.X + size.X / 2, position.Y + size.Y)) <= minGroundDistance)
				{
					position.Y = ((int)(position.Y / Game.Content.tileSize) + 1) * Game.Content.tileSize - minGroundDistance;
					isGrounded = true;
				}
				else if (calcDistanceToGround(new Vector2(position.X + size.X / 2, position.Y + size.Y)) > minGroundDistance)
				{
					isGrounded = false;
				}
			}
		}

		private void HandleTransformation(Vector2 velocity)
		{
			if (!Game.Content.tileMap.CheckCollisionYRay(new Vector2(position.X + size.X / 2, position.Y + size.Y), new Vector2(position.X + size.X / 2, position.Y + velocity.Y + size.Y)) && !Game.Content.tileMap.CheckCollisionYRay(new Vector2(position.X + size.X / 2, position.Y), new Vector2(position.X + size.X / 2, position.Y + velocity.Y)))
			{
				position.Y += velocity.Y;
			}
			else
			{
				if (position.Y <= position.Y + velocity.Y) // falling down
				{
					position.Y = ((int)(position.Y / Game.Content.tileSize) + (int)calcDistanceToGround(new Vector2(position.X + size.X / 2, position.Y + size.Y)) + 1) * Game.Content.tileSize - minGroundDistance;
					isGrounded = true;
				}
				else
				{
					isGrounded = false;
				}
			}

			if (!Game.Content.tileMap.CheckCollision(new Vector2(position.X + velocity.X + size.X, position.Y)) && !Game.Content.tileMap.CheckCollision(new Vector2(position.X + velocity.X, position.Y)))
			{
				position.X += velocity.X;
			}
		}

		protected float calcDistanceToGround(Vector2 point)
		{
			float distance = point.Y - (int)point.Y;
			int tileBelow = (int)(point.Y / Game.Content.tileSize) + 1;

			while (Game.Content.tileMap.getCollisionTypeAt((int)((point.X + size.X / 2) / Game.Content.tileSize), tileBelow) == ECollision.passable)
			{
				tileBelow++;
				distance += Game.Content.tileSize;
			}
			return distance / Game.Content.tileSize;
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
