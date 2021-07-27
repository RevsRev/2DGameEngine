using Microsoft.Xna.Framework;

public abstract class RvAbstractLayoutManager
{
    //For now, vertical/horizontal/grid layouts will be sufficient.
    public abstract Rectangle getDrawingRegion(Rectangle componentBounds, int childComponentIndex, int totalChildComponents);
}