using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rage_of_Stickman
{
	class Scene
	{
		/// <summary>
		/// Scene is a Screen were other stuff goes on.
		/// Multiple scenecomponents can be placed into one scene.
		/// </summary>

		private List<SceneComponent> components;

		public Scene(List<SceneComponent> components)
		{
			this.components = components;
		}

		public void Update()
		{
			if (components == null)
			{
				components = new List<SceneComponent>();
			}

			if (components.Count > 0)
			{
				foreach (SceneComponent component in components)
				{
					component.Update();
				}
			}
		}

		public void Draw()
		{
			if (components == null)
			{
				components = new List<SceneComponent>();
			}

			if (components.Count > 0)
			{
				foreach (SceneComponent component in components)
				{
					component.Draw();
				}
			}
		}

		public static Scene CreateMainmenu()
		{
			// TODO Scene.CreateMainmenu()
			// ----- Create mainmenu -----
			Game.Content.camera = new Camera2D(Vector2.Zero, Vector2.Zero);

			if (Game.Content.animations[(int)EAnimation.background_mainmenu] == null)
			{
				Game.Content.animations[(int)EAnimation.background_mainmenu] = new AnimatedTexture2D(new Texture2D[] { Game.Content.contentManager.Load<Texture2D>("Graphics/Background_Mainmenu") });
			}

			Window window = new Window(Game.Content.animations[(int)EAnimation.background_mainmenu], new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));

			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(window);
			Scene scene = new Scene(components);

			return scene;
		}

		public static Scene CreateLevel1()
		{
			// ----- Create level one -----
			Game.Content.camera = new Camera2D(new Vector2(Game.Content.viewport.Width / 2.0f, Game.Content.viewport.Height / 2.0f), Vector2.Zero);
			Level level = new Level(null, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(level);
			Scene scene = new Scene(components);

			return scene;
		}

		public static Scene CreateLevel2()
		{
			// TODO Scene.CreateLevel2()
			// ----- Create level two -----
			Level level = new Level(null, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(level);
			Scene scene = new Scene(components);

			return scene;
		}

		public static Scene CreateLevel3()
		{
			// TODO Scene.CreateLevel3()
			// ----- Create level three -----
			Level level = new Level(null, new Vector2(0, 0), new Vector2(Game.Content.viewport.Width, Game.Content.viewport.Height));
			List<SceneComponent> components = new List<SceneComponent>();
			components.Add(level);
			Scene scene = new Scene(components);

			return scene;
		}
	}
}
