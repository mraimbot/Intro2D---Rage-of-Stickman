﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class SceneComponent : GameObject
	{
		/// <summary>
		/// SceneComponents are objects which can be placed into a scene.
		/// Multiple scenecomponents can be placed into one scene.
		/// 
		/// Examples are gamelevels and windows forms.
		/// </summary>

		protected AnimatedTexture2D background;

		public SceneComponent(AnimatedTexture2D background, Vector2 position, Vector2 size, bool active = true, bool visible = true)
			: base(position, size, active, visible)
		{
			this.background = background;
		}

		public virtual void EventHandler()
		{

		}

		public override void Update(bool isPaused)
		{
			base.Update(isPaused);
			//if (active)
			//{

			//}
		}

		public override void Draw()
		{
			base.Draw();

			if (visible)
			{
				if (background != null)
				{
					background.Draw(position);
				}
			}
		}
	}
}
