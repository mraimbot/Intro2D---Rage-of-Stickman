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

                TileMap tileMap;
		private Player player;

		public Main()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
            tileMap = new TileMap(new Texture2D[] { Content.Load<Texture2D>("Asphalt"),Content.Load<Texture2D>("Stein") }, Content.Load<Texture2D>("RageMap"),16);
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


            tileMap.Update(gameTime);
            player.Update();
     
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();

            tileMap.Draw(spriteBatch);


		    player.Draw(spriteBatch);

            spriteBatch.End();
			base.Draw(gameTime);

		}
	}
}
