using System;
using System.Collections.Generic;
using System.Text;

namespace Roboquatic
{
    class Boss 
    {
        public void Update(int timer, Random rng)
        {
            if(timer % 360 == 359)
            {
                switch(rng.Next(0, 3))
                {
                    case 0:
                        ShotPatternOne();
                        break;

                    case 1:
                        ShotPatternTwo();
                        break;

                    case 2:
                        ShotPatternThree();
                        break;
                }
            }
        }
        public void ShotPatternOne()
        {

        }
        public void ShotPatternTwo()
        {

        }
        public void ShotPatternThree()
        {

        }
    }
}
