using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Utilities
{
    public static void UpdateTimer(bool condition, ref float timeKeeper, in float time, Action func)
    {
        if (condition)
        {
            timeKeeper -= Time.deltaTime;
            if (timeKeeper < 0)
            {
                func();
                timeKeeper = time;
            }
        }
    }
}
