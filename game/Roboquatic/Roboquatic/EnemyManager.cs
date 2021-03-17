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
        //Updates all the enemies
        public void ManageEnemies(Game1 game, GameTime gameTime, List<Enemy> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gameTime, game);
                
            }
        }
    }
}
