public interface RvScreenI
{
    public abstract void Draw(RvAbstractDrawer drawer);
    public abstract void init();
    public abstract bool isOkToFinish();
    public abstract void doFinish();
}