/// <summary>
/// 游戏进度
/// </summary>
public enum GameState
{
    /// <summary>
    /// 等人
    /// </summary>
    wait = 0,
    /// <summary>
    /// 开始
    /// </summary>
    start=1,
    /// <summary>
    /// 进行中
    /// </summary>
    playing=2,
    /// <summary>
    /// 结束
    /// </summary>
    end=3,
    /// <summary>
    /// 扔骰子
    /// </summary>
    turn=4,
    /// <summary>
    /// 移动/出场
    /// </summary>
    action,
}

/// <summary>
/// 游戏模式
/// </summary>
public enum GameMode
{
    Null,
    Stand,
    Sever
}