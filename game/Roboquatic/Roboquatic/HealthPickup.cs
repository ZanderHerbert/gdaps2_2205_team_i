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
        private Rectangle position;
        private Texture2D pickupSprite;

        public HealthPickup(Rectangle position, Game1 game)
        {
            this.position = position;
            pickupSprite = game.Content.Load<Texture2D>("UpgradeHealth");
        }

        public bool Update(Player player)
        {
            if (position.Intersects(player.HitBox))
            {
                if(player.Health < player.MaxHP)
                {
                    player.Health++;
                }
                return true;
            }
            position.X -= 1;
            return false;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(pickupSprite, position, Color.White);
        }
    }
}
