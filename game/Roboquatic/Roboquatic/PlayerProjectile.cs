using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class PlayerProjectile : Projectile
    {
        public PlayerProjectile(Texture2D sprite, int speed, Rectangle position, Player player)
            : base(sprite, speed, position)
        {
            damage = player.ProjectileDamage;
        }
        public override Enemy EnemyContact(List<Enemy> enemies)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if ((position.Y + position.Height > enemies[i].Position.Y && position.Y < enemies[i].Position.Y + enemies[i].Position.Height) && (position.X + position.Width > enemies[i].Position.X && position.X < enemies[i].Position.X + enemies[i].Position.Width))
                {
                    return enemies[i];
                }
            }
            return null;

        }
    }
}
