using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using GameProject0.Screens;
using GameProject0.StateManagement;
using GameProject0;

namespace GameProject0
{
    public class ChickenFarmGame : Game
    {
        //various variables used to control aspects of the game 
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SnakeSprite[] snakes;
        private ChickenSprite chicken;
        private EggSprite[] eggs;
        private SpriteFont bangers;
        private int eggsLeft;
        private Texture2D backgroundTexture;

        private SoundEffect eggCollected;
        private SoundEffect collision;
        private Song backgroudMusic;

        private readonly ScreenManager screenManager;


        public ChickenFarmGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            IntialScreens();
        }

        private void IntialScreens()
        {
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }
        
        /// <summary>
        /// initializes necessary sections of the game such as sprites
        /// </summary>
        protected override void Initialize()
        {
            System.Random randPosition = new System.Random();
            // TODO: Add your initialization logic here
            chicken = new ChickenSprite();
            snakes = new SnakeSprite[]
            {
                new SnakeSprite((new Vector2(400, 375)), SnakeDirection.Right),
                new SnakeSprite((new Vector2(325, 100)), SnakeDirection.Right),
                new SnakeSprite((new Vector2(200, 300)), SnakeDirection.Left),
                new SnakeSprite((new Vector2(350, 200)), SnakeDirection.Left)
            };
            //eggs = new EggSprite[]
            //{
            //    new EggSprite(new Vector2(625, 250)),
            //    new EggSprite(new Vector2(100, 400)),
            //    new EggSprite(new Vector2(570, 50)),
            //    new EggSprite(new Vector2(350, 325))
            //};
            //eggsLeft = eggs.Length;

            Vector2 eggPosition;
            eggs = new EggSprite[4];
            for(int i = 0; i < eggs.Length; i++)
            {
                eggPosition = new Vector2((float)randPosition.NextDouble() * GraphicsDevice.Viewport.Width, (float)randPosition.NextDouble() * GraphicsDevice.Viewport.Height);
                if(eggPosition.X < 25 && eggPosition.Y < 40)
                {
                    eggPosition.X += 40;
                    eggPosition.Y += 60;
                }
                eggs[i] = new EggSprite(eggPosition);
            }

            base.Initialize();
        }

        /// <summary>
        /// loads the content needed for the game
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            backgroundTexture = Content.Load<Texture2D>("Plains");
            chicken.LoadContent(Content);
            foreach (var snake in snakes)
            {
                snake.LoadContent(Content);
            }
            foreach(var egg in eggs)
            {
                egg.LoadContent(Content);
            }
            bangers = Content.Load<SpriteFont>("bangers");
            eggCollected = Content.Load<SoundEffect>("EggPickup");
            collision = Content.Load<SoundEffect>("Collision");
            backgroudMusic = Content.Load<Song>("BackgroundCountry");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroudMusic);
        }

        /// <summary>
        /// updates the game as it's played
        /// </summary>
        /// <param name="gameTime">the game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            chicken.Update(gameTime);
            foreach(var snake in snakes)
            {
                snake.Update(gameTime);
                if (chicken.Bounds.CollidesWith(snake.Bounds))
                {
                    chicken.Reset();
                    foreach(var egg in eggs)
                    {
                        if (egg.Collected)
                        {
                            egg.Collected = false;
                            eggsLeft++;
                        }     
                    }
                    collision.Play();
                }
            }
            foreach(var egg in eggs)
            {
                if(!egg.Collected && egg.Bounds.CollidesWith(chicken.Bounds))
                {
                    egg.Collected = true;
                    eggsLeft--;
                    eggCollected.Play();
                }
            }
            
            if(eggsLeft == 0)
            {
                chicken.Reset();
                foreach(var egg in eggs)
                {
                    egg.Collected = false;
                    eggsLeft++;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// draws the sprites, backgrounds, and text needed for the game 
        /// </summary>
        /// <param name="gameTime">the game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            chicken.Draw(gameTime, spriteBatch);
            foreach(var snake in snakes)
            {
                snake.Draw(gameTime, spriteBatch);
            }
            foreach(var egg in eggs)
            {
                egg.Draw(gameTime, spriteBatch);
            }
            spriteBatch.DrawString(bangers, $"Eggs Left: {eggsLeft} ", new Vector2(15, 35), Color.Black);
            spriteBatch.DrawString(bangers, $"Use 'ESC' to exit game", new Vector2(15, 5), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
