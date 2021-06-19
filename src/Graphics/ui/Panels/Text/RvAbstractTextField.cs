using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System.Collections.Generic;
using System;

public abstract class RvAbstractTextField : RvAbstractPanel, RvFocusableI
{
    protected StringBuilder sb = new StringBuilder();
    private Dictionary<Keys, Tuple<char, char>> keysToChars = new Dictionary<Keys, Tuple<char, char>>();

    public RvAbstractTextField(Rectangle bounds) : base(bounds)
    {
    }

    public override void init()
    {
        base.init();
        setPanelColor(Color.White);
        initKeysToChars();
        RvFocusHandler.the().addFocusable(this);
    }

    public override void dispose()
    {
        RvFocusHandler.the().removeFocus(this);
        base.dispose();
    }

    protected abstract void initKeysToChars();

    public override void Draw(RvAbstractDrawer drawer)
    {
        //at the moment, this is just to text.
        base.Draw(drawer);
        drawer.DrawString(sb.ToString(), new Vector2(getDrawingRegion().X, getDrawingRegion().Y), 20, RvUiConstantsI.DRAWING_LAYER_TEXT);
    }

    public Rectangle getFocusRegion()
    {
        return getDrawingRegion();
    }
    public void focusKeyEvent(Keys keys)
    {
        bool shiftModifier = RvKeyboard.the().getShift();
        processKey(keys, shiftModifier);
    }

    private void processKey(Keys key, bool shiftModifier)
    {
        if (key == Keys.Back && sb.Length > 0)
        {
            sb.Remove(sb.Length-1 ,1);
        }

        if (shiftModifier && keysToChars.ContainsKey(key))
        {
            sb.Append(keysToChars[key].Item2);
        }
        else if (keysToChars.ContainsKey(key))
        {
            sb.Append(keysToChars[key].Item1);
        }
    }

    protected void addKeyToChar(Keys key, char charNoShift, char charShift)
    {
        keysToChars[key] = new Tuple<char, char>(charNoShift, charShift);
    }
}