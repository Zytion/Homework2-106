using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Homework2_106
{
	enum GameState { Menu, Game, GameOver };

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		GameState gameState;
        Player player;
        List<Collectible> collectibles;
        int level = 0;
        KeyboardState kbState;
        KeyboardState previousKBState;
        float timer;

        Texture2D collectableTexture;
        Texture2D playerTexture;

        Random rnd = new Random();

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTexture = Content.Load<Texture2D>("terrariaguide");

            collectableTexture = Content.Load<Texture2D>("coin");

        }

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			KeyboardState keyboard = Keyboard.GetState();

			if(gameState == GameState.Menu && keyboard.IsKeyDown(Keys.Enter))
				gameState = GameState.Game;
			else if(gameState == GameState.GameOver && keyboard.IsKeyDown(Keys.Enter))
				gameState = GameState.Menu;
			//else if()	//Timer runs out
			//gameState = GameState.GameOver;


			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}

        public void NextLevel()
        {
            level++;
            timer = 10;
            player.LevelScore = level;
            player.X = GraphicsDevice.Viewport.Width / 2;
            player.Y = GraphicsDevice.Viewport.Height / 2;
            collectibles = new List<Collectible>(rnd.Next(3, 7 + level));
            for(int i = 0; i < collectibles.Capacity; i++)
            {
                collectibles.Add(new Collectible(collectableTexture, new Rectangle(rnd.Next(300), rnd.Next(300), 25, 25)));
            }
        }

        public void ResetGame()
        {
            level = 0;
            player.TotalScore = 0;
            NextLevel();
        }

        public void ScreeenWrap(GameObject objToWrap)
        {
            if (objToWrap.X == GraphicsDevice.Viewport.X)
                objToWrap.X = GraphicsDevice.Viewport.Width - objToWrap.Position.Width;

            if (objToWrap.X == GraphicsDevice.Viewport.Width)
                objToWrap.X = GraphicsDevice.Viewport.X;
        }
	}
}
