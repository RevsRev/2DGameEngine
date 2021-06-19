using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class RvNumberField : RvAbstractTextField
{
    public RvNumberField(Rectangle bounds) : base(bounds)
    {
    }

    protected override void initKeysToChars()
    {
        addKeyToChar(Keys.D1, '1', '1');
        addKeyToChar(Keys.D2, '2', '2');
        addKeyToChar(Keys.D3, '3', '3');
        addKeyToChar(Keys.D4, '4', '4');
        addKeyToChar(Keys.D5, '5', '5');
        addKeyToChar(Keys.D6, '6', '6');
        addKeyToChar(Keys.D7, '7', '7');
        addKeyToChar(Keys.D8, '8', '8');
        addKeyToChar(Keys.D9, '9', '9');
        addKeyToChar(Keys.D0, '0', '0');
        //addKeyToChar(Keys.OemMinus, '-' '-'); //todo (when we want negative numbers) - similarly for decimal.
    }

    public int getNumber()
    {
        string numberAsString = sb.ToString();
        return int.Parse(numberAsString);
    }
}