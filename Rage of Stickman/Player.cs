using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Audio;

namespace Rage_of_Stickman
{
	class Player : Entity
	{
		private enum EPlayerDirection
		{
			Left,
			Right
		}

		private AnimatedTexture2D animation_idle;
		private AnimatedTexture2D animation_move;
		private AnimatedTexture2D animation_jump;
		private AnimatedTexture2D animation_punch;
		private AnimatedTexture2D animation_kick;

		private SoundEffect sound_move;
		private SoundEffect sound_jump;
		private SoundEffect sound_punch;
		private SoundEffect sound_kick;

		private bool move_left;
		private bool move_right;
		private bool move_jump;
		private bool move_punch;
		private bool move_kick;

		private bool jumped;
		private bool punched;
		private bool kicked;

		private bool jumping;

		private Vector2 position_start;
		private EPlayerDirection direction;

		private int health_max;
		private int rage;

		private float speed_max;
		private float speed;
		private Timer speed_next_boost;

		private float jump_force_max;
		private float jump_force;
		private Timer can_Jump;
		private Timer can_Jump_next_boost;

		private Timer can_Attack;

		protected Timer claim_timer;
		protected bool isClaiming;

		public Player(Vector2 position)
			: base(position, Vector2.One, 0, 100, 75, false, true, false, true)
		{
			// ----- Load Textures & Animations -----
			if (Game.Content.animations[(int)EAnimation.player_idle] == null)
			{
				Game.Content.textures[(int)ETexture.player_idle_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Player_Idle");
				Texture2D[] player_idle = { Game.Content.textures[(int)ETexture.player_idle_0] };
				Game.Content.animations[(int)EAnimation.player_idle] = new AnimatedTexture2D(player_idle, 100);
			}

			if (Game.Content.animations[(int)EAnimation.player_move] == null)
			{
				Game.Content.textures[(int)ETexture.player_move_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/PlayerLauf/Player_Move_0");
				Game.Content.textures[(int)ETexture.player_move_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/PlayerLauf/Player_Move_1");
				Game.Content.textures[(int)ETexture.player_move_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/PlayerLauf/Player_Move_2");
				Texture2D[] player_move = { Game.Content.textures[(int)ETexture.player_move_0], Game.Content.textures[(int)ETexture.player_move_1], Game.Content.textures[(int)ETexture.player_move_2] };
				Game.Content.animations[(int)EAnimation.player_move] = new AnimatedTexture2D(player_move, 100);
			}

			if (Game.Content.animations[(int)EAnimation.player_jump] == null)
			{
				Game.Content.textures[(int)ETexture.player_jump_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/Sprung0");
				Texture2D[] player_jump = { Game.Content.textures[(int)ETexture.player_jump_0] };
				Game.Content.animations[(int)EAnimation.player_jump] = new AnimatedTexture2D(player_jump, 100);
			}

			if (Game.Content.animations[(int)EAnimation.player_punch] == null)
			{
				Game.Content.textures[(int)ETexture.player_punch_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Schlag/Schlag0");
				Game.Content.textures[(int)ETexture.player_punch_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Schlag/Schlag1");
				Game.Content.textures[(int)ETexture.player_punch_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Schlag/Schlag2");
				Texture2D[] player_punch = { Game.Content.textures[(int)ETexture.player_punch_0], Game.Content.textures[(int)ETexture.player_punch_1], Game.Content.textures[(int)ETexture.player_punch_2] };
				Game.Content.animations[(int)EAnimation.player_punch] = new AnimatedTexture2D(player_punch, 100);
			}

			if (Game.Content.animations[(int)EAnimation.player_kick] == null)
			{
				Game.Content.textures[(int)ETexture.player_kick_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick0");
				Game.Content.textures[(int)ETexture.player_kick_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick1");
				Game.Content.textures[(int)ETexture.player_kick_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick2");
				Game.Content.textures[(int)ETexture.player_kick_3] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick3");
				Texture2D[] player_kick = { Game.Content.textures[(int)ETexture.player_kick_0], Game.Content.textures[(int)ETexture.player_kick_1], Game.Content.textures[(int)ETexture.player_kick_2], Game.Content.textures[(int)ETexture.player_kick_3] };
				Game.Content.animations[(int)EAnimation.player_kick] = new AnimatedTexture2D(player_kick, 100);
			}

			// TODO Player.Player() : Load midair and landing Animations
			//if (Game.Content.animations[(int)EAnimation.player_midair] == null)
			//{
			//	Game.Content.textures[(int)ETexture.player_midair_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/SprungMidair");
			//	Texture2D[] player_midair = { Game.Content.textures[(int)ETexture.player_midair_0] };
			//	Game.Content.animations[(int)EAnimation.player_midair] = new AnimatedTexture2D(player_midair, 100);
			//}

			//if (Game.Content.animations[(int)EAnimation.player_landing] == null)
			//{
			//	Game.Content.textures[(int)ETexture.player_landing_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/SprungLanding0");
			//	Game.Content.textures[(int)ETexture.player_landing_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/SprungLanding1");
			//	Texture2D[] player_land = { Game.Content.textures[(int)ETexture.player_landing_0], Game.Content.textures[(int)ETexture.player_landing_1] };
			//	Game.Content.animations[(int)EAnimation.player_landing] = new AnimatedTexture2D(player_land, 100);
			//}

			animation_idle = Game.Content.animations[(int)EAnimation.player_idle];
			animation_move = Game.Content.animations[(int)EAnimation.player_move];
			animation_jump = Game.Content.animations[(int)EAnimation.player_jump];
			animation_punch =  Game.Content.animations[(int)EAnimation.player_punch];
			animation_kick = Game.Content.animations[(int)EAnimation.player_kick];

			// ----- Load Soundeffects -----
			sound_move = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/Step");
			sound_jump = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/341247__jeremysykes__jump01");
			sound_kick = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/348244__newagesoup__punch-boxing-01");
			sound_punch = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/Punch");

			// ----- Initialize start settings -----
			position_start = position;
			size = animation_idle.Size();
			health_max = health;
			speed_max = 30;
			speed_next_boost = new Timer(0.05f);
			jump_force_max = 75;
			can_Jump = new Timer(0.3f);
			can_Jump_next_boost = new Timer(0.05f);
			can_Attack = new Timer(1);
			claim_timer = new Timer(1);
			Initialize();
		}

		public void Initialize()
		{
			// TODO Player.Initialize() : size.X *= 0.8f ?
			// size.X *= 0.8f;
			direction = EPlayerDirection.Right;
			position = position_start;
			health = health_max;
			rage = 50;
			speed = 0;
			jump_force = 0;
			can_Jump.Reset();

			jumping = false;
		}

		public override void Update(bool isPaused)
		{
			if (isActive)
			{
				if (!isPaused)
				{
					Input();
					Logic();
					Trigger();
				}
			}

			base.Update(isPaused);

			CameraController();
		}

		private void Input()
		{
			move_left = false;
			move_right = false;
			move_jump = false;
			move_punch = false;
			move_kick = false;

			foreach (Keys key in Keyboard.GetState().GetPressedKeys())
			{
				switch (key)
				{
					case Keys.A:
					case Keys.Left:
						move_left = true;
						break;

					case Keys.D:
					case Keys.Right:
						move_right = true;
						break;

					case Keys.W:
					case Keys.Up:
					case Keys.Space:
						move_jump = true;
						break;

					case Keys.E:
					case Keys.Y:
						move_punch = true;
						break;

					case Keys.F:
					case Keys.X:
						move_kick = true;
						break;
				}
			}
		}

		private void Logic()
		{
			jumped = false;
			punched = false;
			kicked = false;

			if (!isDead())
			{
				speed_next_boost.Update(false);
				can_Jump.Update(false);
				can_Jump_next_boost.Update(false);
				can_Attack.Update(false);

				// ----- Movement -----
				if (!move_left && !move_right)
				{
					speed = 0;
				}

				if (move_left && speed_next_boost.IsTimeUp() && isGrounded)
				{
					speed -= (speed_max * 0.25f);
					direction = EPlayerDirection.Left;
				}

				if (move_right && speed_next_boost.IsTimeUp() && isGrounded)
				{
					speed += (speed_max * 0.25f);
					direction = EPlayerDirection.Right;
				}

				if (speed > speed_max)
				{
					speed = speed_max;
				}
				else if (speed < -speed_max)
				{
					speed = -speed_max;
				}

				if (speed_next_boost.IsTimeUp())
				{
					speed_next_boost.Reset();
					Force(new Vector2(speed, 0));
				}

				// ----- Jump -----
				// TODO Player.Logic() : Adjust playerjump

				// TODO DEBUG
				Console.WriteLine("speed = " + speed);

				if (isGrounded)
				{
					jumping = false;
				}

				if (jumping)
				{
					can_Jump.Reset();
				}
				else
				{
					jump_force = 0;
				}

				if (move_jump)
				{
					if (can_Jump.IsTimeUp() && isGrounded)
					{
						jumped = true;
						jumping = true;
						jump_force = (jump_force_max * 0.5f);
					}
					else if (jumping && can_Jump_next_boost.IsTimeUp() && !isGrounded)
					{
						jump_force += (jump_force_max * 0.5f);
						if (jump_force >= jump_force_max)
						{
							jump_force = 0;
							can_Jump.Reset();
						}
					}
				}
				else
				{
					jumping = false;
				}

				if (can_Jump_next_boost.IsTimeUp())
				{
					can_Jump_next_boost.Reset();
					Force(new Vector2(0, -jump_force));
				}

				// ----- Attacks -----
				if (can_Attack.IsTimeUp())
				{
					if (move_punch && rage > 0)
					{
						if (rage > 0)
						{
							Vector2 attack_force = (direction == EPlayerDirection.Right) ? (new Vector2(50, -20)) : (new Vector2(-50, -20));
							Rectangle attack_range = (direction == EPlayerDirection.Right) ? (new Rectangle((int)(position.X + size.X / 2), (int)(position.Y), (int)size.X, (int)size.Y / 2)) : (new Rectangle((int)(position.X + size.X / 2), (int)(position.Y), -(int)size.X, -(int)size.Y / 2));
							punched = Attack(Game.Content.enemies, attack_range, 1, attack_force);
							can_Attack.Reset(0.5f);
							rage--;
						}
						else
						{
							isClaiming = true;
							claim_timer.Reset(6);
						}
					}

					else if (move_kick && rage > 4)
					{
						if (rage > 4)
						{
							Vector2 attack_force = (direction == EPlayerDirection.Right) ? (new Vector2(25, -40)) : (new Vector2(-25, -40));
							Rectangle attack_range = (direction == EPlayerDirection.Right) ? (new Rectangle((int)(position.X + size.X / 2), (int)(position.Y + size.Y / 2), (int)size.X, (int)size.Y / 2)) : (new Rectangle((int)(position.X + size.X / 2), (int)(position.Y + size.Y / 2), -(int)size.X, -(int)size.Y / 2));
							punched = Attack(Game.Content.enemies, attack_range, 5, attack_force);
							can_Attack.Reset(1);
							rage -= 5;
						}
						else
						{
							isClaiming = true;
							claim_timer.Reset(6);
						}
					}
				}

				if (isClaiming)
				{
					claim_timer.Update(false);
					if (claim_timer.IsTimeUp())
					{
						isClaiming = false;
					}
				}
			}
		}

		private void Trigger()
		{
			foreach (Trigger trigger in Game.Content.triggers)
			{
				if (new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y).Intersects(new Rectangle((int)trigger.Position().X, (int)trigger.Position().Y, (int)trigger.Size().X, (int)trigger.Size().Y)))
				{
					trigger.Activate();
				}
			}
		}

		private void CameraController()
		{
			Vector2 position_camera = position;
			if (position_camera.X - Game.Content.camera.Origin().X < 0)
			{
				position_camera.X = Game.Content.camera.Origin().X;
			}
			else if (position_camera.X + Game.Content.camera.Origin().X > Game.Content.tileMap.Size().X * Game.Content.tileSize)
			{
				position_camera.X = Game.Content.tileMap.Size().X * Game.Content.tileSize - Game.Content.camera.Origin().X;
			}

			if (position_camera.Y - Game.Content.camera.Origin().Y < 0)
			{
				position_camera.Y = Game.Content.camera.Origin().Y;
			}
			else if (position_camera.Y + Game.Content.camera.Origin().Y > Game.Content.tileMap.Size().Y * Game.Content.tileSize)
			{
				position_camera.Y = Game.Content.tileMap.Size().Y * Game.Content.tileSize - Game.Content.camera.Origin().Y;
			}

			Game.Content.camera.Update(position_camera);
		}

		private bool Attack(List<Entity> targets, Rectangle attack_range, int damage, Vector2 attack_force)
		{
			bool hitTarget = false;

			foreach (Entity target in targets)
			{
				if (attack_range.Intersects(new Rectangle((int)target.Position().X, (int)target.Position().Y, (int)target.Size().X, (int)target.Size().Y)))
				{
					target.Damage(damage);
					target.Force(attack_force);
					hitTarget = true;
				}
			}

			return hitTarget;
		}

		public override void Draw()
		{
			if (isVisible)
			{
				SpriteEffects s = SpriteEffects.None;

				if (direction == EPlayerDirection.Left)
				{
					s = SpriteEffects.FlipHorizontally;
				}
				else
				{
					s = SpriteEffects.None;
				}

				// TODO Player.Draw() : Build in animation_landing
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
				else if (move_punch)
				{
					animation_punch.Update();
					animation_punch.Draw(position, s);
				}
				else if (move_kick)
				{
					animation_kick.Update();
					animation_kick.Draw(position, s);
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
					ShowText.Text(new Vector2(position.X + size.X / 2, position.Y - 32), "I'm not that angry.", Color.Red, 0, 1, ETextAlign.Center);
				}
			}

			if (jumped)
			{
				sound_jump.Play(0.05f, RandomGenerator.NextFloat(min: -1, max: -0.5f), 0);
			}

			else if (punched)
			{
				sound_punch.Play(1, RandomGenerator.NextFloat(min: -0.2f, max: 0.2f), 0);
			}
			else if (kicked)
			{
				sound_kick.Play(1, RandomGenerator.NextFloat(min: -0.2f, max: 0.2f), 0);
			}

			DrawGUI();
		}

		private void DrawGUI()
		{
			Vector2 origin = Game.Content.camera.Position() - Game.Content.camera.Origin();
			DrawPrimitive.Rectangle(origin, new Color (32, 16, 0, 32), 170, 80);
			ShowText.Text(new Vector2(origin.X + 8, origin.Y + 16), "Lifepoints: " + health, Color.Green, 0, 1, ETextAlign.Left);
			ShowText.Text(new Vector2(origin.X + 8, origin.Y + 48), "Rage: " + rage, Color.Red, 0, 1, ETextAlign.Left);
		}
	}
}
