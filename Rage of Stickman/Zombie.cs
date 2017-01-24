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
			: base(startPosition, new Vector2(1, 1), 2, 0.3f, 20)
		{
			if (Game.Content.animations[(int)EAnimation.enemie_zombie_move] == null)
			{
				Game.Content.textures[(int)ETexture.enemy_zombie_move_0] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie0");
				Game.Content.textures[(int)ETexture.enemy_zombie_move_1] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie1");
				Game.Content.textures[(int)ETexture.enemy_zombie_move_2] = Game.Content.contentManager.Load<Texture2D>("Graphics/Enemies/Zombie/Zombie2");

				Texture2D[] zombie_move = { Game.Content.textures[(int)ETexture.enemy_zombie_move_0], Game.Content.textures[(int)ETexture.enemy_zombie_move_1], Game.Content.textures[(int)ETexture.enemy_zombie_move_2] };

				Game.Content.animations[(int)EAnimation.enemie_zombie_move] = new AnimatedTexture2D(zombie_move, Game.Content.textures[(int)ETexture.enemy_zombie_move_0].Width, Game.Content.textures[(int)ETexture.enemy_zombie_move_0].Height, 200.0f);
			}

			this.LoadAnimations(Game.Content.animations[(int)EAnimation.enemie_zombie_move]);
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
			// TODO Zombie.Logic()
		}

		public new void Draw()
		{
			base.Draw();
		}
	}
}
