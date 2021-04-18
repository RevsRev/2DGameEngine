using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class RvDebug
{
   public static bool MODE_DEBUG = false;
   public static Texture2D PIXEL = null;

   public static bool isDebugMode()
   {
       return MODE_DEBUG;
   }
   public static void setDebutMode(bool debug)
   {
       MODE_DEBUG = debug;
   }
   public static void toggleDebugMode()
   {
       MODE_DEBUG = !MODE_DEBUG;
   }

   public static Texture2D getPixel()
   {
       return PIXEL;
   }
}