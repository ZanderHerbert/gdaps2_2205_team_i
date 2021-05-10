using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //A projectile which is shot by the player
    class PlayerProjectile : Projectile
    {
        //PlayerProjectile constructor which uses the Projectile constructor
        public PlayerProjectile(Texture2D sprite, int speed, Rectangle position, Player player)
            : base(sprite, speed, position)
        {
            damage = player.ProjectileDamage;
        }

        //Methods

        //Checks if the projectile is in contact with an enemy, and returns that enemy
        public Enemy EnemyContact(List<Enemy> enemies)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if (position.Intersects(enemies[i].Position))
                {
                    return enemies[i];
                }
            }
            return null;

        }

        //Updates the projectile
        //
        //Checks if the projectile hit an enemy, if so damages that enemy, removes that enemy from the list of enemies
        //in the game, sets hit to true, denoting that the projectile made contact with something, and then moves the
        //projectile
        public override void Update(GameTime gameTime, Game1 game)
        {
            Enemy hitEnemy = EnemyContact(game.Enemies);
            if (hitEnemy != null)
            {
                hitEnemy.TakeDamage(damage);
                if (hitEnemy.Health <= 0)
                {
                    if(game.RNG.Next(0,20) == 0)
                    {
                        game.Pickups.Add(new HealthPickup(new Rectangle(hitEnemy.Position.X, hitEnemy.Position.Y, 32, 32), game));
                    }
                    game.Enemies.Remove(hitEnemy);
                }
                hit = true;
            }
            if(position.X > game.ViewportWidth)
            {
                hit = true;
            }
            position.X += speed;
        }
    }
}
