using System;
using System.Collections.Generic;
using System.Text;

namespace Roboquatic
{
    class Upgrades
    {
        //Fields:

        private int upHealth;
        private int upSpeed;
        private int upDamage;

        //Properties:

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
           return health = health + upHealth;
        }

        public int SpeedUpgrade (int speed)
        {
            return speed = speed + upSpeed;
        }

        public int DamageUpgrade (int damage)
        {
            return damage = damage + upDamage;
        }

    }
}
