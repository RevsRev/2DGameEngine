using Microsoft.Xna.Framework;

public class RvSObjectEditor : RvSDialog
{
    //screen things
    private RvNumberField numberField;

    //the object we are editing
    private RvAbstractGameObject gameObject;

    public static void doPopup(RvAbstractGameObject obj)
    {
        RvSObjectEditor scrn = (RvSObjectEditor)RvScreenHandler.the().doPopup("RvSObjectEditor");
        scrn.setGameObject(obj);
    }

    public RvSObjectEditor() : this(new Vector2(DEFAULT_X, DEFAULT_Y))
    {
    }
    private RvSObjectEditor(Vector2 position) : this(new Rectangle((int)position.X, (int)position.Y, DEFAULT_WIDTH, DEFAULT_HEIGHT))
    {
    }
    private RvSObjectEditor(Rectangle bounds) : base(bounds)
    {
        setTitle("Object Editor");
        initComponents();
    }

    public override void init()
    {
        base.init();
        initComponents();
    }

    //todo
    private void initComponents()
    {
        numberField = new RvNumberField(new Rectangle(0,0,100,30)); //todo - layout manager etc.
        numberField.setOffset(new Vector2(30, 40));

        addComponent(numberField);
    }

    private void setGameObject(RvAbstractGameObject gameObject)
    {
        this.gameObject = gameObject;
    }
}