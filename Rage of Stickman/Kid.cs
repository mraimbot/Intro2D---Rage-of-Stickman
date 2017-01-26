using Microsoft.Xna.Framework;
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
		public Kid(Vector2 startPosition)
			: base(startPosition, new Vector2(1, 1), 1, 0.4f, 5)
		{
			// ----- Load Textures & Animations -----
			if (Game.Content.animations[(int)EAnimation.enemie_kid_move] == null)
			{
				Game.Content.textures[(int)ETexture.enemy_kid_move_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Kid/Kid1");
				Game.Content.textures[(int)ETexture.enemy_kid_move_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Kid/Kid2");
				Game.Content.textures[(int)ETexture.enemy_kid_move_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Kid/Kid3");
				Game.Content.textures[(int)ETexture.enemy_kid_move_3] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Kid/Kid4");

				Texture2D[] kid_move = { Game.Content.textures[(int)ETexture.enemy_kid_move_0], Game.Content.textures[(int)ETexture.enemy_kid_move_1], Game.Content.textures[(int)ETexture.enemy_kid_move_2], Game.Content.textures[(int)ETexture.enemy_kid_move_3] };

				Game.Content.animations[(int)EAnimation.enemie_kid_move] = new AnimatedTexture2D(kid_move, Game.Content.textures[(int)ETexture.enemy_kid_move_0].Width, Game.Content.textures[(int)ETexture.enemy_kid_move_0].Height, 100.0f);
			}

			this.LoadAnimations(Game.Content.animations[(int)EAnimation.enemie_kid_move]);

			// ----- Initialize start settings -----
			force_jump = new Vector2(0.0f, -0.6f);
			speed += RandomGenerator.NextFloat(min: -0.02f, max: 0.01f);
			Initialize();
		}

		public override void Update()
		{
			if (active)
			{
				Logic();
			}

			base.Update();
		}

		private void Logic()
		{
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
					move_jump = true;
					lookAtDirection = EDirection.left;
				}
				else if (Game.Content.player.Position().X - 0.1 > this.position.X)
				{
					move_right = true;
					move_jump = true;
					lookAtDirection = EDirection.right;
				}

				// TODO Kid.Logic : Add attack
			}
		}

		public override void Draw()
		{
			base.Draw();
		}
	}
}
