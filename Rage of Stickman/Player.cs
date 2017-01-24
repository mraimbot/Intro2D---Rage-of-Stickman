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
			AnimatedTexture2D[] animationlist = { Game.Content.animations[(int)EAnimation.player_idle], Game.Content.animations[(int)EAnimation.player_move], Game.Content.animations[(int)EAnimation.player_punch], Game.Content.animations[(int)EAnimation.player_kick], Game.Content.animations[(int)EAnimation.player_jump], Game.Content.animations[(int)EAnimation.player_midair], Game.Content.animations[(int)EAnimation.player_land] };
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
