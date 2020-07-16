/*事件监听者，凡是希望响应事件的脚本都实现这个接口*/
public interface IEventListener
{
    //响应事件的触发回调函数，一旦有事件发生就回调此函数
    void OnEventTrigger(Event _Event);
}
