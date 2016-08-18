using System;
using System.Reflection;
using UnityEngine;

/// <summary>
/// huangchuwen
/// 用来使数据拥有能够被绑定的作用
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class NBindable
{
    protected event Action onValueChange = () =>
    {
        Debug.Log("OnValueChange");
    };
    /// <summary>
    /// 引发一个值改变事件
    /// </summary>
    public virtual void RaiseValueChangeEvent()
    {
        if (this.onValueChange != null)
        {
            this.onValueChange();
        }
    }

    public void InitChangeEvent(Type type, Action changeHandle)
    {
        this.onValueChange += changeHandle;
        // 获取所有继承自NBindable的成员属性, 进行事件监听
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType.IsSubclassOf(typeof (NBindable)))
            {
                var child = field.GetValue(this) as NBindable;
                if (child == null) continue;
                //child.onValueChange += changeHandle;
                child.InitChangeEvent(field.FieldType, changeHandle);
            }
        }
    }

    public void RemoveChangeEvent(Type type, Action changeHandle)
    {
        // 获取所有继承自NBindable的成员属性, 进行事件监听
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo field in fields)
        {
            if (field.GetType().IsSubclassOf(typeof(NBindable)))
            {
                var child = field.GetValue(this) as NBindable;
                if (child == null) continue;
                child.onValueChange -= changeHandle;
                child.RemoveChangeEvent(child.GetType(), changeHandle);
            }
        }
    }
}