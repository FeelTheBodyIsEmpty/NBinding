using System;
using UnityEngine;
using System.Collections;

public abstract class NViewModel : NBindable
{
    protected virtual void OnValueChange()
    {
        base.RaiseValueChangeEvent();
    }
}