using System;
using System.Collections.Generic;
using UnityEngine;

namespace NetWorkAndData
{
    namespace APIS
    {
        [Serializable]
        public class APIS
        {
            /// <summary>
            /// IP
            /// </summary>
            public const string SERVER_IP = "192.168.3.113";
            /// <summary>
            /// 端口号
            /// </summary>
            public const int SERVER_PORT = 19090;

            public const string userInfo = "userInfo";
            public const string nickName = "nickName";
            public const string userId = "userId";
            public const string accounts = "accounts";
            public const string type = "type";
            public const string accountId = "accountId";
            public const string removeAd = "removeAd";
            public const string expendInvCount = "expendInvCount";
            public const string amounts = "amounts";
            public const string coin = "coin";
            public const string totalWinFirstCount = "totalWinFirstCount";
            public const string continuedSignInCount = "continuedSignInCount";
            public const string online = "online";
            public const string signInReward = "signInReward";
            public const string items = "items";
            public const string lastLoginTime = "lastLoginTime";
            public const string friends = "friends";
            public const string maxInvCount = "maxInvCount";
            public const string uid = "uid";
            public const string itemId = "itemId";
            public const string id = "id";
            public const string cupItemId = "cupItemId";
            public const string diceItemId = "diceItemId";
            public const string avatarItemId = "avatarItemId";
            public const string itemCount = "itemCount";
            public const string coinCount = "coinCount";
            public const string gameUsers = "gameUsers";
            public const string gameState = "gameState";
            public const string dice = "dice";
            public const string pos = "pos";
        }

        [Serializable]
        public class Actor//(游戏信息)包含人物信息前端显示
        {
            private static Actor instance = null;
            public static string nickName { get; set; }
            public static long Coin { get; set; }
            public static long Diamond { get; set; }
            public static string userId { get; set; }
            public static string uid { get; set; }
            public static List<Sprite> HeadSprite = new List<Sprite>();

            private Actor()
            {
                nickName = SignInResponse.GetSign(APIS.nickName).ToString();//姓名
                Coin = Convert.ToInt32(SignInResponse.GetSign(APIS.coin).ToString()); //(int);//金币
                Diamond = 0;//钻石
                userId = SignInResponse.GetSign(APIS.userId).ToString();//唯一ID
                uid = SignInResponse.GetSign(APIS.uid).ToString();
                foreach (var mSprite in Resources.LoadAll<Sprite>("UI/HeadPortrait"))
                { HeadSprite.Add(mSprite); }
            }

            public static Actor mAcrot()
            {
                if (instance == null)
                    instance = new Actor();
                return instance;
            }
        }

        /// <summary>
        /// 临时数据存放
        /// </summary>
        [Serializable]
        public class EpheMeralActor
        {
            private EpheMeralActor() { }
            public static string nickName;//临时记录名字
            public static Sprite mSprite;//临时记录进入匹配房间时的初始占位图
            public static Dictionary<string, GameUser> DictUser { get; set; } = new Dictionary<string, GameUser>();//临时记录匹配完毕全部进入游戏时各个玩家的状态
            public static Player mPlayer;//临时记录当前要移动的杯子
            public static Queue<Action> Actions = new Queue<Action>();
        }
    }
}


