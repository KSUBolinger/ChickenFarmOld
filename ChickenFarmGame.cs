using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        public ChickenFarmGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }
        
        /// <summary>
        /// initializes necessary sections of the game such as sprites
        /// </summary>
        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            chicken = new ChickenSprite();
            snakes = new SnakeSprite[]
            {
                new SnakeSprite((new Vector2(400, 375)), SnakeDirection.Right),
                new SnakeSprite((new Vector2(325, 100)), SnakeDirection.Right),
                new SnakeSprite((new Vector2(200, 300)), SnakeDirection.Left),
                new SnakeSprite((new Vector2(350, 200)), SnakeDirection.Left)
            };
            eggs = new EggSprite[]
            {
                new EggSprite(new Vector2(625, 250)),
                new EggSprite(new Vector2(100, 400)),
                new EggSprite(new Vector2(570, 50)),
                new EggSprite(new Vector2(350, 325))
            };
            eggsLeft = eggs.Length;

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
                }
            }
            foreach(var egg in eggs)
            {
                if(!egg.Collected && egg.Bounds.CollidesWith(chicken.Bounds))
                {
                    egg.Collected = true;
                    eggsLeft--;
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
            spriteBatch.DrawString(bangers, $"Eggs Left: {eggsLeft} ", new Vector2(20, 20), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
