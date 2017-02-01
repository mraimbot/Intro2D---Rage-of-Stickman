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
	class Zombie : Enemy
	{
		public Zombie(Vector2 position)
			: base(Game.Content.player, position, Vector2.One, 70, 1, 20)
		{
			// ----- Load Textures & Animations -----
			if (Game.Content.animations[(int)EAnimation.enemie_zombie_move] == null)
			{
				Game.Content.textures[(int)ETexture.enemy_zombie_move_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie0");
				Game.Content.textures[(int)ETexture.enemy_zombie_move_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie1");
				Game.Content.textures[(int)ETexture.enemy_zombie_move_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie2");

				Texture2D[] zombie_move = { Game.Content.textures[(int)ETexture.enemy_zombie_move_0], Game.Content.textures[(int)ETexture.enemy_zombie_move_1], Game.Content.textures[(int)ETexture.enemy_zombie_move_2] };

				Game.Content.animations[(int)EAnimation.enemie_zombie_move] = new AnimatedTexture2D(zombie_move, 1000);
			}

			// TODO Zombie.Zombie() : load animations
			animation_idle = Game.Content.animations[(int)EAnimation.enemie_zombie_move];
			animation_move = Game.Content.animations[(int)EAnimation.enemie_zombie_move];
			animation_jump = Game.Content.animations[(int)EAnimation.enemie_zombie_move];
			animation_attack = Game.Content.animations[(int)EAnimation.enemie_zombie_move];

			// ----- Load Soundeffects -----
			sound_move = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/Step");
			sound_jump = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/Jump");
			sound_attack = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/Punch");

			// ----- Initialize start settings -----
			position_start = position;
			size = animation_idle.Size();
			health_max = health;
			speed = RandomGenerator.NextFloat(min: 1000, max: 1500);
			attack_range = Game.Content.tileSize;
			KI_range = 50 * Game.Content.tileSize;
			follow_range = 20 * Game.Content.tileSize;
			can_Jump = new Timer(10);
			jump_force = 10;
			can_Attack = new Timer(5);
			claim_color = Color.Green;
			claims.Add("Brain... ");
			claims.Add("I am hungry!");
			claims.Add("Do you have some money?");
			move_timer = new Timer(0.01f);
			Initialize();

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
			moved = false;
			jumped = false;
			attacked = false;

			move_random = false;
			move_left = false;
			move_right = false;
			move_jump = false;
			move_attack = false;

			if (!isDead())
			{
				move_timer.Update(false);
				RandomMovement_Timer.Update(false);
				can_Attack.Update(false);

				// ----- Health -----
				if (health > health_max)
				{
					health = health_max;
				}

				// ----- Input -----
				if (target != null)
				{
					distanceToTarget = TargetDistance(target);

					if (distanceToTarget <= KI_range)
					{
						if (distanceToTarget <= follow_range)
						{
							if (distanceToTarget > attack_range)
							{
								if (position.X > target.Position().X)
								{
									move_left = true;
								}
								else
								{
									move_right = true;
								}
							}
							else
							{
								if (can_Attack.IsTimeUp())
								{
									move_attack = true;
								}
							}
						}
						else
						{
							move_random = true;
						}
					}
				}
				else
				{
					move_random = true;
				}

				if (move_random)
				{
					if (RandomMovement_Timer.IsTimeUp())
					{
						if (RandomGenerator.NextInt(min: 0, max: 1) == 0)
						{
							randomDirectionMove = EEnemyDirection.Left;
						}
						else
						{
							randomDirectionMove = EEnemyDirection.Right;
						}
						RandomMovement_Timer.Reset(RandomGenerator.NextFloat(min: 3, max: 10));
					}

					if (randomDirectionMove == EEnemyDirection.Left)
					{
						move_left = true;
					}
					else
					{
						move_right = true;
					}
				}

				// ----- Movement -----
				// TODO Zombie.Logic() : take it into KI
				//if (hit_left || hit_right)
				//{
				//	speed_force *= 0.25f;
				//}

				//if (hit_up || hit_down)
				//{
				//	jump_force *= 0.25f;
				//}

				if (move_timer.IsTimeUp())
				{
					move_timer.Reset();
					if (move_left)
					{
						if (isGrounded)
						{
							moved = true;
							Impulse(new Vector2(-speed, 0) * Game.Content.gameTime.ElapsedGameTime.Milliseconds * Game.Content.timeScale);
						}
						direction = EEnemyDirection.Left;
					}

					if (move_right)
					{
						if (isGrounded)
						{
							moved = true;
							Impulse(new Vector2(speed, 0) * Game.Content.gameTime.ElapsedGameTime.Milliseconds * Game.Content.timeScale);
						}
						direction = EEnemyDirection.Right;
					}
				}

				// ----- Attacks -----
				// TODO Zombie.Logic() : Zombie BRAAAAINN!!!
				//if (can_Attack.IsTimeUp())
				//{
				//	if (move_attack)
				//	{
				//		if (rage > 0)
				//		{
				//			Vector2 attack_force = (direction == EPlayerDirection.Right) ? (new Vector2(50, -20)) : (new Vector2(-50, -20));
				//			Rectangle attack_range = (direction == EPlayerDirection.Right) ? (new Rectangle((int)(position.X + size.X / 2), (int)(position.Y), (int)size.X, (int)size.Y / 2)) : (new Rectangle((int)(position.X - size.X / 2), (int)(position.Y), (int)size.X, (int)(size.Y / 2)));
				//			punched = Attack(Game.Content.enemies, attack_range, 1, attack_force);
				//			if (punched)
				//			{
				//				rage--;
				//			}
				//			can_Attack.Reset(0.5f);
				//		}
				//		else
				//		{
				//			isClaiming = true;
				//		}
				//	}
				//}
			}
		}

		public override void Draw()
		{
			base.Draw();
		}
	}
}
