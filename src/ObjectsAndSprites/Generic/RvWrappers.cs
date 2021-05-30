
//interface
public interface RvWrappableI
{
    RvAbstractWrapper wrap();
}
public interface RvWrapperI
{
    RvAbstractWrappable unWrap();
}

//class
public abstract class RvAbstractWrapper : RvWrapperI
{
    public abstract RvAbstractWrappable unWrap();
}
public abstract class RvAbstractWrappable : RvWrappableI
{
    public abstract RvAbstractWrapper wrap();
}