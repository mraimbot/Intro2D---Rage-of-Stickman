using Microsoft.Xna.Framework;
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
		public Zombie(Vector2 startPosition)
			: base(startPosition, new Vector2(1, 1), 70, 1, 20)
		{
			// ----- Load Textures & Animations -----
			if (Game.Content.animations[(int)EAnimation.enemie_zombie_move] == null)
			{
				Game.Content.textures[(int)ETexture.enemy_zombie_move_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie0");
				Game.Content.textures[(int)ETexture.enemy_zombie_move_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie1");
				Game.Content.textures[(int)ETexture.enemy_zombie_move_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie2");

				Texture2D[] zombie_move = { Game.Content.textures[(int)ETexture.enemy_zombie_move_0], Game.Content.textures[(int)ETexture.enemy_zombie_move_1], Game.Content.textures[(int)ETexture.enemy_zombie_move_2] };

				Game.Content.animations[(int)EAnimation.enemie_zombie_move] = new AnimatedTexture2D(zombie_move, Game.Content.textures[(int)ETexture.enemy_zombie_move_0].Width, Game.Content.textures[(int)ETexture.enemy_zombie_move_0].Height, 1000);
			}

			this.LoadAnimations(Game.Content.animations[(int)EAnimation.enemie_zombie_move]);


			// ----- Initialize start settings -----
			Initialize();
			jump_timer.Reset(5);
			force_jump = new Vector2(0.0f, 0.2f);
			speed += RandomGenerator.NextFloat(min: -0.2f, max: 0.2f);
			claim_color = Color.Green;
			claims.Add("Brain... ");
			claims.Add("I am hungry!");
			claims.Add("Do you have some money?");
		}

		public override void Update()
		{
			if (this.active)
			{
				Logic();
			}

			base.Update();
		}

		private void Logic()
		{
			// TODO Zombie.Logic()
			move_right = false;
			move_left = false;
			move_jump = false;
			move_attack1 = false;
			move_attack2 = false;

			if (!isDead())
			{
				if (Game.Content.player.Position().X + 0.1 < this.position.X)
				{
					move_left = true;
					lookAtDirection = EDirection.left;
				}
				else if (Game.Content.player.Position().X - 0.1 > this.position.X)
				{
					move_right = true;
					lookAtDirection = EDirection.right;
				}

				// TODO Zombie.Logic : Add attack
			}
		}

		public override void Draw()
		{
			base.Draw();
		}
	}
}
