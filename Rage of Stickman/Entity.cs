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

		protected Vector2 position_start;

		protected EDirection lookAtDirection;

		protected float mass;
		protected float speed;

		protected List<Vector2> forces;
		protected List<Vector2> impulses;
		protected Vector2 force_input;

		protected Vector2 force_jump = new Vector2(0.0f, -1.5f);

		protected float minGroundDistance = 1.0f;
		protected bool isGrounded;

		protected int health;
		protected int health_start;

		protected List<int> damages;

		protected bool move_left;
		protected bool move_right;
		protected bool move_jump;
		protected bool move_attack1;
		protected bool move_attack2;

		public Entity(Vector2 position_start, Vector2 size, EDirection lookAtDirection, float mass, float speed, bool enableGravity, int health)
			: base(position_start, size)
		{
			this.position_start = position_start;
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

			this.health_start = health;
			this.health = this.health_start;

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
			animations = new AnimatedTexture2D[1];
			animations[0] = animation;

			size.X = (int)animations[0].Size().X;
			size.Y = (int)animations[0].Size().Y;
		}

		public void addDamage(int damage)
		{
			damages.Add(damage);
		}

		public new void Update()
		{
			if (active)
			{
				Logic();
				Physic();
			}

			base.Update();
		}

		private void Logic()
		{
			// ----- Damage -----
			int damage = 0;
			foreach (int damage_input in damages)
				damage += damage_input;

			// ----- Health -----
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

			// ----- Movement -----
			// TODO Entity.Logic : Add movement here!
			if (!this.isDead())
			{
				if (this.move_jump && this.isGrounded)
				{
					this.impulses.Add(force_jump);
				}

				if (this.move_left && this.isGrounded)
				{
					this.impulses.Add(new Vector2(-speed, 0.0f));
					this.lookAtDirection = EDirection.left;
				}

				if (this.move_right && this.isGrounded)
				{
					this.impulses.Add(new Vector2(speed, 0.0f));
					this.lookAtDirection = EDirection.right;
				}

				if (this.move_attack1)
				{
					// TODO Player.move_punch
				}

				if (this.move_attack2)
				{
					// TODO Player.move_kick
				}
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

			if ((isGrounded || calcDistanceToGround() <= minGroundDistance) && velocity.Y >= 0)
			{
				if (calcDistanceToGround() < Game.Content.tileSize / 2)
				{
					position.Y = position.Y + calcDistanceToGround() - minGroundDistance;
				}
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

		public new void Draw()
		{
			if (!(animations == null) && !(animations.Length == 0))
			{
				this.animations[0].Draw(position);
			}
		}
	}
}
