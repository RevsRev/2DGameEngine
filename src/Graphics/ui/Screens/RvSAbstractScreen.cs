using Microsoft.Xna.Framework;
using System.Threading.Tasks;

public abstract class RvSAbstractScreen : RvAbstractPanel, RvScreenI
{
    //20 frames per second seems reasonable.
    public static readonly int SCREEN_REFRESH_PERIOD = 50;

    //FOR TESTING
    public static readonly int DEFAULT_X = 500;
    public static readonly int DEFAULT_Y = 300;

    bool okToFinish = false;

    protected RvSAbstractScreen(Rectangle bounds) : base(bounds)
    {
    }

    public override void init()
    {
        base.init();
        setBounds(new Rectangle(0, 0, DEFAULT_X, DEFAULT_Y));
        setOffset(new Vector2((RvSystem.SCR_WIDTH-DEFAULT_X)/2, (RvSystem.SCR_HEIGHT - DEFAULT_Y)/2));
    }

    protected void setOkToFinish(bool okToFinish)
    {
        this.okToFinish = okToFinish;
    }

    public bool isOkToFinish()
    {
        return okToFinish;
    }
    public virtual void doFinish()
    {
        dispose();
    }
}