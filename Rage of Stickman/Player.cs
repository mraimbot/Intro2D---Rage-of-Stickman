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
		private SoundEffect sound_punch;
		private SoundEffect sound_kick;
		private SoundEffect sound_jump;
		private int rage;

		public Player(Vector2 startPosition, EDirection lookAtDirection)
			: base(startPosition, new Vector2(1, 1), lookAtDirection, 75, 30, true, 100)
		{
			// ----- Load Textures & Animations -----
			if (Game.Content.animations[(int)EAnimation.player_idle] == null)
			{
				Game.Content.textures[(int)ETexture.player_idle_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Stehen");
				Texture2D[] player_idle = { Game.Content.textures[(int)ETexture.player_idle_0] };
				Game.Content.animations[(int)EAnimation.player_idle] = new AnimatedTexture2D(player_idle, Game.Content.textures[(int)ETexture.player_idle_0].Width, Game.Content.textures[(int)ETexture.player_idle_0].Height, 100.0f);
			}

			if (Game.Content.animations[(int)EAnimation.player_move] == null)
			{
				Game.Content.textures[(int)ETexture.player_move_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/PlayerLauf/Player_Move_0");
				Game.Content.textures[(int)ETexture.player_move_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/PlayerLauf/Player_Move_1");
				Game.Content.textures[(int)ETexture.player_move_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/PlayerLauf/Player_Move_2");
				Texture2D[] player_move = { Game.Content.textures[(int)ETexture.player_move_0], Game.Content.textures[(int)ETexture.player_move_1], Game.Content.textures[(int)ETexture.player_move_2] };
				Game.Content.animations[(int)EAnimation.player_move] = new AnimatedTexture2D(player_move, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);
			}

			if (Game.Content.animations[(int)EAnimation.player_punch] == null)
			{
				Game.Content.textures[(int)ETexture.player_punch_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Schlag/Schlag0");
				Game.Content.textures[(int)ETexture.player_punch_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Schlag/Schlag1");
				Game.Content.textures[(int)ETexture.player_punch_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Schlag/Schlag2");
				Texture2D[] player_punch = { Game.Content.textures[(int)ETexture.player_punch_0], Game.Content.textures[(int)ETexture.player_punch_1], Game.Content.textures[(int)ETexture.player_punch_2] };
				Game.Content.animations[(int)EAnimation.player_punch] = new AnimatedTexture2D(player_punch, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);
			}

			if (Game.Content.animations[(int)EAnimation.player_kick] == null)
			{
				Game.Content.textures[(int)ETexture.player_kick_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick0");
				Game.Content.textures[(int)ETexture.player_kick_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick1");
				Game.Content.textures[(int)ETexture.player_kick_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick2");
				Game.Content.textures[(int)ETexture.player_kick_3] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Kick/Kick3");
				Texture2D[] player_kick = { Game.Content.textures[(int)ETexture.player_kick_0], Game.Content.textures[(int)ETexture.player_kick_1], Game.Content.textures[(int)ETexture.player_kick_2], Game.Content.textures[(int)ETexture.player_kick_3] };
				Game.Content.animations[(int)EAnimation.player_kick] = new AnimatedTexture2D(player_kick, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);
			}

			if (Game.Content.animations[(int)EAnimation.player_jump] == null)
			{
				Game.Content.textures[(int)ETexture.player_jump_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/Sprung0");
				Texture2D[] player_jump = { Game.Content.textures[(int)ETexture.player_jump_0] };
				Game.Content.animations[(int)EAnimation.player_jump] = new AnimatedTexture2D(player_jump, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);
			}

			if (Game.Content.animations[(int)EAnimation.player_midair] == null)
			{
				Game.Content.textures[(int)ETexture.player_midair_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/SprungMidair");
				Texture2D[] player_midair = { Game.Content.textures[(int)ETexture.player_midair_0] };
				Game.Content.animations[(int)EAnimation.player_midair] = new AnimatedTexture2D(player_midair, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);
			}

			if (Game.Content.animations[(int)EAnimation.player_landing] == null)
			{
				Game.Content.textures[(int)ETexture.player_landing_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/SprungLanding0");
				Game.Content.textures[(int)ETexture.player_landing_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/PlayerAnimation/Sprung/SprungLanding1");
				Texture2D[] player_land = { Game.Content.textures[(int)ETexture.player_landing_0], Game.Content.textures[(int)ETexture.player_landing_1] };
				Game.Content.animations[(int)EAnimation.player_landing] = new AnimatedTexture2D(player_land, Game.Content.tileSize, Game.Content.tileSize * 2, 100.0f);
			}

			AnimatedTexture2D[] animationlist = { Game.Content.animations[(int)EAnimation.player_idle], Game.Content.animations[(int)EAnimation.player_move], Game.Content.animations[(int)EAnimation.player_punch], Game.Content.animations[(int)EAnimation.player_kick], Game.Content.animations[(int)EAnimation.player_jump], Game.Content.animations[(int)EAnimation.player_midair], Game.Content.animations[(int)EAnimation.player_landing] };

			this.LoadAnimations(animationlist);

			// ----- Initialize start settings -----
			Initialize();
			size.X *= 0.8f;
			jump_timer.Reset(0.3f);
			force_jump = new Vector2(0.0f, -100f);
			useWind = false;
			rage = 50;

			sound_kick = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/348244__newagesoup__punch-boxing-01");
			sound_punch = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/216197__rsilveira-88__cartoon-punch-03");
			sound_jump = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/341247__jeremysykes__jump01");
		}

		public override void Update()
		{
			if (active)
			{
				Input();
				Trigger();
				Logic();
			}

			CameraController();

			base.Update();
		}

		private void Input()
		{
			move_left = false;
			move_right = false;
			move_jump = false;
			move_attack1 = false;
			move_attack2 = false;

			foreach (Keys key in Keyboard.GetState().GetPressedKeys())
			{
				switch (key)
				{
					case Keys.W:
					case Keys.Up:
					case Keys.Space:
						move_jump = true;
						break;

					case Keys.A:
					case Keys.Left:
						move_left = true;
						break;

					case Keys.D:
					case Keys.Right:
						move_right = true;
						break;

					case Keys.E:
					case Keys.Y:
						move_attack1 = true;
						break;

					case Keys.F:
					case Keys.X:
						move_attack2 = true;
						break;
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

		private void Logic()
		{
			if (!isDead())
			{
				if (can_attack.IsTimeUp())
				{
					if (move_attack1) // Punch
					{
						Vector2 attack_force = (lookAtDirection == EDirection.right) ? (new Vector2(50, -20)) : (new Vector2(-50, -20));
						if (Attack(Game.Content.enemies, new Rectangle((int)(position.X + size.X / 2), (int)(position.Y), (int)size.X, (int)size.Y / 2), 1, attack_force, 0.2f))
						{
							sound_punch.Play();
						}
					}

					if (move_attack2) // Kick
					{
						Vector2 attack_force = (lookAtDirection == EDirection.right) ? (new Vector2(25, -80)) : (new Vector2(-25, -80));
						if (Attack(Game.Content.enemies, new Rectangle((int)(position.X + size.X / 2), (int)(position.Y + size.Y / 2), (int)size.X, (int)size.Y / 2), 5, attack_force, 0.7f))
						{
							sound_kick.Play();
						}
					}
				}

				if (move_jump && jump_timer.IsTimeUp())
				{
					sound_jump.Play(0.05f, -1f, 1f);
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

		public override void Draw()
		{
			SpriteEffects s = SpriteEffects.None;

			if (lookAtDirection == EDirection.left)
			{
				s = SpriteEffects.FlipHorizontally;
			}
			else
			{
				s = SpriteEffects.None;
			}

			if (animations == null || animations.Length == 0)
			{
				base.Draw();
			}
			else
			{
				// Animations:
				//		1: idle
				//		2: move
				//		3: punch
				//		4: kick
				//		5: jump
				//		6: midair
				//		7: land

				// TODO Player.Draw() : Build in animation_landing
				if (!isGrounded)
				{
					this.animations[5].Update();
					this.animations[5].Draw(position, s);
				}
				else if (move_left || move_right)
				{
					this.animations[1].Update();
					this.animations[1].Draw(position, s);
				}
				else if (move_attack1)
				{
					this.animations[2].Update();
					this.animations[2].Draw(position, s);
				}
				else if (move_attack2)
				{
					this.animations[3].Update();
					this.animations[3].Draw(position, s);
				}
				else if (move_jump)
				{
					this.animations[4].Update();
					this.animations[4].Draw(position, s);
				}
				else
				{
					this.animations[0].Update();
					this.animations[0].Draw(position, s);
				}
			}

			DrawGUI();
			// Game.Content.spriteBatch.Draw(Game.Content.textures[(int)ETexture.pixel], new Rectangle((int)this.position.X, (int)this.position.Y, (int)size.X, (int)size.Y), Color.Green); 
		}

		private void DrawGUI()
		{
			Vector2 origin = Game.Content.camera.Position() - Game.Content.camera.Origin();
			// TODO Player.DrawGUI()
			DrawPrimitive.Rectangle(origin, new Color (32, 16, 0, 32), 136, 72);
			ShowText.Text(new Vector2(origin.X + 8, origin.Y + 16), "Lifepoints: " + health, Color.Green, 0, 1, ETextFormate.Left);
			ShowText.Text(new Vector2(origin.X + 8, origin.Y + 48), "Rage: " + rage, Color.Red, 0, 1, ETextFormate.Left);
		}
	}
}
