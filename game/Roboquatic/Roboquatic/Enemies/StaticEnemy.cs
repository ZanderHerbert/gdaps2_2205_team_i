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
        //Declaring fields
        private Texture2D projectileSprite;

        //BaseEnemy Constructor, uses Enemy constructor
        public StaticEnemy(Texture2D sprite, Rectangle position, int speed)
            : base(sprite, position, speed)
        {
            health = 4;
            contactDamage = 2;
        }

        //Updates the enemy
        //
        //Moves the enemy, then checks the enemy position to see if it needs to change the speed of the enemy,
        //increments the shooting timer, checks if the enemy can shoot, and shoots a projectile if it can.
        public override void Update(GameTime gameTime, Game1 game)
        {
            position.X -= speed;
        }
    }
}
