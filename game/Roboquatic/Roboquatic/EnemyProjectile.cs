using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //Enemy projectile which only moves left
    class EnemyProjectile : Projectile
    {
        //EnemyProjectile constructor, uses its parent Projectile constructor
        public EnemyProjectile(Texture2D sprite, int speed, Rectangle position)
            : base(sprite, speed, position)
        {
            damage = 1;
        }

        //Checks if the projectile is in contact with the player
        public override bool PlayerContact(Player player)
        {
            if((position.Y + position.Height > player.Position.Y && position.Y < player.Position.Y + player.Position.Height) && (position.X + position.Width > player.Position.X && position.X < player.Position.X + player.Position.Width))
            {
                return true;
            }
            return false;
        }
    }
}
