/*事件ID类，在此类中定义所有游戏中用到的事件ID*/
public class EventID
{
    /************游戏事件************/
    /// <summary>
    /// 扔骰子事件   
    /// </summary>
    public const int PlayTurn = 0;
    /// <summary>
    /// 选择移动的杯子事件
    /// </summary>
    public const int DoAction = PlayTurn + 1;
    /// <summary>
    /// 杯子出场事件
    /// </summary>
    public const int TakeCup = DoAction + 1;
    /// <summary>
    /// 杯子移动事件
    /// </summary>
    public const int MoveCup = TakeCup + 1;
}
