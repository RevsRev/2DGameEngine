using System.Collections.Generic;

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

public class RvWrapperUtils
{
    public static List<U> unwrapVector<U, T>(List<T> wrappers) where U : RvAbstractWrappable where T : RvAbstractWrapper
    {
        List<U> retval = new List<U>();
        for (int i=0; i<wrappers.Count; i++)
        {
            retval.Add((U)wrappers[i].unWrap());
        }
        return retval;
    }
    public static List<T> wrapVector<U, T>(List<U> wrappables) where U : RvAbstractWrappable where T : RvAbstractWrapper
    {
        List<T> retval = new List<T>();
        for (int i=0; i<wrappables.Count; i++)
        {
            retval.Add((T)wrappables[i].wrap());
        }
        return retval;
    }
}