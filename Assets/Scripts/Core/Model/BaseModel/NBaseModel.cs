using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// public class NBaseModel<T> : NBindable
// {
//     public NBaseModel(T val)
//     {
//         Value = val;
//     }
// 
//     private T val;
//     public T Value
//     {
//         get
//         {
//             return val;
//         }
//         set
//         {
//             if (val != null && !val.Equals(value))
//             {
//                 OnValueChange();
//             }
//             val = value;
//         }
//     }
// }

public class NString : NBindable
{
    public NString(string val)
    {
        Value = val;
    }

    private string val;
    public string Value
    {
        get
        {
            return val;
        }
        set
        {
            if (val != null && !val.Equals(value))
            {
                OnValueChange();
            }
            val = value;
        }
    }
}
