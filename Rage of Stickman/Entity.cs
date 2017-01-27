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

		protected bool useGravity;
		protected bool useWind;
		protected List<Vector2> impulses;
		protected Vector2 force_input;

		protected Vector2 force_jump;
		protected Timer jump_timer;

		protected float minGroundDistance = 1.0f;
		protected bool isGrounded;

		protected int health;
		protected int health_start;

		protected List<int> damages;

		protected Timer can_attack;

		protected bool move_left;
		protected bool move_right;
		protected bool move_jump;
		protected bool move_attack1;
		protected bool move_attack2;

		public Entity(Vector2 position_start, Vector2 size, EDirection lookAtDirection, float mass, float speed, bool enablePhysics, int health)
			: base(position_start, size)
		{
			this.position_start = position_start;
			this.lookAtDirection = EDirection.right;
			this.mass = mass;
			this.speed = speed;
			this.impulses = new List<Vector2>();
			jump_timer = new Timer(0.2f);

			this.isGrounded = false;

			useGravity = enablePhysics;
			useWind = enablePhysics;

			this.health_start = health;
			this.health = this.health_start;

			this.damages = new List<int>();
			can_attack = new Timer(1);
		}

		public void LoadAnimations(AnimatedTexture2D[] animationList)
		{
			animations = new AnimatedTexture2D[animationList.Length];
			for (int ID = 0; ID < animations.Length; ID++)
			{
				animations[ID] = animationList[ID];
			}

			size.X = (int)animations[0].Size().X;
			size.Y = (int)animations[0].Size().Y;
		}

		public void LoadAnimations(AnimatedTexture2D animation)
		{
			animations = new AnimatedTexture2D[1];
			animations[0] = animation;

			size.X = (int)animations[0].Size().X;
			size.Y = (int)animations[0].Size().Y;
		}

		public void Initialize()
		{
			// ----- Start settings -----
			position = position_start;
			lookAtDirection = EDirection.right;
			health = health_start;
		}

		public bool TargetInRange(Entity target, Rectangle attack_range)
		{
			return attack_range.Intersects(new Rectangle((int)target.Position().X, (int)target.Position().Y, (int)target.Size().X, (int)target.Size().Y));
		}

		public void Attack(List<Entity> targets, Rectangle attack_range, int damage, float attack_time)
		{
			can_attack.Reset(attack_time);
			foreach (Entity target in targets)
			{
				if (attack_range.Intersects(new Rectangle((int)target.Position().X, (int)target.Position().Y, (int)target.Size().X, (int)target.Size().Y)))
				{
					target.addDamage(this, damage);
				}
			}
		}

		public virtual void addDamage(Entity attacker, int damage)
		{
			damages.Add(damage);
		}

		public bool isDead()
		{
			return (health == 0) ? true : false;
		}

		public override void Update()
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

			damages.Clear();

			// ----- Health -----
			if (health_start > 0)
			{
				health -= damage;
				if (health < 0)
				{
					health = 0;
				}
			}

			if (health == 0)
			{
				// TODO Entity.Logic : Make Dead-Animation
				visible = false;
				active = false;
			}

			// ----- Movement -----
			if (!isDead())
			{
				jump_timer.Update();
				can_attack.Update();

				if (move_jump && jump_timer.IsTimeUp() && isGrounded)
				{
					impulses.Add(force_jump);
					jump_timer.Reset();
				}

				if (move_left && isGrounded)
				{
					impulses.Add(new Vector2(-speed, 0.0f));
					lookAtDirection = EDirection.left;
				}

				if (move_right && isGrounded)
				{
					impulses.Add(new Vector2(speed, 0.0f));
					lookAtDirection = EDirection.right;
				}

				if (move_attack1)
				{
					// TODO move_attack1
				}

				if (move_attack2)
				{
					// TODO move_attack2
				}
			}
		}

		private void Physic()
		{
			if (isGrounded)
			{
				force_input.X = 0.0f;
				force_input.Y = 0.0f;
			}

			// ----- Forces -----
			if (useGravity)
			{
				force_input += Game.Content.force_gravity;
			}

			if (useWind)
			{
				force_input += Game.Content.force_wind;
			}

			foreach (Vector2 impulse in impulses)
				force_input += impulse;

			impulses.Clear();

			if (force_input != Vector2.Zero)
			{
				Vector2 accel = force_input / mass;
				Vector2 velocity = accel * Game.Content.gameTime.ElapsedGameTime.Milliseconds;

				// ----- Translation -----
				HandleTransformation(velocity);
			}
		}

		private void HandleTransformation(Vector2 velocity)
		{
			// ----- Vertically -----
			if (!(isGrounded && velocity.Y > 0))
			{
				if (!Game.Content.tileMap.CheckCollisionYRay(new Vector2(position.X, position.Y), new Vector2(position.X, position.Y + velocity.Y)) // Point UpperLeft
					&& !Game.Content.tileMap.CheckCollisionYRay(new Vector2(position.X, position.Y + size.Y), new Vector2(position.X, position.Y + size.Y + velocity.Y)) // Point UpperRight
					&& !Game.Content.tileMap.CheckCollisionYRay(new Vector2(position.X + size.X, position.Y), new Vector2(position.X + size.X, position.Y + velocity.Y)) // Point BottomLeft
					&& !Game.Content.tileMap.CheckCollisionYRay(new Vector2(position.X + size.X, position.Y + size.Y), new Vector2(position.X + size.X, position.Y + size.Y + velocity.Y))) // Point BottomRight
				{
					position.Y += velocity.Y;
				}
				else
				{
					if (velocity.Y > 0) // falling down
					{
						isGrounded = true;
					}
					else if (velocity.Y < 0) // collided with something above
					{
						force_input.Y = 0;
					}
				}
			}

			// ----- Horizontally -----
			if (!Game.Content.tileMap.CheckCollision(new Vector2(position.X + velocity.X, position.Y)) // Point UpperLeft
				&& !Game.Content.tileMap.CheckCollision(new Vector2(position.X + size.X + velocity.X, position.Y)) // Point BottomLeft
				&& !Game.Content.tileMap.CheckCollision(new Vector2(position.X + velocity.X, position.Y + size.Y)) // Point UpperRight
				&& !Game.Content.tileMap.CheckCollision(new Vector2(position.X + size.X + velocity.X, position.Y + size.Y)) // point BottomRight
				&& !Game.Content.tileMap.CheckCollision(new Vector2(position.X + velocity.X, position.Y + size.Y / 2)) // Point MiddleLeft
				&& !Game.Content.tileMap.CheckCollision(new Vector2(position.X + size.X + velocity.X, position.Y + size.Y / 2))) // Point MiddleRight
			{
				position.X += velocity.X;
			}

			// ----- Position correction -----
			if ((isGrounded || calcDistanceToGround() <= minGroundDistance) && velocity.Y >= 0)
			{
				if (calcDistanceToGround() < Game.Content.tileSize)
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

		public override void Draw()
		{
			if (!(animations == null) && !(animations.Length == 0))
			{
				this.animations[0].Draw(position);
			}
		}
	}
}
