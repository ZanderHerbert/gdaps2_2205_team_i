using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
   public class HealthPickup
    {
        //Fields:

        private Rectangle position;
        private Texture2D pickupSprite;

        //Constructor:

        public HealthPickup(Rectangle position, Game1 game)
        {
            this.position = position;
            pickupSprite = game.Content.Load<Texture2D>("UpgradeHealth");
        }

        //Methods:

        public bool Update(Player player)
        {
            //Checks if the player is intersecting the pick up 
            if (position.Intersects(player.HitBox))
            {
                //Checks if the players health is less than their max health

                if(player.Health < player.MaxHP)
                {
                    //gives the player one more health

                    player.Health++;
                }
                return true;
            }
            position.X -= 1;
            return false;
        }

        //Draws the pick up

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(pickupSprite, position, Color.White);
        }
    }
}
