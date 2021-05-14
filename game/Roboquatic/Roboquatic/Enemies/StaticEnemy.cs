using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class StaticEnemy : Enemy
    {
        //BaseEnemy Constructor, uses Enemy constructor
        public StaticEnemy(Texture2D sprite, Rectangle position, int speed, Rectangle hitBox)
            : base(sprite, position, speed, hitBox)
        {
            health = 4;
            contactDamage = 2;
        }

        //Methods

        //Updates the enemy
        public override void Update(GameTime gameTime, Game1 game)
        {
            //Changes position of the enemy
            position.X -= speed;
            //Increments a hit timer if it was hit, so that it becomes invisible for 5 frames to indicate being hit
            if (hit)
            {
                hitTimer++;
                if (hitTimer == 5)
                {
                    hit = false;
                    hitTimer = 0;
                }
            }
            //Moves hitbox to its new position
            hitBox.X = position.X + (int)(2 * GlobalScalars.x);
            hitBox.Y = position.Y;
        }
    }
}
