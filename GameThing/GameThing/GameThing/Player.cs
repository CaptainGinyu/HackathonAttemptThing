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


namespace GameThing
{
    class Player:Character
    {
        int health;


        public Player()
        {
            this.setPos(10, 10);
            health = 1000;
            this.setRect(new Rectangle(getPosX(), getPosY(), 20, 20));
        }
        public Player(GraphicsDevice gd)
        {
            this.setPos(new Random().Next(0, gd.Viewport.Width - 150), new Random().Next(0, gd.Viewport.Height - 150));
            health = 100;
            this.setRect(new Rectangle(getPosX(), getPosY(), 20, 20));
        }
        public bool isDead()
        {
            return health <= 0;
        }
        public void hurt()
        {
            health--;
        }
        public void super_hurt()
        {
            health -= 5;
        }
        public int getHealth()
        {
            return health;
        }
    }
}
