using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using System.Collections.Generic;

public class RvPhysicalObjectCreator : RvSDialog
{
    //This is not ideal but will do for now.
    private readonly Rectangle TIP_TEXT_POSITIONING = new Rectangle(5, 5, 90, 20);
    private readonly Rectangle TEXT_FIELD_POSITIONING = new Rectangle(5, 30, 90, 65);

    //private RvText tipText;
    //private RvTextField textField;

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

    private void initComponents()
    {
        setTitle("Object Creator");

        //tipText = createRvText();
        //textField = createRvTextField();

        //addComponent(tipText);
        //addComponent(textField);

        createButtons();
    }

    private void createButtons()
    {
        //todo - layout manager/move these to statics etc
        int buttonWidth = (int)(0.95f*(float)bounds.Width/2);
        int buttonHeight = 30;

        List<string> objectNames = getObjectNames();

        //todo - this works for now, but probably want to make scrollable in the future.
        for (int i=0; i<objectNames.Count; i++)
        {
            int rowNumber = i/2;
            int columnNumber = i%2;

            string objectName = objectNames[i];
            Rectangle buttonBounds = new Rectangle(0,0, buttonWidth, buttonHeight);
            RvButtonText objectButton = new RvButtonText(objectName, buttonBounds);
            objectButton.setOffset(new Vector2(buttonWidth*columnNumber, buttonHeight*rowNumber));
            objectButton.Subscribe(this);
            addComponent(objectButton);
        }
    }

    private List<string> getObjectNames()
    {
        //todo
        return new List<string>{"RvKnight"};
    }

    public override void buttonPressed(string actionString)
    {
        base.buttonPressed(actionString);

        List<string> objectNames = getObjectNames();
        if (objectNames.Contains(actionString))
        {
            RvAbstractGameObject gameObject = (RvAbstractGameObject)RvClassLoader.createByName(actionString);
            RvGame.the().getCurrentLevel().addToObjectHandler(gameObject);
        }
    }

    // private Rectangle getAdjustedBounds(Rectangle boundsToAdjust)
    // {
    //     Rectangle contentBounds = getContentBounds();
    //     int width = (int)(contentBounds.Width * (boundsToAdjust.Width/(double)100));
    //     int height = (int)(contentBounds.Height * (boundsToAdjust.Height/(double)100));
    //     return new Rectangle(0, 0, width, height);
    // }
    // private Vector2 getOffset(Rectangle boundsToAdjust)
    // {
    //     Rectangle contentBounds = getContentBounds();
    //     int x = (int)(contentBounds.Width * boundsToAdjust.X/(double)100);
    //     int y = (int)(contentBounds.Height * boundsToAdjust.Y/(double)100);

    //     return new Vector2(x, y);
    // }
    // private RvText createRvText()
    // {        
    //     Rectangle adjustedBounds = getAdjustedBounds(TIP_TEXT_POSITIONING);
    //     Vector2 offset = getOffset(TIP_TEXT_POSITIONING);

    //     tipText = new RvText("Enter object class", adjustedBounds);
    //     tipText.setOffset(offset);
    //     return tipText;
    // }
    // private RvTextField createRvTextField()
    // {
    //     Rectangle adjustedBounds = getAdjustedBounds(TEXT_FIELD_POSITIONING);
    //     Vector2 offset = getOffset(TEXT_FIELD_POSITIONING);

    //     RvTextField textField = new RvTextField(adjustedBounds);
    //     textField.setOffset(offset);

    //     return textField;
    // }
}