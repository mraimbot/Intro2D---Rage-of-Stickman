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

			// TODO Oma.Oma() : load animations
			animation_idle = Game.Content.animations[(int)EAnimation.enemie_zombie_move];
			animation_move = Game.Content.animations[(int)EAnimation.enemie_zombie_move];
			animation_jump = Game.Content.animations[(int)EAnimation.enemie_zombie_move];
			animation_attack = Game.Content.animations[(int)EAnimation.enemie_zombie_move];

			// ----- Load Soundeffects -----
			sound_move = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/Step");
			sound_jump = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/341247__jeremysykes__jump01");
			sound_attack = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/348244__newagesoup__punch-boxing-01");


			// ----- Initialize start settings -----
			health_max = health;
			speed += RandomGenerator.NextFloat(min: -0.2f, max: 0.2f);
			jump_force = 10;
			can_Jump = new Timer(10);
			can_Attack = new Timer(5);
			claim_color = Color.Green;
			claims.Add("Brain... ");
			claims.Add("I am hungry!");
			claims.Add("Do you have some money?");
			Initialize();

			// ----- Initialize start settings -----
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
			// TODO Zombie.Logic()
			move_right = false;
			move_left = false;
			move_jump = false;
			move_attack = false;

			if (!isDead())
			{
				if (target != null)
				{
					if (target.Position().X + 0.1 < this.position.X)
					{
						move_left = true;
						direction = EEnemyDirection.Left;
					}
					else if (target.Position().X - 0.1 > this.position.X)
					{
						move_right = true;
						direction = EEnemyDirection.Right;
					}
				}

				// TODO Zombie.Logic() : Add attack
			}
		}

		public override void Draw()
		{
			base.Draw();
		}
	}
}
