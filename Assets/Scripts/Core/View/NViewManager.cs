// /*
// * ==============================================================================
// *
// * Description: 
// *
// * Version: 1.0
// * Created: 2016-08-11 20:03
// *
// * Author: Surui (76963802@qq.com)
// * Company: NBinding
// *
// * ==============================================================================
// */

using System;
using UnityEngine;
using System.Collections;

public class NViewManager
{
    private static NViewManager manager;

    public static NViewManager Create()
    {
        manager = manager ?? new NViewManager();
        return manager;
    }

    public void Open(Type type)
    {
    }

    public void Close(Type type)
    {
    }
}