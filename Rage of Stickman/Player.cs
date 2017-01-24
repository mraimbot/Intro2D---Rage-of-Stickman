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

namespace Rage_of_Stickman
{
	class Player : Entity
	{
		/// <summary>
		/// Animations:
		///		1: idle
		///		2: move
		///		3: punch
		///		4: kick
		///		5: jump
		///		6: midair
		///		7: land
		/// </summary>

		private bool move_left;
		private bool move_right;
		private bool move_punch;
		private bool move_kick;
		private bool move_jump;

		private Vector2 force_jump = new Vector2(0.0f, -1.5f);

		public Player(Vector2 startPosition, EDirection lookAtDirection, float mass, float speed, int health)
			: base(startPosition, new Vector2(1, 1), lookAtDirection, mass, speed, true, health)
		{
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
			Initialize();
		}

		public void Initialize()
		{
			this.position = this.startPosition;
			this.health = this.health_max;
		}

		public new void Update()
		{
			if (this.active)
			{
				Input();
				Logic();
			}

			base.Update();
		}

		private void Input()
		{
			this.move_jump = false;
			this.move_left = false;
			this.move_right = false;
			this.move_punch = false;
			this.move_kick = false;

			if (!this.isDead())
			{
				foreach (Keys key in Keyboard.GetState().GetPressedKeys())
					switch (key)
					{
						case Keys.W:
						case Keys.Up:
						case Keys.Space:
							this.move_jump = true;
							break;

						case Keys.A:
						case Keys.Left:
							this.move_left = true;
							break;

						case Keys.D:
						case Keys.Right:
							this.move_right = true;
							break;

						case Keys.E:
						case Keys.Y:
							this.move_punch = true;
							break;

						case Keys.F:
						case Keys.X:
							this.move_kick = true;
							break;
					}
			}
		}

		private void Logic()
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

			if (this.move_punch)
			{
				// TODO Player.move_punch
			}

			if (this.move_kick)
			{
				// TODO Player.move_kick
			}
		}

		public new void Draw()
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
				// TODO Player: Build in animation_land
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
				else if (move_punch)
				{
					this.animations[2].Update();
					this.animations[2].Draw(position, s);
				}
				else if (move_kick)
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

			// Game.Content.spriteBatch.Draw(Game.Content.textures[(int)ETexture.pixel], new Rectangle((int)this.position.X, (int)this.position.Y, (int)size.X, (int)size.Y), Color.Green); 
		}
	}
}
