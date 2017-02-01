using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class Entity : GameObject
	{
		protected AnimatedTexture2D animation;
		protected bool useDynamicSize;

		protected int health;
		protected float mass;

		protected bool isImmortal;

		protected bool useGravity;
		protected bool useWind;
		protected bool useBouncing;
		protected bool useFriction;
		protected bool useFallDamage;

		protected bool hit_up;
		protected bool hit_down;
		protected bool hit_left;
		protected bool hit_right;

		protected List<Vector2> impulses;
		protected Vector2 force;

		protected bool isGrounded;

		protected List<int> damages;
		protected bool gotFallDamage;

		public Entity(Vector2 position, Vector2 size, float rotation, int health = 1, float mass = 1, bool isImmortal = false, bool useGravity = false, bool useWind = false, bool useBouncing = false, bool useFriction = false, bool useFallDamage = false, bool isActive = true, bool isVisible = true)
			: base(position, size, rotation, Color.Pink, isActive, isVisible)
		{
			useDynamicSize = false;

			if (health < 0)
			{
				health = 0;
			}
			this.health = health;

			if (mass <= 0)
			{
				mass = 1;
			}
			this.mass = mass;

			this.isImmortal = isImmortal;

			this.useGravity = useGravity;
			this.useWind = useWind;
			this.useBouncing = useBouncing;
			this.useFriction = useFriction;
			this.useFallDamage = useFallDamage;

			this.impulses = new List<Vector2>();

			isGrounded = false;

			this.damages = new List<int>();
		}

		public void LoadAnimations(AnimatedTexture2D animation, bool useDynamicSize)
		{
			if (animation != null)
			{
				this.animation = animation;
				this.useDynamicSize = useDynamicSize;
			}
		}

		// TODO Entity.TargetInRange() : copy to another class (enemy-class)
		//public bool TargetInRange(Entity target, Rectangle attack_range)
		//{
		//	return attack_range.Intersects(new Rectangle((int)target.Position().X, (int)target.Position().Y, (int)target.Size().X, (int)target.Size().Y));
		//}

		public bool isDead()
		{
			if (isImmortal)
			{
				return false;
			}
			return (health == 0) ? true : false;
		}

		public virtual void Impulse(Vector2 impulse)
		{
			impulses.Add(impulse);
		}

		public virtual void Damage(int damage)
		{
			damages.Add(damage);
		}

		public override void Update(bool isPaused)
		{
			if (isActive)
			{
				if (!isPaused)
				{
					Logic();
					Physic();
				}
			}

			base.Update(isPaused);
		}

		private void Logic()
		{
			// ----- Damage -----
			int damage_taken = 0;
			foreach (int damage in damages)
				damage_taken += damage;

			damages.Clear();

			// ----- Health -----
			if (!isImmortal)
			{
				health -= damage_taken;
				if (health < 0)
				{
					health = 0;
				}
			}

			if (useDynamicSize)
			{
				size.X = animation.Size().X;
				size.Y = animation.Size().Y;
			}
		}

		private void Physic()
		{
			hit_up = false;
			hit_down = false;
			hit_left = false;
			hit_right = false;

			gotFallDamage = false;

			// ----- simple friction -----
			if (useFriction)
			{
				if (isGrounded)
				{
					if (force.X != 0)
					{
						force.X -= (force.X * mass * 0.025f);
						if (force.X < 0.1f && force.X > -0.1f)
						{
							force.X = 0;
						}
					}

					if (force.Y > 0)
					{
						force.Y *= 0.5f;
						if (force.Y < 1)
						{
							force.Y = 0;
						}
					}
				}
				else
				{
					if (force.X != 0)
					{
						force.X -= (force.X * mass * 0.025f);
						if (force.X < 0.1f && force.X > -0.1f)
						{
							force.X = 0;
						}
					}
				}
			}

			// ----- forces & impulses -----
			if (useGravity)
			{
				force += (Game.Content.force_gravity * Game.Content.gameTime.ElapsedGameTime.Milliseconds * Game.Content.timeScale);
			}

			if (useWind)
			{
				force += (Game.Content.force_wind * Game.Content.gameTime.ElapsedGameTime.Milliseconds * Game.Content.timeScale);
			}

			foreach (Vector2 impulse in impulses)
				force += impulse / mass;

			impulses.Clear();

			// ----- Translation -----
			if (force != Vector2.Zero)
			{
				HandleTranslation(force);
			}
		}

		private void HandleTranslation(Vector2 velocity)
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
					if (velocity.Y < 0) // collided with something above
					{
						hit_up = true;
					}
					force.Y = 0;
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
			else
			{
				if (velocity.X < 0) // collided with something left
				{
					hit_left = true;
				}
				else if (velocity.X > 0) // collided with something right
				{
					hit_right = true;
				}
			}

			// ----- Position correction -----
			float distanceToGround = calcDistanceToGround();
			if ((distanceToGround <= Game.Content.minGroundDistance + velocity.Y) && velocity.Y >= 0)
			{
				position.Y = position.Y + distanceToGround - Game.Content.minGroundDistance;
				isGrounded = true;

				if (velocity.Y > 0) // collided with somthing below
				{
					hit_down = true;
					isGrounded = true;

					// ----- fall damage -----
					if (useFallDamage)
					{
						if (!isImmortal && force.Y > 10)
						{
							Damage((int)(force.Y * 0.75f));
							gotFallDamage = true;
						}
					}
				}
			}
			else
			{
				isGrounded = false;
			}

			// ----- simple bouncing -----
			if (useBouncing)
			{
				if ((hit_up || hit_down) && force.Y != 0)
				{
					impulses.Add(new Vector2(0, -force.Y * 3.5f));
					isGrounded = false;
				}

				if (hit_left)
				{
					hit_left = true;
				}

				if ((hit_left || hit_right) && force.X != 0)
				{
					impulses.Add(new Vector2(-force.X * 7, 0));
				}
			}
		}

		protected float calcDistanceToGround()
		{
			// TODO Entity.calcDistanceToGround() : Make better algorithm
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
			if (isVisible)
			{
				if (animation != null)
				{
					animation.Draw(position);
				}
				else
				{
					base.Draw();
				}
			}
		}
	}
}
