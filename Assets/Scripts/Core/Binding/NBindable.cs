using System;
using System.Reflection;
using UnityEngine;

/// <summary>
/// huangchuwen
/// ����ʹ����ӵ���ܹ����󶨵�����
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class NBindable
{
    protected event Action onValueChange = () =>
    {
        Debug.Log("OnValueChange");
    };
    /// <summary>
    /// ����һ��ֵ�ı��¼�
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
        // ��ȡ���м̳���NBindable�ĳ�Ա����, �����¼�����
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
        // ��ȡ���м̳���NBindable�ĳ�Ա����, �����¼�����
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