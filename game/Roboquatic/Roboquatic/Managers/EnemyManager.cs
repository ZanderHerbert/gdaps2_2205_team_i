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
                if (PlayerContact(game.Player.Position))
                {
                    game.Player.TakeDamage(enemies[i].ContactDamage);
                }

                // Remove enemies outside of the window
                if (enemies[i].Position.X < 0 - enemies[i].Position.Width)
                {
                    enemies.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(_spriteBatch);
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
