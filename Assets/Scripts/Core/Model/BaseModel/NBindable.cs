using System;
using UnityEngine;

/// <summary>
/// huangchuwen
/// ����ʹ����ӵ���ܹ����󶨵�����
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