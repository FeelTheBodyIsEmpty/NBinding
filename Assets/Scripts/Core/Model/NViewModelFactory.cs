using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

/**
 *  huangchuwen
 *  2016/08/12
 *  用来生产ViewModel，生产出来的ViewModel实例内部的Field都的改变都会引起ViewModel的  
 *  OnValueChange被调用
 */
class NViewModelFactory<TViewModel> where TViewModel : NViewModel, new ()
{
    public static TViewModel Create()
    {
        TViewModel vmRet = new TViewModel();
        var typeVM = typeof(TViewModel);
        var fields = typeVM.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var field in fields)
        {
            if (field.FieldType.IsSubclassOf(typeof(NBindable)))
            {
                NBindable nbPpt = field.GetValue(vmRet) as NBindable; 
                nbPpt.OnValueChangeHandle += vmRet.OnValueChangeHandle;
            }
        }
        return vmRet;
    }
}