using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System.Collections.Generic;
using System;

public class RvText : RvAbstractComponent
{
    private string text = "";

    public RvText(string text, Rectangle bounds) : base(bounds)
    {
        this.text = text;
    }

    public override void Draw(RvSpriteBatch spriteBatch)
    {
        //at the moment, this is just to text.
        spriteBatch.DrawRectangle(bounds, Color.White, RvSpriteBatch.DEFAULT_UI_LAYER);
        spriteBatch.DrawRectangleBorder(bounds, Color.Gray, RvSpriteBatch.DEFAULT_UI_BORDER_LAYER);
        spriteBatch.DrawString(text, new Vector2(bounds.X, bounds.Y), 20);
    }
}