using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
			: base(position, size, 0, Color.CornflowerBlue, active, visible)
		{
			this.background = background;
		}

		public virtual void EventHandler()
		{
			if (Game.Content.gameEvents.Count > 0)
			{
				for (int ID = Game.Content.gameEvents.Count - 1; ID >= 0; ID--)
				{
					if (Game.Content.gameEvents[ID].Target() == ETarget.SceneComponent)
					{
						switch (Game.Content.gameEvents[ID].Event())
						{
							case EGameEvent.NewBackground:
								Background(Game.Content.gameEvents.ElementAt(ID).Text());
								break;
						}
						Game.Content.gameEvents.RemoveAt(ID);
					}
				}
			}
		}

		public void Background(string source)
		{
			if (source != "")
			{
				background = new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>(source) });
			}
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

			if (isVisible)
			{
				if (background != null)
				{
					background.Draw(position);
				}
				else
				{
					DrawPrimitive.Rectangle(position, new Color(32, 0, 0, 32), (int)size.X, (int)size.Y);
				}
			}
		}
	}
}
