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
			: base(startPosition, new Vector2(1, 1), 2, 0.2f, 500)
		{
			if (Game.Content.animations[(int)EAnimation.enemie_oma_move] == null)
			{
				Game.Content.textures[(int)ETexture.enemy_oma_move_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma0");
				Game.Content.textures[(int)ETexture.enemy_oma_move_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma1");
				Game.Content.textures[(int)ETexture.enemy_oma_move_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma2");
				Game.Content.textures[(int)ETexture.enemy_oma_move_3] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Oma/Oma3");

				Texture2D[] oma_move = { Game.Content.textures[(int)ETexture.enemy_oma_move_0], Game.Content.textures[(int)ETexture.enemy_oma_move_1], Game.Content.textures[(int)ETexture.enemy_oma_move_2], Game.Content.textures[(int)ETexture.enemy_oma_move_3] };

				Game.Content.animations[(int)EAnimation.enemie_oma_move] = new AnimatedTexture2D(oma_move, Game.Content.textures[(int)ETexture.enemy_oma_move_0].Width, Game.Content.textures[(int)ETexture.enemy_oma_move_0].Height, 300.0f);
			}

			this.LoadAnimations(Game.Content.animations[(int)EAnimation.enemie_oma_move]);
		}

		public new void Update()
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
		}

		public new void Draw()
		{
			base.Draw();
		}
	}
}
