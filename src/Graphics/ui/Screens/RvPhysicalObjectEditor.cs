using Microsoft.Xna.Framework;

public class RvPhysicalObjectEditor : RvSDialog<RvPhysicalObject>
{
    private Vector2 position;
    private Vector2 velocity;
    private bool immovable;

    private RvPhysicalObjectEditor(Rectangle bounds) : base(bounds)
    {
        initComponents();
    }

    public static RvPhysicalObjectEditor factory()
    {
        Rectangle bounds = new Rectangle(0,0, 500, 500); //todo
        return new RvPhysicalObjectEditor(bounds);
    }

    //todo
    private void initComponents()
    {
        //position

        //velocity

        //immovable
    }
}