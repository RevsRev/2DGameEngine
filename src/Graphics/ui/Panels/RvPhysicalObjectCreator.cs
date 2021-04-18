using Microsoft.Xna.Framework;

public class RvPhysicalObjectCreator : RvPanel
{
    private RvText tipText;
    private RvTextField textField;

    public RvPhysicalObjectCreator(Rectangle bounds) : base(bounds)
    {
        initComponents();
    }

    private void initComponents()
    {
        tipText = new RvText("Enter object class", getTipTextBounds());
        textField = new RvTextField(getTextFieldBounds());

        addComponent(tipText);
        addComponent(textField);
    }

    private Rectangle getTipTextBounds()
    {
        int height = (int)(0.9 * bounds.Height/2);
        int width = (int)(0.9 * bounds.Width);
        int x = (int)(0.05*bounds.Width + bounds.X);
        int y = (int)(0.05*bounds.Height/2 + bounds.Y);
        return new Rectangle(x, y, width, height);
    }
    private Rectangle getTextFieldBounds()
    {
        int height = (int)(0.9 * bounds.Height/2);
        int width = (int)(0.9 * bounds.Width);
        int x = (int)(0.05*bounds.Width + bounds.X);
        int y = (int)(bounds.Height/2 + 0.05*bounds.Height/2 + bounds.Y);
        return new Rectangle(x, y, width, height);
    }
}