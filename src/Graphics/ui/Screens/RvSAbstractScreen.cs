using Microsoft.Xna.Framework;
using System.Threading.Tasks;

public abstract class RvSAbstractScreen<T> : RvAbstractPanel, RvScreenTypeI<T>
{

    //20 frames per second seems reasonable.
    public static readonly int SCREEN_REFRESH_PERIOD = 50;

    //FOR TESTING
    public static readonly int DEFAULT_X = 200;
    public static readonly int DEFAULT_Y = 200;

    bool okToFinish = false;
    T data = default(T);

    public RvSAbstractScreen(Rectangle bounds) : base(bounds)
    {
    }

    public virtual void init()
    {
        setBounds(new Rectangle(0,0, DEFAULT_X, DEFAULT_Y));
    }

    public abstract override void unInit();

    protected void setData(T data)
    {
        this.data = data;
    }
    protected void setOkToFinish(bool okToFinish)
    {
        this.okToFinish = okToFinish;
    }

    public T doPopup()
    {
        while (!okToFinish)
        {
            RvUiDrawer.the().Begin();
            Draw();
            Update();
            RvUiDrawer.the().End();
            RvThread.sleep(SCREEN_REFRESH_PERIOD);
        }
        return data;
    }
}