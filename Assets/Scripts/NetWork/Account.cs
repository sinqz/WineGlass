using System;
namespace NetWorkAndData
{
    /**/
    public class Account
    {
        public Account()
        {

        }
    }

    /// <summary>
    /// 连接状态
    /// </summary>
    public enum PacketType
    {
        /// <summary>
        ///连接
        /// </summary>
        connect,

        /// <summary>
        /// 断开连接
        /// </summary>
        disconnect,

        /// <summary>
        /// 收到连接
        /// </summary>
        recv
    }

    /// <summary>
    /// 请求接口
    /// </summary>
    public enum ProtocolTypes
    {
        /// <summary>
        /// 0：请求注册/登录
        /// </summary>
        SignInRequest,
        /// <summary>
        /// 1：注册/登录回答
        /// </summary>
        SignInResponse,
        /// <summary>
        ///2：请求绑定第三方账号
        /// </summary>
        AccountBindRequest,
        /// <summary>
        ///3：绑定第三方账号回答
        /// </summary>
        AccountBindResponse,
        /// <summary>
        /// 4：请求实名认证
        /// </summary>
        VerifiedRequest,
        /// <summary>
        /// 5：实名认证回答
        /// </summary>
        VerifiedResponse,
        /// <summary>
        /// 6：请求签到奖励目录
        /// </summary>
        SignInRewardListRequest,
        /// <summary>
        /// 7：签到奖励目录回答
        /// </summary>
        SignInRewardListResponse,
        /// <summary>
        /// 8：请求商城产品目录
        /// </summary>
        StoreItemRequest,
        /// <summary>
        /// 9：商城产品目录回答
        /// </summary>
        StoreItemResponse,
        /// <summary>
        /// 10：请求修改昵称/头像
        /// </summary>
        ChangeNickNameRequest,
        /// <summary>
        /// 11：修改昵称/头像回答
        /// </summary>
        ChangeNickNameResponse,
        /// <summary>
        /// 12：请求添加好友
        /// </summary>
        FriendAddRequest,
        /// <summary>
        /// 13：添加好友回答
        /// </summary>
        FriendAddResponse,
        /// <summary>
        /// 14：请求我的好友目录
        /// </summary>
        FriendListRequest,
        /// <summary>
        /// 15：我的好友目录回答
        /// </summary>
        FriendListResponse,
        /// <summary>
        /// 16：请求领取签到奖励
        /// </summary>
        SignInRewardRequest,
        /// <summary>
        /// 17：领取签到奖励回答
        /// </summary>
        SignInRewardResponse,
        /// <summary>
        /// 18：请求买东西
        /// </summary>
        StoreBuyItemRequest,
        /// <summary>
        /// 19：买东西回答
        /// </summary>
        StoreBuyItemResponse,
        /// <summary>
        /// 20：请求打开盲盒
        /// </summary>
        RandomBoxChoiceRequest,
        /// <summary>
        /// 21：打开盲盒回答
        /// </summary>
        RandomBoxChoiceResponse,
        /// <summary>
        /// 22：请求排行
        /// </summary>
        RankingListRequest,
        /// <summary>
        /// 23：排行回答
        /// </summary>
        RankingListResponse,
        /// <summary>
        /// 24：请求钻石换盲盒次数
        /// </summary>
        DiamondToRandomBoxPointRequest,
        /// <summary>
        /// 25：钻石换盲盒次数回答
        /// </summary>
        DiamondToRandomBoxPointResponse,
        /// <summary>
        /// 26：请求任务进度
        /// </summary>
        AchievementProgressRequest,
        /// <summary>
        /// 27：任务进度回答
        /// </summary>
        AchievementProgressResponse,
        /// <summary>
        /// 28：请求完成任务目录
        /// </summary>
        AchievementListRequest,
        /// <summary>
        /// 29：完成任务目录回答
        /// </summary>
        AchievementListResponse,
        /// <summary>
        /// 30：请求领取任务奖励
        /// </summary>
        AchievementRewardRequest,
        /// <summary>
        /// 31：领取任务奖励回答
        /// </summary>
        AchievementRewardResponse,
        /// <summary>
        /// 32：请求去掉广告
        /// </summary>
        RemoveAdRequest,
        /// <summary>
        /// 33：去掉广告回答
        /// </summary>
        RemoveAdResponse,
        /// <summary>
        /// 34：请求游戏结果记录目录
        /// </summary>
        GameLogRequest,
        /// <summary>
        /// 35：游戏结果记录目录回答
        /// </summary>
        GameLogResponse,
        /// <summary>
        /// 36：请求看广告
        /// </summary>
        OpenAdRequest,
        /// <summary>
        /// 37：看广告回答
        /// </summary>
        OpenAdResponse,
        /// <summary>
        /// 38：请求创建游戏房间
        /// </summary>
        CreateGameRoomRequest,
        /// <summary>
        /// 39：创建游戏房间回答
        /// </summary>
        CreateGameRoomResponse,
        /// <summary>
        /// 40：请求进路邀请得房间
        /// </summary>
        EnterGameRoomRequest,
        /// <summary>
        /// 41：进路邀请得房间回答
        /// </summary>
        EnterGameRoomResponse,
        /// <summary>
        /// 42：进路邀请得房间通报
        /// </summary>
        EnterGameRoom,
        /// <summary>
        /// 43：请求退出游戏房间
        /// </summary>
        ExitGameRoomRequest,
        /// <summary>
        /// 44：退出游戏房间回答
        /// </summary>
        ExitGameRoomResponse,
        /// <summary>
        /// 45：退出游戏房间通报
        /// </summary>
        ExitGameRoom,
        /// <summary>
        /// 46：请求客户端版本
        /// </summary>
        CheckVersionRequest,
        /// <summary>
        /// 47：客户端版本回答
        /// </summary>
        CheckVersionResponse,
        /// <summary>
        /// 48：请求进入线上游戏房间
        /// </summary>
        EnterRandomGameRoomRequest,
        /// <summary>
        /// 49：进入线上游戏房间回答
        /// </summary>
        EnterRandomGameRoomResponse,
        /// <summary>
        /// 50：游戏房间状态通报
        /// </summary>
        GameRoomState,
        /// <summary>
        /// 51：请求客户端场面切换完成
        /// </summary>
        ChangeSceneCompleteRequest,
        /// <summary>
        /// 52：客户端场面切换完成回答
        /// </summary>
        ChangeSceneCompleteResponse,
        /// <summary>
        /// 53：仍骰子（收到这个通报能是userId是跟自己一样的话能仍骰子）
        /// </summary>
        PlayTurn,
        /// <summary>
        /// 54：请求扔骰子
        /// </summary>
        PlayTurnRequest,
        /// <summary>
        /// 55：扔骰子回答
        /// </summary>
        PlayTurnResponse,
        /// <summary>
        ///56：扔骰子结果通报 
        /// </summary>
        PlayTurnResult,
        /// <summary>
        /// 57：扔骰子后让客户端选动作（出场/移动）
        /// </summary>
        DoAction,
        /// <summary>
        /// 58：请求被子出场
        /// </summary>
        TakeOffRequest,
        /// <summary>
        /// 59：被子出场回答
        /// </summary>
        TakeOffResponse,
        /// <summary>
        /// 60：被子出场结果通报
        /// </summary>
        TakeOffResult,
        /// <summary>
        /// 61：请求被子移动
        /// </summary>
        MoveRequest,
        /// <summary>
        /// 62：被子移动回答
        /// </summary>
        MoveResponse,
        /// <summary>
        /// 63：被子移动结果通报
        /// </summary>
        MoveResult,
        /// <summary>
        /// 64：游戏结果通报
        /// </summary>
        EndGame,
        /// <summary>
        /// 65:杯子回到原地（连着3次 6 或者 被别的玩家被抓）
        /// </summary>
        PlayerReset,
        /// <summary>
        /// 66:被子到达终点
        /// </summary>
        PlayerArrive,
        /// <summary>
        /// 67：请求使用道具
        /// </summary>
        UseItemRequest,
        /// <summary>
        /// 68：使用道具回答
        /// </summary>
        UseItemResponse,
        /// <summary>
        /// 69：通报使用道具
        /// </summary>
        UseItem,
        /// <summary>
        /// 70：效果触发
        /// </summary>
        EffectTrigger,
        /// <summary>
        /// 封闭砖瓦
        /// </summary>
        RoadBlocked,
        /// <summary>
        /// 72：通报使用换位卡
        /// </summary>
        TranspositionResult,
        /// <summary>
        /// 73：请求使用换位卡
        /// </summary>
        TranspositionRequest,
        /// <summary>
        /// 74：请求使用路障卡
        /// </summary>
        RoadBlockRequest,
        /// <summary>
        /// 75：使用抢夺卡结果
        /// </summary>
        SnatchItemResult,
        /// <summary>
        /// 76：请求使用骰子卡
        /// </summary>
        AppointmentDiceRequest,
        /// <summary>
        /// 77：请求使用抢夺卡
        /// </summary>
        SnatchItemRequest,
        /// <summary>
        /// 78：请求物品安装
        /// </summary>
        EquipItemRequest,
        /// <summary>
        /// 79：物品安装回答
        /// </summary>
        EquipItemResponse,
        /// <summary>
        /// 80：请求邀请参加游戏
        /// </summary>
        JoinGameRequest,
        /// <summary>
        /// 81：邀请参加游戏回答
        /// </summary>
        JoinGameResponse,
        /// <summary>
        /// 82：邀请参加游戏
        /// </summary>
        JoinGame,
        /// <summary>
        /// 83：请求分享
        /// </summary>
        SNSShareRequest,
        /// <summary>
        /// 84: 分享回答
        /// </summary>
        SNSShareResponse,
        /// <summary>
        /// 85: 通报这回仍骰子超时的人信息
        /// </summary>
        PlayTurnTimeout,
        /// <summary>
        /// 86：扔骰子后让客户端选动作超时
        /// </summary>
        DoActionTimeout,
        /// <summary>
        /// 87：自己游戏结束结果信息
        /// </summary>
        GameResult,
    }

    /// <summary>
    /// 商城接口
    /// </summary>
    public enum CategoryType
    {
        /// <summary>
        /// 金币
        /// </summary>
        coin,
        /// <summary>
        /// 杯子
        /// </summary>
        cup,
        /// <summary>
        /// 钻石
        /// </summary>
        diamond,
        /// <summary>
        /// 骰子
        /// </summary>
        dice,
        /// <summary>
        /// 道具
        /// </summary>
        item,
    }

    //身份证类
    public enum LicenseType
    {
        idCard,
        passport,
    }

    /// <summary>
    /// 游戏级别
    /// </summary>
    public enum GameLevelType
    {
        /// <summary>
        /// 初级
        /// </summary>
        primary,
        /// <summary>
        /// 普通
        /// </summary>
        ordinary,
        /// <summary>
        /// 中级
        /// </summary>
        intermediate,
        /// <summary>
        /// 高级
        /// </summary>
        senior,
        /// <summary>
        /// 好友
        /// </summary>
        friendly,
    }


    [Serializable]
    public class Packet
    {
        public PacketType type;
        public ProtocolTypes proto;
        public byte[] data;
    }

    [Serializable]
    public class Header
    {
        public UInt16 size { get; set; }
        public UInt16 type { get; set; }
    }

    [Serializable]
    public class ProtocolBase
    {
        public ProtocolTypes type;
    }
}