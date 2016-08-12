using System;
using UnityEngine;

/// <summary>
/// huangchuwen
/// 用来使数据拥有能够被绑定的作用
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class NBindable
{
    public Action OnValueChangeHandle = () =>
    {
        Debug.Log("xxxxxxx");
    };
    protected virtual void OnValueChange()
    {
        if (this.OnValueChangeHandle != null)
        {
            this.OnValueChangeHandle();
        }
    }
}