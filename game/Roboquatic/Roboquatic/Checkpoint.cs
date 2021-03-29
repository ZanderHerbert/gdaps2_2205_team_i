using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roboquatic
{
    class Checkpoint
    {
        // Checkpoint fields
        private Texture2D checkpointImage;
        private Rectangle position;
        private string checkpointName;
        private int time; // This is the time when a checkpoint should show up in the game

        // Game stats fields
        private int health;
        private int damage;
        private int speed;

        // Properties
        // Get the checkpoint's name
        public string GetName
        {
            get { return checkpointName; }
        }

        // Get the checkpoint's position
        public Rectangle GetPosition
        {
            get { return position; }
        }

        // Get the checkpoint's image
        public Texture2D GetImage
        {
            get { return checkpointImage; }
        }

        // Constructor
        public Checkpoint(string checkpointName, Texture2D checkpointImage, Rectangle position, int time)
        {
            this.checkpointName = checkpointName;
            this.checkpointImage = checkpointImage;
            this.position = position;
            this.time = time;
        }

        /// <summary>
        /// Collision dectection
        /// Save the player's progress
        /// </summary>
        /// <param name="game"></param>
        public void Update(Game1 game)
        {
            if (position.Contains(game.player.Position))
            {
                // First save the player's stats
                health = game.player.Health;
                speed = game.player.Speed;
                damage = game.player.ProjectileDamage;

                // Reset the player's health and set the current checkpoint to this checkpoint
                game.player.Health = 6;
                game.currentCheckpoint = checkpointName;

            }
        }

        public void Draw(SpriteBatch spriteBatch, Game1 game)
        {
            if (time <= game.time && time+5 >= game.time)
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
}
