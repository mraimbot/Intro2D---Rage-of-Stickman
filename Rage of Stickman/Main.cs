/*
 * File: Main.cs
 * Description: This is the main file where all important data is created and initialized.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rage_of_Stickman
{
	public class Main : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

	    private Player player;

		public Main()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

		    player = new Player(Content.Load<Texture2D>("player"), new Vector2(0, 300));
		}

		protected override void UnloadContent(){}

		protected override void Update(GameTime gameTime)
		{
			// TODO Main - Update() - Delete Exit() through Escape-Key.
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

		    player.Update();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
		    spriteBatch.Begin();

		    player.Draw(spriteBatch);

            spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
