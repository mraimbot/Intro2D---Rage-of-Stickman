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
	class Enemy : Entity
	{
		protected enum EEnemyDirection
		{
			Left,
			Right
		}

		protected AnimatedTexture2D animation_idle;
		protected AnimatedTexture2D animation_move;
		protected AnimatedTexture2D animation_jump;
		protected AnimatedTexture2D animation_attack;

		protected SoundEffect sound_move;
		protected SoundEffect sound_jump;
		protected SoundEffect sound_attack;

		protected bool move_left;
		protected bool move_right;
		protected bool move_jump;
		protected bool move_attack;

		protected bool jumped;
		protected bool attacked;

		protected Vector2 position_start;
		protected EEnemyDirection direction;

		protected int health_max;

		protected float speed;

		protected float jump_force;
		protected Timer can_Jump;

		protected Timer can_Attack;

		protected Entity target;
		protected float range; // The Enemy is only moving to his target, if the target is in this range

		protected Timer claim_timer;
		protected bool isClaiming;
		protected List<string> claims;
		protected int claim_ID;
		protected Color claim_color;

		public Enemy(Entity target, Vector2 position, Vector2 size, float mass, float speed, int health, bool isImmortal = false)
			: base(position, size, 0, health, mass, isImmortal, true, false, false, true, true, true, true)
		{
			position_start = position;
			claim_timer = new Timer(RandomGenerator.NextInt(min: 3, max: 20));
			claims = new List<string>();
			isClaiming = false;
			claim_color = Color.White;
			this.target = target;
		}

		public void Initialize()
		{
			direction = EEnemyDirection.Left;
			position = position_start;
			health = health_max;
			speed = 0;
			jump_force = 0;
			can_Jump.Reset();
		}

		public override void Update(bool isPaused)
		{
			if (isActive)
			{
				if (!isPaused)
				{
					Logic();
				}
			}

			base.Update(isPaused);
		}

		private void Logic()
		{
			claim_timer.Update(false);

			if (claim_timer.IsTimeUp())
			{
				isClaiming = !isClaiming;
				if (isClaiming)
				{
					claim_ID = RandomGenerator.NextInt(min: 0, max: claims.Count);
					claim_timer.Reset(claims.ElementAt(claim_ID).Length / 2);
				}
				else
				{
					claim_timer.Reset(RandomGenerator.NextInt(min: 3, max: 20));
				}
			}
		}

		public override void Draw()
		{
			if (isVisible)
			{
				SpriteEffects s = SpriteEffects.None;

				if (direction == EEnemyDirection.Left)
				{
					s = SpriteEffects.FlipHorizontally;
				}
				else
				{
					s = SpriteEffects.None;
				}

				if (!isGrounded)
				{
					animation_jump.Update();
					animation_jump.Draw(position, s);
				}
				else if (move_left || move_right)
				{
					animation_move.Update();
					animation_move.Draw(position, s);
				}
				else if (move_attack)
				{
					animation_attack.Update();
					animation_attack.Draw(position, s);
				}
				else if (move_jump)
				{
					animation_jump.Update();
					animation_jump.Draw(position, s);
				}
				else
				{
					animation_idle.Update();
					animation_idle.Draw(position, s);
				}

				if (isClaiming)
				{
					ShowText.Text(new Vector2(position.X + size.X / 2, position.Y - 32), claims.ElementAt(claim_ID), claim_color, 0, 1, ETextAlign.Center);
				}
			}

			if (jumped)
			{
				sound_jump.Play(0.05f, RandomGenerator.NextFloat(min: -1, max: -0.5f), 0);
			}

			else if (attacked)
			{
				sound_attack.Play(1, RandomGenerator.NextFloat(min: -0.2f, max: 0.2f), 0);
			}
		}
	}
}
