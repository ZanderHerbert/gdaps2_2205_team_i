using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class Hud
    {
        private Texture2D healthSheet;
        private Texture2D progressBar;
        private Texture2D progressBarFiller;
        private int rightHealthOffset;
        private bool onceThrough;

        public Hud(Game1 game)
        {
            healthSheet = game.Content.Load<Texture2D>("HealthSheet");
            progressBar = game.Content.Load<Texture2D>("ProgressBar");
            progressBarFiller = game.Content.Load<Texture2D>("ProgressFiller");
            rightHealthOffset = 0;
            onceThrough = false;
        }

        public void Draw(SpriteBatch sb, Player player)
        {
            sb.Draw(healthSheet, new Vector2(30, 30), new Rectangle(0, 0, 155, 90), Color.White);
            for(int i = 0; i <= player.MaxHP - 7; i++)
            {
                sb.Draw(healthSheet, new Vector2(185 + i * 35, 30), new Rectangle(155, 0, 35, 90), Color.White);
            }
            rightHealthOffset = 35 * (player.MaxHP - 6);
            sb.Draw(healthSheet, new Vector2(185 + rightHealthOffset, 30), new Rectangle(190, 0, 70, 90), Color.White);

            if(player.Health == 1)
            {
                sb.Draw(healthSheet, new Vector2(50, 65), new Rectangle(20, 95, 25, 30), Color.White);
            }
            else if(player.Health == 2)
            {
                sb.Draw(healthSheet, new Vector2(50, 65), new Rectangle(20, 125, 60, 30), Color.White);
            }
            else if (player.Health == 3)
            {
                sb.Draw(healthSheet, new Vector2(50, 65), new Rectangle(20, 160, 95, 30), Color.White);
            }
            else if (player.Health == 4)
            {
                sb.Draw(healthSheet, new Vector2(50, 65), new Rectangle(20, 195, 130, 30), Color.White);
            }
            else if (player.Health == 5)
            {
                sb.Draw(healthSheet, new Vector2(50, 65), new Rectangle(20, 230, 165, 30), Color.White);
            }
            else if (player.Health == 6)
            {
                sb.Draw(healthSheet, new Vector2(50, 65), new Rectangle(20, 265, 200, 30), Color.White);
            }
            else
            {
                sb.Draw(healthSheet, new Vector2(50, 65), new Rectangle(20, 300, 200, 30), Color.White);
                rightHealthOffset = 0;
                for(int i = 0; i < player.Health - 7; i++)
                {
                    sb.Draw(healthSheet, new Vector2(255 + i * 35, 65), new Rectangle(50, 300, 30, 30), Color.White);
                    rightHealthOffset = 35 * (i + 1);
                }
                if(player.Health == player.MaxHP)
                {
                    sb.Draw(healthSheet, new Vector2(255 + rightHealthOffset, 65), new Rectangle(225, 300, 30, 30), Color.White);
                }
                else
                {
                    sb.Draw(healthSheet, new Vector2(255 + rightHealthOffset, 65), new Rectangle(50, 300, 30, 30), Color.White);
                }
            }
        }
    }
}
