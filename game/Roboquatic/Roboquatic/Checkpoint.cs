using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roboquatic
{
    public class Checkpoint
    {
        // Checkpoint fields
        private Texture2D checkpointImage;
        private Rectangle position;
        private string checkpointName;
        private float time; // This is the time when a checkpoint should show up in the game
        private bool contact;


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

        // Get set the checkpoint's position
        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        // Get the checkpoint's image
        public Texture2D GetImage
        {
            get { return checkpointImage; }
        }

        public bool Contact
        {
            get { return contact; }
            set { contact = value; }
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
            if (position.Intersects(game.PlayerPosition))
            {
                // First save the player's stats
                health = game.PlayerHealth;
                speed = game.PlayerSpeed;
                damage = game.PlayerDamage;

                // Reset the player's health and set the current checkpoint to this checkpoint
                game.PlayerHealth = 6;
                game.CurrentCheckpoint = this;

                // Check if the player has reached the checkpoint or not
                contact = true;

                game.Time = time;
            }

            // Within the checkpoint time, no enemies should spawn
            // Until the player reached the checkpoint
            if (time <= game.Time && !contact)
            {
                game.SpawnEnemy = false;
            }
            else
            {
                game.SpawnEnemy = true;
            }
        }

        /// <summary>
        /// Draw the checkpoint by following certain rules
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="game"></param>
        public void Draw(SpriteBatch spriteBatch, Game1 game)
        {
            // Draw the checkpoint only if the player reached the right time and has cleared all the enemies 
            // Remove the checkpoint if the player contacted with the checkpoint
            if (time <= game.Time && !contact && game.Enemies.Count < 1)
            {
                spriteBatch.Draw(checkpointImage, position, Color.White);

                if (position.X >= game.ViewportWidth / 2)
                {
                    position.X -= 2;
                }
            }
        }

        // Print "game saved" message after contacted with the checkpoint
        public void PrintMessage(SpriteBatch spriteBatch, Game1 game)
        {
            // If contacted, then send a message that says "game saved"
            if (contact && game.Time <= time + 2 && game.Time >= time)
            {
                spriteBatch.DrawString(game.Font, "Game saved!", new Vector2(game.ViewportWidth / 2, game.ViewportHeight / 2), Color.White);
            }
        }
    }
}
