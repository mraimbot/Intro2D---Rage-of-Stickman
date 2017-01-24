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
			: base(startPosition, new Vector2(1, 1), 1, 0.6f, 5)
		{
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
			// TODO Kid.Logic()
		}

		public new void Draw()
		{
			base.Draw();
		}
	}
}
