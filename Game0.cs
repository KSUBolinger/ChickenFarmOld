using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    public class Game0 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        private SnakeSprite snake;
        private SnakeSprite[] snakes;
        private ChickenSprite chicken;
        private EggSprite[] eggs;
        private InputManager inputManager;
        private SpriteFont bangers;
        private int eggsLeft;

        private Texture2D backgroundTexture;

        public Game0()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            chicken = new ChickenSprite();
            //snake = new SnakeSprite() { Position = new Vector2(400, 400), snakeDirection = SnakeDirection.Left };
            snakes = new SnakeSprite[]
            {
                new SnakeSprite(){ Position = new Vector2(400, 375), snakeDirection = SnakeDirection.Right },
                new SnakeSprite(){ Position = new Vector2(325, 100), snakeDirection = SnakeDirection.Right },
                new SnakeSprite(){ Position = new Vector2(200, 300), snakeDirection = SnakeDirection.Left },
                new SnakeSprite(){ Position = new Vector2(350, 200), snakeDirection = SnakeDirection.Left }
            };

            eggs = new EggSprite[]
            {
                new EggSprite(new Vector2(625, 250)),
                new EggSprite(new Vector2(100, 400)),
                new EggSprite(new Vector2(570, 50)),
                new EggSprite(new Vector2(350, 325))
            };
            eggsLeft = eggs.Length;

            inputManager = new InputManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //snake.LoadContent(Content);
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

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            chicken.Update(gameTime);
            //snake.Update(gameTime);
            foreach(var snake in snakes)
            {
                snake.Update(gameTime);
            }

            foreach(var egg in eggs)
            {
                if(!egg.Collected && egg.Bounds.CollidesWith(chicken.Bounds))
                {
                    chicken.color = Color.Red;
                    egg.Collected = true;
                    eggsLeft--;
                }
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

            chicken.Draw(gameTime, spriteBatch);
            //snake.Draw(gameTime, spriteBatch);
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
