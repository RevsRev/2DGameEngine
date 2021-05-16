using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Windows.Input;
using System;

public class RvKeyboard
{
    private static RvKeyboard instance = null;
    private static readonly object padlock = new object();

    private static readonly float MIN_TIME_BETWEEN_SAME_KEY_PRESSES = 0.2f;
    private static readonly int NUM_KEYS = 254;

    List<RvKeyboardListenerI> listeners = new List<RvKeyboardListenerI>();
    private Dictionary<int, float> keyPressedTimes = new Dictionary<int, float>();

    private bool shift = false;

    //singleton.
    private RvKeyboard() 
    {
        initKeyPressedTimes();
    }

    public static RvKeyboard the()
    {
        if (instance == null)
        {
            lock(padlock)
            {
                if (instance == null)
                {
                    instance = new RvKeyboard();
                }
            }
        }
        return instance;
    }

    public void addKeyboardListener(RvKeyboardListenerI listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void Update(GameTime gameTime)
    {
        Keys[] keys = Keyboard.GetState().GetPressedKeys();
        for (int i=0; i<keys.Length; i++)
        {
            processKey(keys[i]);
        }
        shift = Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift);
        updateKeyPressedTimes((float)gameTime.ElapsedGameTime.TotalSeconds);
    }   
    private void processKey(Keys key)
    {
        if (keyPressedTimes[(int)key] <= MIN_TIME_BETWEEN_SAME_KEY_PRESSES)
        {
            return;
        }
        keyPressedTimes[(int)key] = 0.0f;

        for (int i=0; i<listeners.Count; i++)
        {
            listeners[i].keyPressed(key);
        }
    }

    public bool getShift()
    {
        return shift;
    }

    private void initKeyPressedTimes()
    {
        //there are 254 keys. We will init the dictionary in this method.
        for (int i=1; i<=NUM_KEYS; i++)
        {
            keyPressedTimes[i] = 0.0f;
        }
    }
    private void updateKeyPressedTimes(float time)
    {
        for (int i=1; i<=NUM_KEYS; i++)
        {
            keyPressedTimes[i] += time;
        }
    }
}