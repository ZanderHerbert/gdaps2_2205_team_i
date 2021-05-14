using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    //Controls the Heads Up Display
    class Hud
    {
        //Fields
        private Texture2D healthSheet;
        private Texture2D progressBar;
        private Texture2D progressBarFiller;
        private int rightHealthOffset;
        private bool onceThrough;
        private Texture2D checkpointFlag;
        private Texture2D playerTexture;
        private Texture2D bossTexture;
        private Texture2D progressText;

        //Constructor
        public Hud(Game1 game)
        {
            healthSheet = game.Content.Load<Texture2D>("HealthSheet");

            progressBar = game.Content.Load<Texture2D>("ProgressBar");
            progressBarFiller = game.Content.Load<Texture2D>("ProgressFiller");

            checkpointFlag = game.Content.Load<Texture2D>("CheckpointFlag");

            bossTexture = game.Content.Load<Texture2D>("BossSprite1");
            playerTexture = game.Content.Load<Texture2D>("PlayerFishSprite");
            progressText = game.Content.Load<Texture2D>("Progress");

            rightHealthOffset = 0;
            onceThrough = false;
        }

        //Methods

        //Draws all HUD related sprites, including the health bar and the progress bar, and fills each based on the Player's positiona dn health
        public void Draw(SpriteBatch sb, Player player, int screenHeight, int checkpointsCrossed, double percent, Upgrades upgrade)
        {

            sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(20, 20, 93, 54)), new Rectangle(0, 0, 155, 90), Color.White);
            for(int i = 0; i <= player.MaxHP - 7; i++)
            {
                sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(113 + i * 21, 20, 21, 54)), new Rectangle(155, 0, 35, 90), Color.White);
            }
            rightHealthOffset = 21 * (player.MaxHP - 6);
            sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(113 + rightHealthOffset, 20, 42, 54)), new Rectangle(190, 0, 70, 90), Color.White);


            if(player.Health == 1)
            {
                sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(32, 41, 15, 18)), new Rectangle(20, 95, 25, 30), Color.White);
            }
            else if(player.Health == 2)
            {
                sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(32, 41, 36, 18)), new Rectangle(20, 125, 60, 30), Color.White);
            }
            else if (player.Health == 3)
            {
                sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(32, 41, 57, 18)), new Rectangle(20, 160, 95, 30), Color.White);
            }
            else if (player.Health == 4)
            {
                sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(32, 41, 78, 18)), new Rectangle(20, 195, 130, 30), Color.White);
            }
            else if (player.Health == 5)
            {
                sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(32, 41, 99, 18)), new Rectangle(20, 230, 165, 30), Color.White);
            }
            else if (player.Health == 6)
            {
                sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(32, 41, 120, 18)), new Rectangle(20, 265, 200, 30), Color.White);
            }
            else
            {
                sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(32, 41, 120, 18)), new Rectangle(20, 300, 200, 30), Color.White);
                rightHealthOffset = 0;
                for(int i = 0; i < player.Health - 7; i++)
                {
                    sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(155 + i * 21, 41, 18, 18)), new Rectangle(50, 300, 30, 30), Color.White);
                    rightHealthOffset = 21 * (i + 1);
                }
                if(player.Health == player.MaxHP)
                {
                    sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(155 + rightHealthOffset, 41, 18, 18)), new Rectangle(225, 300, 30, 30), Color.White);
                }
                else
                {
                    sb.Draw(healthSheet, GlobalScalars.scaleRect(new Rectangle(155 + rightHealthOffset, 41, 18, 18)), new Rectangle(50, 300, 30, 30), Color.White);
                }
            }

           
            sb.Draw(progressBar, GlobalScalars.scaleRect(new Rectangle(280, screenHeight - 38, 428, 20)), new Rectangle(2, 59, 285, 20), Color.White);
            sb.Draw(progressText, GlobalScalars.scaleRect(new Rectangle(60, screenHeight - 45, 144, 30)), new Rectangle(0, 0, 240, 50), Color.White);
            for(int i = 0; i <= checkpointsCrossed; i++)
            {
                if(i == checkpointsCrossed && checkpointsCrossed != 3)
                {
                    if(upgrade == null)
                    {
                        sb.Draw(progressBarFiller, GlobalScalars.scaleRect(new Rectangle(288 + 105 * i, screenHeight - 33, (int)(98 * percent), 10)), new Rectangle(0, 0, 65, 10), Color.White);
                    }
                }
                else
                {
                    sb.Draw(progressBarFiller, GlobalScalars.scaleRect(new Rectangle(288 + 105 * i, screenHeight - 33, 98, 10)), new Rectangle(0, 0, 65, 10), Color.White);
                }
            }
            sb.Draw(checkpointFlag, GlobalScalars.scaleRect(new Rectangle(378, screenHeight - 50, 32, 32)), Color.White);
            sb.Draw(checkpointFlag, GlobalScalars.scaleRect(new Rectangle(483, screenHeight - 50, 32, 32)), Color.White);
            sb.Draw(checkpointFlag, GlobalScalars.scaleRect(new Rectangle(588, screenHeight - 50, 32, 32)), Color.White);
            if (checkpointsCrossed == 3)
            {
                sb.Draw(progressBarFiller, GlobalScalars.scaleRect(new Rectangle(603, screenHeight - 33, 98, 10)), new Rectangle(0, 0, 65, 10), Color.Red);
                sb.Draw(playerTexture, GlobalScalars.scaleRect(new Rectangle(608, screenHeight - 43, 32, 32)), Color.White);
                sb.Draw(bossTexture, GlobalScalars.scaleRect(new Rectangle(640, screenHeight - 43, 64, 32)), null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }
        }
    }
}
