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

    public override void Draw()
    {
        //at the moment, this is just to text.
        RvUiDrawer.the().DrawRectangle(bounds, Color.White);
        RvUiDrawer.the().DrawRectangleBorder(bounds, Color.Gray);
        RvUiDrawer.the().DrawString(text, new Vector2(bounds.X, bounds.Y), 20);
    }

    public override void unInit()
    {
        //to do
    }
}