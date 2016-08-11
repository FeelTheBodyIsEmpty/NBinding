using System;

/// <summary>
/// huangchuwen
/// 用来使数据拥有能够被绑定的作用
/// </summary>
/// <typeparam name="T"></typeparam>
public class NBindableProperty <T>
{
    private T val;
    public T Value
    {
        get
        {
            return val;
        }
        set
        {
            ChangeValue(this.val, value);
            this.val = value;
        }
    }

    private void ChangeValue(T newVal, T oldVal)
    {
        if (OnValueChange != null)
        {
            OnValueChange(newVal, oldVal);
        }
    }

    public Action<T, T> OnValueChange = (T newVal, T oldVal) => { };
}