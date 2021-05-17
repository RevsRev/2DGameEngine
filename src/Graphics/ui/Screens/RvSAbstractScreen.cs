using Microsoft.Xna.Framework;
using System.Threading.Tasks;

public abstract class RvSAbstractScreen<T> : RvAbstractPanel, RvScreenTypeI<T>
{
    bool okToFinish = false;
    T data = default(T);

    public RvSAbstractScreen(Rectangle bounds) : base(bounds)
    {

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
            //draw
            //update
        }
        return data;
    }
}