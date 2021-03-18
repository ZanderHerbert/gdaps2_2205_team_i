using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    public class EnemyManager
    {
        private List<Enemy> enemies;

        public EnemyManager(List<Enemy> enemies)
        {
            this.enemies = enemies;
        }

        //Updates all the enemies
        public void ManageEnemies(Game1 game, GameTime gameTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime, game);
            }
        }

        //Checks if any enemies are in contact with the player
        public bool PlayerContact(Rectangle playerPosition)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Position.Intersects(playerPosition))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
