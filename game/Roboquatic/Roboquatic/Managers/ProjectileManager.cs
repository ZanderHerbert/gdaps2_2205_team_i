using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //Manager class which manages projectiles
    public class ProjectileManager
    {
        //Fields
        private List<Projectile> projectiles;

        //Constructor
        public ProjectileManager(List<Projectile> projectiles)
        {
            this.projectiles = projectiles;
        }

        //Methods

        //Updates all the projectiles
        public void ManageProjectiles(Game1 game, GameTime gameTime)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(gameTime, game);
                if(projectiles[i].Hit)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
