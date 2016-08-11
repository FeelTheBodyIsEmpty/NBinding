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

public abstract class NViewController : MonoBehaviour
{
    public NViewModel model { get; private set; }
    public NViewManager manager;
    private void Awake()
    {
        manager = NViewManager.Create();
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
        manager.Open(this.GetType());
        OnOpen();
    }

    public virtual void Close()
    {
        model.OnValueChanged += OnViewModelValueChanged;
        manager.Close(this.GetType());
        OnClose();
    }

    public virtual void BindModel(NViewModel viewModel)
    {
        model = viewModel;
        viewModel.OnValueChanged += OnViewModelValueChanged;
    }

    private void OnViewModelValueChanged()
    {
        this.RefreshUI();
    }

    private void RefreshUI()
    {
        // TODO 这里根据绑定的UI进行遍历更新
    }
}