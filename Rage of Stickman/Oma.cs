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
	class Oma : Enemy
	{
		public Oma(Vector2 position)
			: base(Game.Content.player, position, Vector2.One, 60, 2, 500)
		{
			// ----- Load Textures & Animations -----
			if (Game.Content.animations[(int)EAnimation.enemie_oma_move] == null)
			{
				Game.Content.textures[(int)ETexture.enemy_oma_move_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma0");
				Game.Content.textures[(int)ETexture.enemy_oma_move_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma1");
				Game.Content.textures[(int)ETexture.enemy_oma_move_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma2");
				Game.Content.textures[(int)ETexture.enemy_oma_move_3] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma3");

				Texture2D[] oma_move = { Game.Content.textures[(int)ETexture.enemy_oma_move_0], Game.Content.textures[(int)ETexture.enemy_oma_move_1], Game.Content.textures[(int)ETexture.enemy_oma_move_2], Game.Content.textures[(int)ETexture.enemy_oma_move_3] };

				Game.Content.animations[(int)EAnimation.enemie_oma_move] = new AnimatedTexture2D(oma_move, 350);
			}

			// TODO Oma.Oma() : load animations
			animation_idle = Game.Content.animations[(int)EAnimation.enemie_oma_move];
			animation_move = Game.Content.animations[(int)EAnimation.enemie_oma_move];
			animation_jump = Game.Content.animations[(int)EAnimation.enemie_oma_move];
			animation_attack = Game.Content.animations[(int)EAnimation.enemie_oma_move];

			// ----- Load Soundeffects -----
			sound_move = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/Step");
			sound_jump = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/341247__jeremysykes__jump01");
			sound_attack = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/348244__newagesoup__punch-boxing-01");


			// ----- Initialize start settings -----
			health_max = health;
			speed += RandomGenerator.NextFloat(min: -0.2f, max: 0.2f);
			jump_force = 0;
			can_Jump = new Timer(100);
			can_Attack = new Timer(10);
			claim_color = Color.Gray;
			claims.Add("Oh, my back hurts!");
			claims.Add("Don't just run around!");
			claims.Add("Can you help me?");
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
			// TODO Oma.Logic()
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

				// TODO Oma.Logic : Add attack
			}
		}

		public override void Draw()
		{
			base.Draw();
		}
	}
}
