using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roboquatic
{
    public class Upgrades
    {
        //Fields:

        private int upHealth = 1;
        private int upSpeed = 1;
        private int upDamage = 1;

        private bool contact = false;

        private Texture2D healthUpgradeImg;
        private Texture2D speedUpgradeImg;
        private Texture2D damageUpgradeImg;

        private Rectangle positionHealth;
        private Rectangle positionDamage;
        private Rectangle positionSpeed;

        //Properties:

        //Allows for get and set properties for the position of the upgrades:

        public Rectangle PositionHealth
        {
            get { return positionHealth; }
            set { positionHealth = value; }
        }

        public Rectangle PositionDamage
        {
            get { return positionDamage; }
            set { positionDamage = value; }
        }

        public Rectangle PositionSpeed
        {
            get { return positionSpeed; }
            set { positionSpeed = value; }
        }

        //Allows for get and set properties for the images of the upgrades:

        public Texture2D healthImage
        {
            get { return healthUpgradeImg; }
            set { healthUpgradeImg = value; }
        }

        public Texture2D speedImage
        {
            get { return speedUpgradeImg; }
            set { speedUpgradeImg = value; }
        }

        public Texture2D damageImage
        {
            get { return damageUpgradeImg; }
            set { damageUpgradeImg = value; }
        }

        //Constructor:

        public Upgrades(int upHealth, int upSpeed, int upDamage,
            Rectangle positionHealth, Rectangle positionDamage, Rectangle positionSpeed,
            Texture2D healthImage, Texture2D damageImage, Texture2D speedImage)
        {

            this.upHealth = upHealth;
            this.upSpeed = upSpeed;
            this.upDamage = upDamage;

            this.positionHealth = positionHealth;
            this.positionDamage = positionDamage;
            this.positionSpeed = positionSpeed;

            this.healthImage = healthImage;
            this.damageImage = damageImage;
            this.speedImage = speedImage;
        }

        //Method that implements the upgrades

        //Update method 

        public void Update(Game1 game)
        {
            //Checks if the player is in contact with the upgrade image

            if (positionHealth.Intersects(game.PlayerPosition))
            {
                //Adds the upgrade to the max health

                game.Player.MaxHP = game.Player.MaxHP + upHealth;

                //Adds the health

                game.Player.Health = game.Player.MaxHP;

                //Makes the upgrade null so it no longer appears on screen and changes contact to true

                game.Upgrade = null;

                contact = true;
            }
            else if (positionDamage.Intersects(game.PlayerPosition))
            {
                //Adds the upgrade ammount to the projectile damage

                game.Player.ProjectileDamage = game.Player.ProjectileDamage + upDamage;

                //Makes the upgrade null so it no longer appears on screen and changes contact to true

                game.Upgrade = null;

                contact = true;
            }
            else if (positionSpeed.Intersects(game.PlayerPosition))
            {
                //Adds the upgrade ammount to the player's speed
                game.Player.Speed = game.Player.Speed + upSpeed;

                //Makes the upgrade null so it no longer appears on screen and changes contact to true

                game.Upgrade = null;

                contact = true;
            }
        }

        // Draw the upgrades
        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Game1 game)
        {
            //Makes a variable to hold font

            
            //checks if the player reached the right time, cleared all enemies, and passed the checkpoint 
          
            if (time <= game.Time && !contact && game.Enemies.Count < 1)
            {
                //Draws the upgrades

                spriteBatch.Draw(healthImage, positionHealth, Color.White);
                spriteBatch.Draw(damageImage, positionDamage, Color.White);
                spriteBatch.Draw(speedImage, positionSpeed, Color.White);

                //Draws the text for each upgrade

                spriteBatch.DrawString(font, "Health", new Vector2(game.ViewportWidth / 2 - 24, 148), Color.White);
                spriteBatch.DrawString(font, "Damage", new Vector2(game.ViewportWidth / 2 - 224, 148), Color.White);
                spriteBatch.DrawString(font, "Speed", new Vector2(game.ViewportWidth / 2 + 176, 148), Color.White);


            }
        }
    }


}
