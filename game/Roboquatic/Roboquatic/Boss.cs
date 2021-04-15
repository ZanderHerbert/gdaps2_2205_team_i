using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class Boss : Enemy
    {
        private int shotPattern;
        private Texture2D projectileSprite;
        private int projectileSpeed;
        private int aimedProjectileSpeed;
        private int homingProjectileSpeed;
        private int timer;
        private Random rng;

        public Boss(Texture2D sprite, Rectangle position, int speed, Texture2D projectileSprite, int projectileSpeed, int aimedProjectileSpeed, int homingProjectileSpeed, int health, Random rng)
            : base(sprite, position, speed)
        {
            this.projectileSprite = projectileSprite;
            this.projectileSpeed = projectileSpeed;
            this.aimedProjectileSpeed = aimedProjectileSpeed;
            this.homingProjectileSpeed = homingProjectileSpeed;
            this.health = health;
            this.rng = rng;
            timer = 300;
            shotPattern = 4;
        }


        public override void Update(GameTime gametime, Game1 game)
        {
            timer++;
            if (timer % 360 == 359)
            {
                shotPattern = rng.Next(0, 3);
                shotPattern = 0;
            }

            switch(shotPattern)
            {
                case 0:
                    ShotPatternOne(timer, game);
                    break;

                case 1:
                    ShotPatternTwo(timer, game);
                    break;

                 case 2:
                    ShotPatternThree(timer, game);
                    break;

                default:
                    break;
            }
            if (hit)
            {
                hitTimer++;
                if (hitTimer == 5)
                {
                    hit = false;
                    hitTimer = 0;
                }
            }
        }
        public void ShotPatternOne(int timer, Game1 game)
        {
            if(timer % 360 == 0)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 16, 32, 32)));
            }
            if (timer % 360 == 20)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 + 16, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 48, 32, 32)));
            }
            if (timer % 360 == 40)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 + 48, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 80, 32, 32)));
            }
            if (timer % 360 == 60)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 + 80, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 112, 32, 32)));
            }
            if (timer % 360 == 80)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 + 112, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 144, 32, 32)));
            }
            if (timer % 360 == 100)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 + 144, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 176, 32, 32)));
            }
            if (timer % 360 == 120)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 + 176, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 208, 32, 32)));
            }
            if (timer % 360 == 140)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 + 208, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 240, 32, 32)));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 + 208, 32, 32), game.Player.Position));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 240, 32, 32), game.Player.Position));
            }
            if (timer % 360 == 180)
            {
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 + 208, 32, 32), game.Player.Position));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 240, 32, 32), game.Player.Position));
                game.Projectiles.Add(new HomingProjectile(projectileSprite, homingProjectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 16, 32, 32), 180));
            }
            if (timer % 360 == 220)
            {
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 + 208, 32, 32), game.Player.Position));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, position.Y + position.Height / 2 - 240, 32, 32), game.Player.Position));
            }

        }
        public void ShotPatternTwo(int timer, Game1 game)
        {

        }
        public void ShotPatternThree(int timer, Game1 game)
        {

        }
    }
}
