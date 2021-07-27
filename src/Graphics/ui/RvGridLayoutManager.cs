using Microsoft.Xna.Framework;

public class RvGridLayoutManager : RvAbstractLayoutManager
{
    int colsPerRow = 1;

    public RvGridLayoutManager(int colsPerRow)
    {
        this.colsPerRow = colsPerRow;
    }

    public override Rectangle getDrawingRegion(Rectangle componentBounds, int childComponentIndex, int totalChildComponents)
    {
        int rows = totalChildComponents/colsPerRow + 1;
        float rowHeight = componentBounds.Height/rows;
        float colWidth = componentBounds.Width/colsPerRow;

        int rowNumber = childComponentIndex/rows;
        int colNumber = childComponentIndex%colsPerRow;

        return new Rectangle((int)colWidth * colNumber, (int)rowHeight * rowNumber, (int)colWidth, (int)rowHeight);
    }
}