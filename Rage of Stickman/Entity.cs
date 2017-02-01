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
		protected bool useFallDamage;

		protected List<Vector2> impulses;
		protected Vector2 force_input;

		protected bool isGrounded;

		protected List<int> damages;

		public Entity(Vector2 position, Vector2 size, float rotation, int health = 1, float mass = 1, bool isImmortal = false, bool useGravity = false, bool useWind = false, bool useFallDamage = false, bool isActive = true, bool isVisible = true)
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

		// TODO Entity.TargetInRange() : copy to another class
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

		public virtual void Force(Vector2 force)
		{
			impulses.Add(force);
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

			// TODO Entity.Logic().Movement : copy to another class
			// ----- Movement -----
			//if (!isDead())
			//{
			//	jump_timer.Update(isPaused);
			//	can_attack.Update(isPaused);

			//	if (move_jump && jump_timer.IsTimeUp() && isGrounded)
			//	{
			//		impulses.Add(force_jump);
			//		jump_timer.Reset();
			//	}


			//	if (move_left && isGrounded)
			//	{
			//		impulses.Add(new Vector2(move_speed, 0.0f));
			//		lookAtDirection = EDirection.left;
			//	}

			//	if (move_right && isGrounded)
			//	{
			//		impulses.Add(new Vector2(move_speed, 0.0f));
			//		lookAtDirection = EDirection.right;
			//	}

			//	if (move_attack1)
			//	{
			//		// TODO move_attack1
			//	}

			//	if (move_attack2)
			//	{
			//		// TODO move_attack2
			//	}
			//}

			if (useDynamicSize)
			{
				size.X = animation.Size().X;
				size.Y = animation.Size().Y;
			}
		}

		private void Physic()
		{
			if (isGrounded)
			{
				// ----- simple friction -----
				if (force_input.X != 0)
				{
					force_input.X -= (force_input.X * 0.25f);
					if (force_input.X < 0.1f && force_input.X > -0.1f)
					{
						force_input.X = 0;
					}
				}

				if (force_input.Y > 0)
				{
					force_input.Y = 0;
				}
			}

			// ----- forces -----
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
				HandleTranslation(velocity);
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
					if (velocity.Y > 0) // collided with somthing below
					{
						isGrounded = true;

						// ----- fall damage -----
						if (!isImmortal && velocity.Y > 20)
						{
							Damage((int)(velocity.Y * 0.5f));
						}
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
			else
			{
				force_input.X = 0; // collided with something left or right
			}

			// ----- Position correction -----
			float distanceToGround = calcDistanceToGround();
			if ((isGrounded || distanceToGround <= Game.Content.minGroundDistance) && velocity.Y >= 0)
			{
				position.Y = position.Y + distanceToGround - Game.Content.minGroundDistance;
				isGrounded = true;
			}
			else
			{
				isGrounded = false;
			}

			// TODO Entity.Physics() : Do I need this?
			//if (distanceToGround > Game.Content.minGroundDistance)
			//{
			//	isGrounded = false;
			//}
			//else
			//{
			//	isGrounded = true;
			//}
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
