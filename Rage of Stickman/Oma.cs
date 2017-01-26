using Microsoft.Xna.Framework;
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
		public Oma(Vector2 startPosition)
			: base(startPosition, new Vector2(1, 1), 60, 5, 500)
		{
			// ----- Load Textures & Animations -----
			if (Game.Content.animations[(int)EAnimation.enemie_oma_move] == null)
			{
				Game.Content.textures[(int)ETexture.enemy_oma_move_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma0");
				Game.Content.textures[(int)ETexture.enemy_oma_move_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma1");
				Game.Content.textures[(int)ETexture.enemy_oma_move_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma2");
				Game.Content.textures[(int)ETexture.enemy_oma_move_3] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma3");

				Texture2D[] oma_move = { Game.Content.textures[(int)ETexture.enemy_oma_move_0], Game.Content.textures[(int)ETexture.enemy_oma_move_1], Game.Content.textures[(int)ETexture.enemy_oma_move_2], Game.Content.textures[(int)ETexture.enemy_oma_move_3] };

				Game.Content.animations[(int)EAnimation.enemie_oma_move] = new AnimatedTexture2D(oma_move, Game.Content.textures[(int)ETexture.enemy_oma_move_0].Width, Game.Content.textures[(int)ETexture.enemy_oma_move_0].Height, 350.0f);
			}

			this.LoadAnimations(Game.Content.animations[(int)EAnimation.enemie_oma_move]);


			// ----- Initialize start settings -----
			Initialize();
			force_jump = new Vector2(0, 0);
			speed += RandomGenerator.NextFloat(min: -1, max: 0.5f);
			claim_color = Color.Gray;
			claims.Add("Oh, my back!");
			claims.Add("Don't just run around!");
			claims.Add("Can you help me?");
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
			// TODO Oma.Logic()
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

				// TODO Oma.Logic : Add attack
			}
		}

		public override void Draw()
		{
			base.Draw();
		}
	}
}
