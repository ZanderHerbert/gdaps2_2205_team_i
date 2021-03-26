using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    class Checkpoint
    {
        private Texture2D checkpointImage;
        private Rectangle position;
        private string checkpointName;

        public Checkpoint(string checkpointName,Texture2D checkpointImage, Rectangle position)
        {
            this.checkpointName = checkpointName;
            this.checkpointImage = checkpointImage;
            this.position = position;
        }

        public void Update(Game1 game)
        {
            if (position.Contains(game.player.Position))
            {
                game.player.Health = 6;
                game.currentCheckpoint = checkpointName;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Game1 game)
        {
            if (position.Contains(game.player.Position))
            {
                spriteBatch.Draw(checkpointImage, position, Color.Green);
            }
            else
            {
                spriteBatch.Draw(checkpointImage, position, Color.White);
            }
        }
    }
}
