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

        /// <summary> The current state of the game </summary>
		GameState gameState;
        /// <summary> The player </summary>
		Player player;
        /// <summary> A list storing all the collectibles on the level </summary>
		List<Collectible> collectibles;
        /// <summary> Current level </summary>
		int level = 0;
        /// <summary> Current keyboard output </summary>
		KeyboardState kbState;
        /// <summary> Keyboard output of the previous frame </summary>
		KeyboardState previousKBState;
        /// <summary> The game time </summary>
		float timer;
        /// <summary> Has the player found all the collectables </summary>
		bool allCollectablesFound;

		Texture2D collectableTexture;

		SpriteFont font;

        Texture2D enemyTexture;
        List<Enemy> enemies;

        Random rnd = new Random();
        /// <summary> Modifies the size of the player </summary>
		private int playerSize = 4;
        /// <summary> Modifies the speed of the player </summary>
		private int playerSpeed = 2;

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
			player = new Player(null, null, new Rectangle(20, 20, 10, 10));
			collectibles = new List<Collectible>();
            enemies = new List<Enemy>();
            gameState = GameState.Menu;
			timer = 15;
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

			player.Texture = Content.Load<Texture2D>("terrariaguide");
			player.FlippedImgage = Content.Load<Texture2D>("terrariaguideleft");
			player.Rectangle = new Rectangle(player.X, player.Y, 10 * playerSize, 12 * playerSize);

            enemyTexture = Content.Load<Texture2D>("slime");
			collectableTexture = Content.Load<Texture2D>("coin");

			font = Content.Load<SpriteFont>("aria");

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

            //Updates Keyboard states
			previousKBState = kbState;
			kbState = Keyboard.GetState();

			//If you press enter on the Main menu...
			if(gameState == GameState.Menu && kbState.IsKeyDown(Keys.Enter))
			{
				//...Start the game
				ResetGame();
				gameState = GameState.Game;
			}

			//If the game is over and you press enter...
			else if(gameState == GameState.GameOver && kbState.IsKeyDown(Keys.Enter))
				//...go to the main menu
				gameState = GameState.Menu;

			//End game when timer runs out
			else if(timer <= 0)
				gameState = GameState.GameOver;

			//When game is running...
			else if(gameState == GameState.Game)
			{

				//...Count down the timer
				timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				
				//...Move player based on input
				if(kbState.IsKeyDown(Keys.A))
				{
					player.X -= playerSpeed;
					player.FacingLeft = true;
				}
				else if(kbState.IsKeyDown(Keys.D))
				{
					player.X += playerSpeed;
					player.FacingLeft = false;
				}
				if(kbState.IsKeyDown(Keys.W))
					player.Y -= playerSpeed;
				else if(kbState.IsKeyDown(Keys.S))
					player.Y += playerSpeed;


				allCollectablesFound = true;

				//Check for collectible collions
				foreach(Collectible collectible in collectibles)
				{
					if(collectible.CheckCollision(player))
					{
						collectible.Active = false;
						player.LevelScore += 1;
					}

					if(collectible.Active)
						allCollectablesFound = false;
				}
				//If none of the collectables are active, start the next level
				if(allCollectablesFound)
					NextLevel();
                //Check to screen wrap player
				ScreenWrap(player);

                foreach(Enemy enemy in enemies)
                {
                    enemy.CheckCollision(player);
                    enemy.Move(rnd);
                    ScreenWrap(enemy);
                }
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();

			switch(gameState)
			{
				//GraphicsDevice.Viewport.(Width/Height) gets the size of the window
				//font.MesaureString(string) gets a Vector2D of the pixel size of the text
				//Dividing these by 2 centers the text
				case GameState.Menu:
					//Menu Text
					spriteBatch.DrawString(font,
					"Press Enter to Start",
					new Vector2((GraphicsDevice.Viewport.Width - font.MeasureString("Press Enter to Start").X )/ 2,
					(GraphicsDevice.Viewport.Height - font.MeasureString("Press Enter to Start").Y) / 2),
					Color.White);
					break;

				case GameState.GameOver:
					//Game over text and total score
					spriteBatch.DrawString(font,
					"Game Over\nTotal Score: " + player.TotalScore + "\nFinal Level: " + level,
					new Vector2((GraphicsDevice.Viewport.Width - font.MeasureString("Game Over\nTotal Score: " + player.TotalScore + "\nFinal Level: " + level).X) / 2,
					(GraphicsDevice.Viewport.Height - font.MeasureString("Game Over\nTotal Score: " + player.TotalScore + "\nFinal Level: " + level).Y) / 2),
					Color.White);

					//Instructions
					spriteBatch.DrawString(font,
						"Press enter to go back to main menu",
						new Vector2((GraphicsDevice.Viewport.Width - font.MeasureString("Press enter to go back to main menu").X) / 2,
						(GraphicsDevice.Viewport.Height - font.MeasureString("Game Over").Y)),
						Color.White);
					break;

				case GameState.Game:
                    //Draw all the enemies
                    foreach(Enemy enemy in enemies)
                    {
                        enemy.Draw(spriteBatch);
                    }

					//Draw all the collectables
					foreach(Collectible collectible in collectibles)
					{
						collectible.Draw(spriteBatch);
					}
					//Draw the player
					player.Draw(spriteBatch);

					//Draw the level text
					spriteBatch.DrawString(font, "Level: " + level + "\nTime: " + (string.Format("{0:0.00}", timer)), Vector2.Zero, Color.White);
					spriteBatch.DrawString(font, "Score: " + player.LevelScore, 
						new Vector2((GraphicsDevice.Viewport.Width - font.MeasureString("Score: " + player.TotalScore).X) / 2, 0), Color.White);
					break;
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}

		/// <summary>
		/// Resets the player, creates new collectables, and increases the level count
		/// </summary>
		void NextLevel()
		{
			allCollectablesFound = false;
			level++;    //Increment the level
			timer = 15; //Reset the timer
			player.TotalScore += player.LevelScore; //Add level score to total
			player.LevelScore = 0;  //Reset level score
			player.X = (GraphicsDevice.Viewport.Width - player.Rectangle.Width) / 2;    //Center the player
			player.Y = (GraphicsDevice.Viewport.Height - player.Rectangle.Width) / 2;

			collectibles = new List<Collectible>(rnd.Next(5, 7) + level);   //Generate new colletables

			for(int i = 0; i < collectibles.Capacity; i++)
			{
				collectibles.Add(new Collectible(collectableTexture,
					new Rectangle(rnd.Next(GraphicsDevice.Viewport.Width - 30), rnd.Next(GraphicsDevice.Viewport.Height - 30), 25, 25)));
			}

            enemies.Clear();
            int enemyNumber = rnd.Next(level) + (level/2);
            for(int i = 0; i < enemyNumber; i++)
            {
                enemies.Add(new Enemy(enemyTexture,
                    new Rectangle(rnd.Next(GraphicsDevice.Viewport.Width - 50), rnd.Next(GraphicsDevice.Viewport.Height - 50), 45, 35), rnd.Next(3), 15));
            }
		}

        /// <summary>
        /// Sets level and total score to 0 and starts up at level 1
        /// </summary>
		void ResetGame()
		{
			level = 0;
			player.TotalScore = 0;
			NextLevel();
		}

        /// <summary>
        /// Checks to see if the GameObject is outside the boundries and sets it position to the other side of the screen
        /// </summary>
        /// <param name="objToWrap"></param>
		void ScreenWrap(GameObject objToWrap)
		{
			if(objToWrap.X == GraphicsDevice.Viewport.X)
				objToWrap.X = GraphicsDevice.Viewport.Width - objToWrap.Rectangle.Width;

			if(objToWrap.X == GraphicsDevice.Viewport.Width)
				objToWrap.X = GraphicsDevice.Viewport.X;

			if(objToWrap.Y == GraphicsDevice.Viewport.Y)
				objToWrap.Y = GraphicsDevice.Viewport.Height - objToWrap.Rectangle.Height;

			if(objToWrap.Y == GraphicsDevice.Viewport.Height)
				objToWrap.Y = GraphicsDevice.Viewport.Y;
		}

		bool SingleKeyPress(Keys key)
		{
			return (kbState.IsKeyDown(key) && !previousKBState.IsKeyDown(key));

		}
	}
}
