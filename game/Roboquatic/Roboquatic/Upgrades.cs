using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roboquatic
{
    class Upgrades
    {
        //Fields:

        private int upHealth = 1;
        private int upSpeed = 1;
        private int upDamage = 1;

        private Texture2D healthUpgradeImg;
        private Texture2D speedUpgradeImg;
        private Texture2D damageUpgradeImg;
        private Rectangle position;

        //Properties:

        //Allows for read only properties for the actual upgrades:
        public int UpHealth
        {
            get { return upHealth; }
        }

        public int UpSpeed
        {
            get { return upSpeed; }
        }

        public int UpDamage
        {
            get { return upDamage; }
        }

        //Allows for get and set properties for the images of the upgrades:
        public Rectangle Position
        {
            get { return position; }
            set { position = value;}
        }

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

        public Texture2D damgeImage
        {
            get { return damageUpgradeImg; }
            set { damageUpgradeImg = value; }
        }

        //Constructor:

        public Upgrades (int upHealth, int upSpeed, int upDamage)
        {
            this.upHealth = upHealth;
            this.upSpeed = upSpeed;
            this.upDamage = upDamage;
        }

        //Method that implements the upgrades

        public int HealthUpgrade (int health)
        {
           return health + upHealth;
        }

        public int SpeedUpgrade (int speed)
        {
            return speed + upSpeed;
        }

        public int DamageUpgrade (int damage)
        {
            return damage + upDamage;
        }


    }
}
