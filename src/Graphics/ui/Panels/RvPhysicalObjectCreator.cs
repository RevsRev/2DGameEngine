using Microsoft.Xna.Framework;

public class RvPhysicalObjectCreator : RvDialogPanel
{
    private const int DEFAULT_WIDTH = 400;
    private const int DEFAULT_HEIGHT = 200;

    //This is not ideal but will do for now.
    private readonly Rectangle TIP_TEXT_POSITIONING = new Rectangle(5, 5, 90, 20);
    private readonly Rectangle TEXT_FIELD_POSITIONING = new Rectangle(5, 30, 90, 65);

    private RvText tipText;
    private RvTextField textField;

    public RvPhysicalObjectCreator(Rectangle bounds) : base(bounds)
    {
        initComponents();
    }

    public RvPhysicalObjectCreator(Vector2 position) : this(new Rectangle((int)position.X, (int)position.Y, DEFAULT_WIDTH, DEFAULT_HEIGHT))
    {

    }

    private void initComponents()
    {
        tipText = new RvText("Enter object class", getTipTextBounds());
        textField = new RvTextField(getTextFieldBounds());

        addComponent(tipText);
        addComponent(textField);
    }

    private Rectangle getAdjustedBounds(Rectangle boundsToAdjust)
    {
        Rectangle contentBounds = getContentBounds();
        int width = (int)(contentBounds.Width * (boundsToAdjust.Width/(double)100));
        int height = (int)(contentBounds.Height * (boundsToAdjust.Height/(double)100));
        int x = (int)(contentBounds.Width * boundsToAdjust.X/(double)100);
        int y = (int)(contentBounds.Height * boundsToAdjust.Y/(double)100);
        return new Rectangle(x, y, width, height);
    }
    private Rectangle getTipTextBounds()
    {        
        return getAdjustedBounds(TIP_TEXT_POSITIONING);
    }
    private Rectangle getTextFieldBounds()
    {
        return getAdjustedBounds(TEXT_FIELD_POSITIONING);
    }
}