using Microsoft.Xna.Framework;
using System.Threading.Tasks;

public class RvPhysicalObjectCreator : RvSDialog<RvPhysicalObject>
{
    private const int DEFAULT_X = 200;
    private const int DEFAULT_Y = 200;
    private const int DEFAULT_WIDTH = 600;
    private const int DEFAULT_HEIGHT = 200;

    //This is not ideal but will do for now.
    private readonly Rectangle TIP_TEXT_POSITIONING = new Rectangle(5, 5, 90, 20);
    private readonly Rectangle TEXT_FIELD_POSITIONING = new Rectangle(5, 30, 90, 65);

    private RvText tipText;
    private RvTextField textField;

    public RvPhysicalObjectCreator() : this(new Vector2(DEFAULT_X, DEFAULT_Y))
    {

    }

    public RvPhysicalObjectCreator(Rectangle bounds) : base(bounds)
    {
        initComponents();
    }

    public RvPhysicalObjectCreator(Vector2 position) : this(new Rectangle((int)position.X, (int)position.Y, DEFAULT_WIDTH, DEFAULT_HEIGHT))
    {

    }

    public override void init()
    {
        base.init();
        initComponents();
    }

    private void initComponents()
    {
        tipText = createRvText();
        textField = createRvTextField();

        addComponent(tipText);
        addComponent(textField);
    }

    private Rectangle getAdjustedBounds(Rectangle boundsToAdjust)
    {
        Rectangle contentBounds = getContentBounds();
        int width = (int)(contentBounds.Width * (boundsToAdjust.Width/(double)100));
        int height = (int)(contentBounds.Height * (boundsToAdjust.Height/(double)100));
        return new Rectangle(0, 0, width, height);
    }
    private Vector2 getOffset(Rectangle boundsToAdjust)
    {
        Rectangle contentBounds = getContentBounds();
        int x = (int)(contentBounds.Width * boundsToAdjust.X/(double)100);
        int y = (int)(contentBounds.Height * boundsToAdjust.Y/(double)100);

        return new Vector2(x, y);
    }
    private RvText createRvText()
    {        
        Rectangle adjustedBounds = getAdjustedBounds(TIP_TEXT_POSITIONING);
        Vector2 offset = getOffset(TIP_TEXT_POSITIONING);

        tipText = new RvText("Enter object class", adjustedBounds);
        tipText.setOffset(offset);
        return tipText;
    }
    private RvTextField createRvTextField()
    {
        Rectangle adjustedBounds = getAdjustedBounds(TEXT_FIELD_POSITIONING);
        Vector2 offset = getOffset(TEXT_FIELD_POSITIONING);

        RvTextField textField = new RvTextField(adjustedBounds);
        textField.setOffset(offset);

        return textField;
    }
}