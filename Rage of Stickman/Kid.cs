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

			// TODO Oma.Oma() : load animations
			animation_idle = Game.Content.animations[(int)EAnimation.enemie_kid_move];
			animation_move = Game.Content.animations[(int)EAnimation.enemie_kid_move];
			animation_jump = Game.Content.animations[(int)EAnimation.enemie_kid_move];
			animation_attack = Game.Content.animations[(int)EAnimation.enemie_kid_move];

			// ----- Load Soundeffects -----
			sound_move = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/Step");
			sound_jump = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/341247__jeremysykes__jump01");
			sound_attack = Game.Content.contentManager.Load<SoundEffect>("SoundEffects/348244__newagesoup__punch-boxing-01");


			// ----- Initialize start settings -----
			health_max = health;
			speed += RandomGenerator.NextFloat(min: -0.5f, max: 0.5f);
			jump_force = 20;
			can_Jump = new Timer(1);
			can_Attack = new Timer(3);
			claim_color = Color.Pink;
			claims.Add("Play with me!");
			claims.Add("Are you poor?");
			claims.Add("You look like you are single.");
			claims.Add("Why are you so big?");
			claims.Add("You look very ugly.");
			claims.Add("Why are you doing this?");
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
			// TODO Kid.Logic()
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

				// TODO Kid.Logic() : Add attack
			}
		}

		public override void Draw()
		{
			base.Draw();
		}
	}
}
