using Microsoft.Xna.Framework;

public class RvPopupMenuItem : RvButtonText
{
    private const int DEFAULT_TEXT_SIZE = 25;
    private const int DEFAULT_WIDTH = 200;
    private const int DEFAULT_HEIGHT = 30;

    public RvPopupMenuItem(string text) : this(text, DEFAULT_TEXT_SIZE)
    {
    }
    private RvPopupMenuItem(string text, int textSize) : base(text, textSize, new Rectangle(0,0, DEFAULT_WIDTH, DEFAULT_HEIGHT))
    {
    }
}