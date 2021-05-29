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

    public override void Draw(RvAbstractDrawer drawer)
    {
        //at the moment, this is just to text.
        base.Draw(drawer);
        drawer.DrawString(text, new Vector2(getDrawingRegion().X, getDrawingRegion().Y), 20, RvUiConstantsI.DRAWING_LAYER_TEXT);
    }

    public override void dispose()
    {
    }
}