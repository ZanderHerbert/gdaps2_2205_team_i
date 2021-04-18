using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        private bool canMoveUp = false;
        private int screenMiddleY;
        private int ySpeed;

        public Boss(Texture2D sprite, Rectangle position, int speed, Texture2D projectileSprite, int projectileSpeed, int aimedProjectileSpeed, int homingProjectileSpeed, int health, Random rng, int ySpeed, int contactDamage)
            : base(sprite, position, speed)
        {
            this.projectileSprite = projectileSprite;
            this.projectileSpeed = projectileSpeed;
            this.aimedProjectileSpeed = aimedProjectileSpeed;
            this.homingProjectileSpeed = homingProjectileSpeed;
            this.health = health;
            this.rng = rng;
            this.ySpeed = ySpeed;
            this.contactDamage = contactDamage;
            timer = 0;
            shotPattern = 4;
            screenMiddleY = position.Y + position.Height / 2;
        }


        public override void Update(GameTime gametime, Game1 game)
        {
            timer++;
            if (timer % 360 == 359)
            {
                shotPattern = rng.Next(0, 2);
            }
            if (timer % 252 < 126 && canMoveUp)
            {
                position.Y += ySpeed;
            }
            else if (timer % 252 >= 126)
            {
                position.Y -= ySpeed;
            }

            if(timer % 250 == 63)
            {
                canMoveUp = true;
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
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 16, 32, 32)));
            }
            if (timer % 360 == 20)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 16, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 48, 32, 32)));
            }
            if (timer % 360 == 40)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 48, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 80, 32, 32)));
            }
            if (timer % 360 == 60)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 80, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 112, 32, 32)));
            }
            if (timer % 360 == 80)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 112, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 144, 32, 32)));
            }
            if (timer % 360 == 100)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 144, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 176, 32, 32)));
            }
            if (timer % 360 == 120)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 176, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 208, 32, 32)));
            }
            if (timer % 360 == 140)
            {
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 208, 32, 32)));
                game.Projectiles.Add(new EnemyProjectile(projectileSprite, projectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 240, 32, 32)));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 208, 32, 32), game.Player.Position));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 240, 32, 32), game.Player.Position));
            }
            if (timer % 360 == 180)
            {
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 208, 32, 32), game.Player.Position));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 240, 32, 32), game.Player.Position));
                game.Projectiles.Add(new HomingProjectile(projectileSprite, homingProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 16, 32, 32), 180));
            }
            if (timer % 360 == 220)
            {
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY + 208, 32, 32), game.Player.Position));
                game.Projectiles.Add(new AimedEnemyProjectile(projectileSprite, aimedProjectileSpeed, new Rectangle(position.X - 32, screenMiddleY - 240, 32, 32), game.Player.Position));
            }

        }
        public void ShotPatternTwo(int timer, Game1 game)
        {
            if(timer % 360 <= 40 && position.X > 0 - position.Width)
            {
                position.X -= 24;
            }
            else if (timer % 360 > 40 && position.X < game.GraphicsDevice.Viewport.Width - position.Width)
            {
                position.X += 3;
            }
        }
        public void ShotPatternThree(int timer, Game1 game)
        {

        }
    }
}
