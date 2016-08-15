using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class NString : NBindable
{
    private string val;
    public NString(string val)
    {
        this.val = val;
    }

    public static implicit operator NString(string s)
    {
        // TODO 这里导致事件链丢失
        return new NString(s);
    }

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
