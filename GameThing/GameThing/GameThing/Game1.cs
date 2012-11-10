using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace GameThing
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D defaultTexture;

        Player you;
        Character enemy;

        Player you2;
        Character enemy2;
        int theSeconds;

        Stopwatch timerA;
        Stopwatch timerB;
        Dictionary<Keys, string> pressChallDict;
        Dictionary<int, Keys> KeyPos;

        bool pressChall;
        int countLength;
        Keys currKey;
        bool pressCorrect;

        bool GameOver;

        String pressChallString;

        int timeSeconds;

        public Game1()
        {
            this.Window.Title = "The Super Special Awesome Game";
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
            this.IsMouseVisible = true;
            GameOver = false;
            pressChall = false;
            timeSeconds = 0;
            timerA = new Stopwatch();
            timerB = new Stopwatch();
            theSeconds = 0;
            pressCorrect = false;
            currKey = Keys.LeftShift;

            pressChallDict = new Dictionary<Keys,string>();

            pressChallDict.Add(Keys.LeftShift, "left shift");

            pressChallDict.Add(Keys.RightShift, "right shift");

            pressChallDict.Add(Keys.Space, "the space bar");
            pressChallDict.Add(Keys.Tab, "tab");


            KeyPos = new Dictionary<int,Keys>();
            countLength = 0;
            pressChallString = "";
            foreach (KeyValuePair<Keys, string> item in pressChallDict)
            {
                KeyPos.Add(countLength, item.Key);
                countLength++;
            }
            
        }




        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("gamefont");
            defaultTexture = new Texture2D(GraphicsDevice, 1, 1);
            defaultTexture.SetData(new Color[] { Color.White });

            you = new Player(GraphicsDevice);
            you.setTexture(defaultTexture);
            you.setColor(Color.Red);
            enemy = new Character();
            enemy.setPos(you.getPosX() + 150, you.getPosY() + 150);
            enemy.setTexture(defaultTexture);
            enemy.setRect(new Rectangle(enemy.getPosX(), enemy.getPosY(), 20, 20));
            enemy.setColor(Color.Green);

            you2 = new Player(GraphicsDevice);
            you2.setTexture(defaultTexture);
            you2.setColor(Color.Turquoise);
            while (you2.getRect().Intersects(you.getRect()))
            {
                you2.setPos(new Random().Next(0, GraphicsDevice.Viewport.Width - 150), new Random().Next(0, GraphicsDevice.Viewport.Height - 150));
            }
            enemy2 = new Character();
            enemy2.setPos(you.getPosX() + 150, you.getPosY() + 150);
            enemy2.setTexture(defaultTexture);
            enemy2.setRect(new Rectangle(enemy2.getPosX(), enemy2.getPosY(), 20, 20));
            enemy2.setColor(Color.Yellow);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (!GameOver)
            {
                //keys for you
                bool upPressed = Keyboard.GetState().IsKeyDown(Keys.Up);
                bool downPressed = Keyboard.GetState().IsKeyDown(Keys.Down);
                bool leftPressed = Keyboard.GetState().IsKeyDown(Keys.Left);
                bool rightPressed = Keyboard.GetState().IsKeyDown(Keys.Right);

                //keys to you2
                bool wPressed = Keyboard.GetState().IsKeyDown(Keys.W);
                bool aPressed = Keyboard.GetState().IsKeyDown(Keys.A);
                bool sPressed = Keyboard.GetState().IsKeyDown(Keys.S);
                bool dPressed = Keyboard.GetState().IsKeyDown(Keys.D);

                MouseState mouse = Mouse.GetState();

                timerA.Start();
                
                //Movement for you
                if (leftPressed && you.getPosX() != 0)
                {
                    you.setPos(you.getPosX() - 1, you.getPosY());
                }
                else if (rightPressed && you.getPosX() != GraphicsDevice.Viewport.Width - you.getRect().Width)
                {
                    you.setPos(you.getPosX() + 1, you.getPosY());
                }
                else if (upPressed && you.getPosY() != 0)
                {
                    you.setPos(you.getPosX(), you.getPosY() - 1);
                }
                else if (downPressed && you.getPosY() != GraphicsDevice.Viewport.Height - you.getRect().Height)
                {
                    you.setPos(you.getPosX(), you.getPosY() + 1);
                }

                //Movement for you2
                if (aPressed && you2.getPosX() != 0)
                {
                    you2.setPos(you2.getPosX() - 1, you2.getPosY());
                }
                else if (dPressed && you2.getPosX() != GraphicsDevice.Viewport.Width - you2.getRect().Width)
                {
                    you2.setPos(you2.getPosX() + 1, you2.getPosY());
                }
                else if (wPressed && you2.getPosY() != 0)
                {
                    you2.setPos(you2.getPosX(), you2.getPosY() - 1);
                }
                else if (sPressed && you2.getPosY() != GraphicsDevice.Viewport.Height - you2.getRect().Height)
                {
                    you2.setPos(you2.getPosX(), you2.getPosY() + 1);
                }

                //Change Color on mouse-over
                if (you.getRect().Contains(mouse.X, mouse.Y))
                {
                    you.setColor(Color.Blue);
                }
                else
                {
                    you.setColor(Color.Red);
                }

                //Intersection
                if (you.getRect().Intersects(enemy.getRect()))
                {
                    you.hurt();
                }
                if (you.getRect().Intersects(enemy2.getRect()))
                {
                    you.hurt();
                }
                if (you2.getRect().Intersects(enemy.getRect()))
                {
                    you2.hurt();
                }
                if (you2.getRect().Intersects(enemy2.getRect()))
                {
                    you2.hurt();
                }
                if (you.getRect().Intersects(you2.getRect()))
                {
                    you.hurt();
                    you2.hurt();
                }

                //Enemy movement
                int enemy_dx = 0;
                int enemy_dy = 0;
                int rise = you.getPosY() - enemy.getPosY();
                int run = you.getPosX() - enemy.getPosX();

                

                if (timeSeconds % 2 == 0)
                {
                    if (rise > 0)
                    {
                        enemy_dy = 1;
                    }
                    else if (rise < 0)
                    {
                        enemy_dy = -1;
                    }

                    if (run > 0)
                    {
                        enemy_dx = 1;
                    }
                    else if (run < 0)
                    {
                        enemy_dx = -1;
                    }

                    enemy.setPos(enemy.getPosX() + enemy_dx, enemy.getPosY() + enemy_dy);
                }

                //Enemy2 movement
                int enemy2_dx = 0;
                int enemy2_dy = 0;
                int rise2 = you2.getPosY() - enemy2.getPosY();
                int run2 = you2.getPosX() - enemy2.getPosX();

                

                if (timeSeconds % 5 == 0)
                {
                    if (rise2 > 0)
                    {
                        enemy2_dy = 2;
                    }
                    else if (rise2 < 0)
                    {
                        enemy2_dy = -2;
                    }

                    if (run2 > 0)
                    {
                        enemy2_dx = 2;
                    }
                    else if (run2 < 0)
                    {
                        enemy2_dx = -2;
                    }

                    enemy2.setPos(enemy2.getPosX() + enemy2_dx, enemy2.getPosY() + enemy2_dy);
                }

                //Press Challenge
                if (Convert.ToInt32(timerA.ElapsedMilliseconds / 1000) != 0 && Convert.ToInt32(timerA.ElapsedMilliseconds / 1000) % 15 == 0)
                {

                    pressChall = true;
                    int index = new Random().Next(0, countLength);
                    currKey = KeyPos[index];
                    pressChallString = pressChallDict[currKey];
                    timerA.Restart();
                    
                    timerB.Start();
                }

                if (pressChall && Keyboard.GetState().IsKeyDown(currKey))
                {
                    pressCorrect = true;
                }
                
                //When Dead
                if (you.isDead() || you2.isDead())
                {
                    GameOver = true;
                }



                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!GameOver)
            {
                GraphicsDevice.Clear(Color.LightGray);
                timeSeconds = Convert.ToInt32(gameTime.TotalGameTime.TotalSeconds);
                // TODO: Add your drawing code here
                base.Draw(gameTime);
                spriteBatch.Begin();

                you.draw(spriteBatch);
                you2.draw(spriteBatch);
                enemy.draw(spriteBatch);
                enemy2.draw(spriteBatch);
                spriteBatch.DrawString(font, "Health of Block 1: " + you.getHealth(), new Vector2(10, 10), Color.Black);
                spriteBatch.DrawString(font, "Health of Block 2: " + you2.getHealth(), new Vector2(250, 10), Color.Black);
                if (pressChall)
                {
                    theSeconds = (Convert.ToInt32(timerB.ElapsedMilliseconds / 1000));
                    if (pressCorrect)
                    {
                        pressChall = false;
                        timerB.Reset();
                        pressCorrect = false;
                    }
                    else if (theSeconds == 3)
                    {
                        pressChall = false;
                        timerB.Reset();


                        you.super_hurt();
                        you2.super_hurt();
                    }

                    spriteBatch.DrawString(font, "Press " + pressChallString + " within " + (3 - theSeconds) + " seconds!", new Vector2(10, GraphicsDevice.Viewport.Height - 25), Color.Black);
                }
                spriteBatch.End();
            }
            else
            {
                GraphicsDevice.Clear(Color.Blue);
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Game over, you fool!", new Vector2(GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 4), Color.White);
                spriteBatch.End();
            }
        }
    }
}
