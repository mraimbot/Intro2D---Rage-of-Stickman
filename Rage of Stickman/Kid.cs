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
	class Kid : Enemy
	{
		public Kid(Vector2 position)
			: base(Game.Content.player, position, Vector2.One, 35, 10, 5)
		{
			// ----- Load Textures & Animations -----
			if (Game.Content.animations[(int)EAnimation.enemie_kid_move] == null)
			{
				Game.Content.textures[(int)ETexture.enemy_kid_move_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Kid/Kid1");
				Game.Content.textures[(int)ETexture.enemy_kid_move_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Kid/Kid2");
				Game.Content.textures[(int)ETexture.enemy_kid_move_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Kid/Kid3");
				Game.Content.textures[(int)ETexture.enemy_kid_move_3] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Kid/Kid4");

				Texture2D[] kid_move = { Game.Content.textures[(int)ETexture.enemy_kid_move_0], Game.Content.textures[(int)ETexture.enemy_kid_move_1], Game.Content.textures[(int)ETexture.enemy_kid_move_2], Game.Content.textures[(int)ETexture.enemy_kid_move_3] };

				Game.Content.animations[(int)EAnimation.enemie_kid_move] = new AnimatedTexture2D(kid_move, 100);
			}

			// TODO Kid.Kid() : load animations
			animation_idle = Game.Content.animations[(int)EAnimation.enemie_kid_move];
			animation_move = Game.Content.animations[(int)EAnimation.enemie_kid_move];
			animation_jump = Game.Content.animations[(int)EAnimation.enemie_kid_move];
			animation_attack = Game.Content.animations[(int)EAnimation.enemie_kid_move];

			// ----- Load Soundeffects -----
			sound_move = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/Step");
			sound_jump = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/Jump");
			sound_attack = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/Punch");

			// ----- Initialize start settings -----
			position_start = position;
			size = animation_idle.Size();
			health_max = health;
			speed = 10000; // RandomGenerator.NextFloat(min: 50000, max: 55000);
			attack_range = Game.Content.tileSize * 2;
			KI_range = 50 * Game.Content.tileSize;
			follow_range = 20 * Game.Content.tileSize;
			can_Jump = new Timer(10);
			jump_force = 100;
			can_Attack = new Timer(1);
			claim_color = Color.Pink;
			claims.Add("Play with me!");
			claims.Add("Are you poor?");
			claims.Add("You look like you are single.");
			claims.Add("Why are you so big?");
			claims.Add("You look very ugly.");
			claims.Add("Why are you doing this?");
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
								move_jump = true;
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

					if (move_jump)
					{
						if (isGrounded)
						{
							jumped = true;
							Impulse(new Vector2(0, -jump_force));
						}

						if (direction == EEnemyDirection.Left)
						{
							Impulse(new Vector2(-100, 0));
						}
						else
						{
							Impulse(new Vector2(100, 0));
						}
					}
				}

				// ----- Attacks -----
				if (can_Attack.IsTimeUp())
				{
					if (move_attack)
					{
						Vector2 attack_force = (direction == EEnemyDirection.Right) ? (new Vector2(50, -20)) : (new Vector2(-50, -20));
						Rectangle attack_range = new Rectangle((int)(position.X), (int)(position.Y), (int)size.X, (int)size.Y);
						attacked = Annoy(new List<Entity> { target }, attack_range, 3, Vector2.Zero);
						if (attacked)
						{
							Attack(new List<Entity> { target }, attack_range, 1, attack_force);
							can_Attack.Reset(0.5f);
						}
					}
				}
			}
		}

		public override void Draw()
		{
			base.Draw();
		}
	}
}
