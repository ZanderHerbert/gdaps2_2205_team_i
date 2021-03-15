using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Roboquatic
{
    // Delegate
    public delegate void OnButtonClickDelegate();

    public class Button
    {
        // Fields
        private SpriteFont font;
        private string text;
        private Rectangle buttonLocation;
        private Vector2 textLocation;
        private Texture2D buttonImage;
        private Color textColor;
        private MouseState preMState;
        private Color buttonColor = Color.DeepSkyBlue;

        // Events
        public event OnButtonClickDelegate OnLeftButtonClick;

        // Properties 
        public Color ChangeColor
        {
            get { return buttonColor; }
            set { buttonColor = value; }
        }

        // Constructor 
        public Button(GraphicsDevice device, Rectangle position, string text, SpriteFont font)
        {
            // Assign parameters to fields 
            this.font = font;
            this.buttonLocation = position;
            this.text = text;

            // Button's location
            Vector2 textSize = font.MeasureString(text);
            textLocation = new Vector2(
                (buttonLocation.X + buttonLocation.Width / 2) - textSize.X / 2,
                (buttonLocation.Y + buttonLocation.Height / 2) - textSize.Y / 2);

            // Text's default color
            textColor = Color.Black;

            // Button's texture
            buttonImage = new Texture2D(device, buttonLocation.Width, buttonLocation.Height, false, SurfaceFormat.Color);
            int[] colorData = new int[buttonImage.Width * buttonImage.Height];
            Array.Fill<int>(colorData, (int)buttonColor.PackedValue);
            buttonImage.SetData<Int32>(colorData, 0, colorData.Length);
        }

        //Update the game based on the clicked button's feature
        public void Update()
        {
            MouseState mState = Mouse.GetState();
            if (mState.LeftButton == ButtonState.Released && preMState.LeftButton == ButtonState.Pressed && buttonLocation.Contains(mState.Position))
            {
                if (OnLeftButtonClick != null)
                {
                    // Call ALL methods attached to this button
                    OnLeftButtonClick();
                }
            }

            preMState = mState;
        }

        // Draw 
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the button
            spriteBatch.Draw(buttonImage, buttonLocation, Color.White);

            // Draw the button's text
            spriteBatch.DrawString(font, text, textLocation, textColor);


        }
    }
}
