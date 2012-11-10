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
    class Character
    {
        int[] position;
        Texture2D texture;
        Color color;
        Rectangle rect;

        public Character()
        {
            position = new int[2];
            
        }
        public void setPos(int x, int y)
        {
            position[0] = x;
            position[1] = y;
            rect.X = x;
            rect.Y = y;
        }
        public int getPosX()
        {
            return position[0];
        }
        public int getPosY()
        {
            return position[1];
        }
        public void setTexture(Texture2D theTexture)
        {
            texture = theTexture;
        }
        public void setColor(Color theColor)
        {
            color = theColor;
        }
        public void setRect(Rectangle theRect)
        {
            rect = theRect;
        }
        public Rectangle getRect()
        {
            return rect;
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, color);
        }
        public Color getColor()
        {
            return color;
        }
    }
}
