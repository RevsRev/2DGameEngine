using Microsoft.Xna.Framework;

public class RvPhysicalObjectEditor : RvSDialog
{

    public RvPhysicalObjectEditor() : this(new Vector2(DEFAULT_X, DEFAULT_Y))
    {
    }
    public RvPhysicalObjectEditor(Vector2 position) : this(new Rectangle((int)position.X, (int)position.Y, DEFAULT_WIDTH, DEFAULT_HEIGHT))
    {
    }
    public RvPhysicalObjectEditor(Rectangle bounds) : base(bounds)
    {
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
    }


    public static RvPhysicalObjectEditor factory()
    {
        Rectangle bounds = new Rectangle(0,0, 500, 500); //todo
        return new RvPhysicalObjectEditor(bounds);
    }
}