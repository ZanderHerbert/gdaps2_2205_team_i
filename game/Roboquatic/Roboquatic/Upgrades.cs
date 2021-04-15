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
