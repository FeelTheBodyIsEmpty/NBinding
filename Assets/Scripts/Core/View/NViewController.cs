// /*
// * ==============================================================================
// *
// * Description: 界面控制器基类
// *
// * Version: 1.0
// * Created: 2016-08-11 18:10
// *
// * Author: Surui (76963802@qq.com)
// * Company: NBinding
// *
// * ==============================================================================
// */

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NViewController<TView> : MonoBehaviour
{
    public NViewModel model { get; private set; }
    public NViewManager manager = NViewManager.Create();
    private Dictionary<string, NViewModel> models;
    private Dictionary<UILabel, string> elLabels;

    private void Awake()
    {
        OnInit();
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public abstract void OnInit();
    public abstract void OnOpen();

    public virtual void OnClose()
    {
    }

    public virtual void Open()
    {
        var type = typeof(TView);
        manager.Open(type);
        models = NViewModelBinder.GetAllModels(this, type);
        elLabels = NViewModelBinder.FindElBindingLabels(this);
        // AutoBind
        foreach (KeyValuePair<string, NViewModel> pair in models)
        {
            BindModel(pair.Value);
        }
        RefreshBandingUI();
        OnOpen();
    }

    public virtual void Close()
    {
        model.OnValueChangeHandle -= OnViewModelChanged;
        var type = typeof(TView);
        manager.Close(type);
        OnClose();
    }

    public virtual void BindModel(NViewModel viewModel)
    {
        model = viewModel;
        model.OnValueChangeHandle += OnViewModelChanged;
    }

    private void OnViewModelChanged()
    {
        this.RefreshBandingUI();
    }

    public void RefreshBandingUI()
    {
        // TODO 这里根据绑定的UI进行遍历更新
        NViewModelBinder.UpdateByEl(this, elLabels, models);
    }
}