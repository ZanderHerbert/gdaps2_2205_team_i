using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //Manager class which manages enemies
    public class EnemyManager
    {
        //Fields
        private List<Enemy> enemies;

        //Constructor
        public EnemyManager(List<Enemy> enemies)
        {
            this.enemies = enemies;
        }

        //Methods

        //Updates all the enemies
        public void ManageEnemies(Game1 game, GameTime gameTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                //Updates enemies and handles collision detection
                enemies[i].Update(gameTime, game);
                if (enemies[i].HitBox.Intersects(game.Player.HitBox))
                {
                    game.Player.TakeDamage(enemies[i].ContactDamage);
                }

                // Remove enemies outside of the window
                if (enemies[i].Position.X < 0 - enemies[i].Position.Width * 2)
                {
                    enemies.RemoveAt(i);
                }
            }
        }

        //Draws all enemies
        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] is Boss)
                {
                    enemies[i].DrawLaser(_spriteBatch);
                }
                enemies[i].Draw(_spriteBatch);
            }
        }
    }
}
