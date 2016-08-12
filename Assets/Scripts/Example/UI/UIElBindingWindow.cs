// /*
// * ==============================================================================
// *
// * Description: 
// *
// * Version: 1.0
// * Created: 2016-08-11 19:55
// *
// * Author: Surui (76963802@qq.com)
// * Company: NBinding
// *
// * ==============================================================================
// */

using UnityEngine;

public class UIElBindingWindow : NViewController<UIElBindingWindow>
{
    private MessageInfo message;

    void Update()
    {
        
    }
    public override void OnInit()
    {
        message = new MessageInfo { Text = "This the Binding Label" };
        var button = this.transform.FindChild("Button");
        // 点击模拟数据修改操作
        UIEventListener.Get(button.gameObject).onClick = go =>
        {
            message.Text += ".Append";
            // 手动挡...
            message.OnValueChanged();
        };
        // 由于是Demo, 所以自己把自己打开
        this.Open();
    }

    public override void OnOpen()
    {
        Debug.Log("界面被打开");
    }

    public override void OnClose()
    {
        Debug.Log("界面被关闭");
    }
}