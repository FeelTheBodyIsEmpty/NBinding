﻿using System;
using UnityEngine;
using System.Collections;

public abstract class NViewModel
{
    public event Action ValueChanged;

    public virtual void OnValueChanged()
    {
        var handler = ValueChanged;
        if (handler != null) handler();
    }
}