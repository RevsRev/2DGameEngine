using Microsoft.Xna.Framework;
using System.Threading.Tasks;

public abstract class RvAbstractScreen<T> : RvAbstractComponent, RvScreenTypeI<T>
{
    public RvAbstractScreen(Rectangle bounds) : base(bounds)
    {

    }

    public abstract T doPopup();
    public abstract override void unInit();
}