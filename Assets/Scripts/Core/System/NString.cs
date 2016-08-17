using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public sealed class NString : NBindable
{
    private string val;
    public NString(string val)
    {
        this.val = val;
        this.RaiseValueChangeEvent();
    }
#region 基础运算符

    public static implicit operator NString(string s)
    {
        // TODO 此处导致事件链丢失
        return new NString(s);
    }

    public static NString operator +(NString ns, string s)
    {
        ns.val = ns.val + s;
        ns.RaiseValueChangeEvent();
        return ns;
    }

    public static NString operator +(string s, NString ns)
    {
        ns.val = ns.val + s;
        ns.RaiseValueChangeEvent();
        return ns;
    }

#endregion

#region 逻辑运算符

    public static bool operator ==(NString a, NString b)
    {
        return a == b || (a != null && b != null && a.val == b.val);
    }
    public static bool operator ==(NString a, string b)
    {
        return (a == null && b == null) || (a != null && a.val == b);
    }

    public static bool operator ==(string b, NString a)
    {
        return (a == null && b == null) || (a != null && a.val == b);
    }

    public static bool operator !=(NString a, string b)
    {
        return !(a == b);
    }

    public static bool operator !=(string b, NString a)
    {
        return !(b == a);
    }

    public static bool operator !=(NString a, NString b)
    {
        return !(a == b);
    }

#endregion

#region 逻辑运算符扩展

    public static bool operator true(NString p)
    {
        return !string.IsNullOrEmpty(p.val);
    }

    public static bool operator false(NString p)
    {
        return string.IsNullOrEmpty(p.val);
    }

#endregion

    public int Length
    {
        get { return val.Length; }
    }

    public override string ToString()
    {
        return val;
    }

    public override bool Equals(object obj)
    {
        return val.Equals(obj);
    }

    public override int GetHashCode()
    {
        return val.GetHashCode();
    }
}
