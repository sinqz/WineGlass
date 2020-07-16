

using EZFramework;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWorkAndData.APIS
{
    public class ResponseMethod : IResponseMethod
    {
        public void ChangeSceneCompleteResponseMethod(Hashtable result)
        {
            SceneController.Instance.CurMode = GameMode.Sever;
            SceneController.Instance.LoadScene(SceneType.ST_GAME, false);//切换到游戏场景
        }

        public void DoActionMethod(Hashtable result)
        {
            Event DoActionEvent = new Event();
            //杯子移动事件ID
            DoActionEvent.EventID = EventID.DoAction;
            //事件产生源
            DoActionEvent.EventObj = result;
            EventController.Instance.PushEvent(DoActionEvent);
        }

        public void EnterGameRoomMethod(Hashtable result)
        {
            JsonData data = JsonMapper.ToObject(result.toJson());
            EpheMeralActor.DictUser.Clear();
            for (int i = 0; i < data[APIS.gameUsers].Count; i++)
            {
                GameUser user = new GameUser();
                user.id = data[APIS.gameUsers][i][APIS.id].ToString();
                user.cupItemId = data[APIS.gameUsers][i][APIS.cupItemId].ToString();
                user.diceItemId = data[APIS.gameUsers][i][APIS.diceItemId].ToString();
                user.avatarItemId = data[APIS.gameUsers][i][APIS.avatarItemId].ToString();
                user.itemCount = int.Parse(data[APIS.gameUsers][i][APIS.itemCount].ToString());
                user.nickName = data[APIS.gameUsers][i][APIS.nickName].ToString();
                user.userId = data[APIS.gameUsers][i][APIS.userId].ToString();
                EpheMeralActor.DictUser.Add(data[APIS.gameUsers][i][APIS.id].ToString(), user);
                EZComponent.GetConment<Game_Prepare>().RoomStatus.Rooms[i].mName.text = user.nickName;
                EZComponent.GetConment<Game_Prepare>().RoomStatus.Rooms[i].userId =user.userId;
            }
        }

        public void EnterRandomGameRoomResponseMethod(Hashtable result)
        {
            EZComponent.RemoveConment<Game_SelectItem>();
            EZComponent.GetConment<Game_Prepare>().ContModemethod(GameModeType.CreareaRoomGame);
        }

        public void ExitGameRoomMethod(Hashtable result)
        {
            if (EZComponent.GetConment<Game_Prepare>() == null)
                return;
            for (int i = 0; i < EZComponent.GetConment<Game_Prepare>().RoomStatus.Rooms.Length; i++)
            {
                if (EZComponent.GetConment<Game_Prepare>().RoomStatus.Rooms[i].userId == result[APIS.userId].ToString())
                    EZComponent.GetConment<Game_Prepare>().RoomStatus.Rooms[i].End();
            }
        }

        public void ExitGameRoomResponseMethod(Hashtable result)
        {
            Actor.Coin = long.Parse(result[APIS.coinCount].ToString());//扣除后剩余的金币
            EZComponent.RemoveConment<Game_UnifyUI>();
            EZComponent.RemoveConment<Game_Prepare>();
        }

        public void GameRoomStateMethod(Hashtable result)
        {
            GameState Types = (GameState)Enum.Parse(typeof(GameState), result[APIS.gameState].ToString());
            switch (Types)
            {
                case GameState.wait:
                    break;
                case GameState.start:
                    NetworkController.Instance.Send(new ChangeSceneCompleteRequest());
                    break;
                case GameState.playing:
                    Debug.Log("开始游戏!");
                    break;
                case GameState.end:
                    break;
                case GameState.turn:
                    break;
                case GameState.action:
                    break;
            }
        }

        

        public void MoveResultMethod(Hashtable result)
        {
            Event MoveCupEvent = new Event();
            //杯子移动全部通知
            MoveCupEvent.EventID = EventID.MoveCup;
            MoveCupEvent.EventObj = result;
            EventController.Instance.PushEvent(MoveCupEvent);
        }

        public void PlayTurnMethod(Hashtable result)
        {
            Event TurnEvent = new Event();
            //扔骰子事件ID
            TurnEvent.EventID = EventID.PlayTurn;
            //事件产生源
            TurnEvent.EventObj = result;
            EventController.Instance.PushEvent(TurnEvent);
        }

        public void PlayTurnResponseMethod(Hashtable result)
        {
            //收到摇完骰子的回调,关闭摇骰子的按钮
            SceneGameController.Instance.UIGameControl.SingleDice.gameObject.SetActive(false);
        }

        public void SignInResponseMethod(Hashtable result)
        {
            SignInResponse.mSignInResponse(result);//实例化Sign类获得字段初始值
            PlayerPrefs.SetString(APIS.accountId, SignInResponse.GetSign(APIS.accountId).ToString());
            SceneController.Instance.LoadScene(SceneType.ST_MAIN, true);
        }

        public void TakeOffResultMethod(Hashtable result)
        {
            Event TakeEvent = new Event();
            //杯子出场全部通知
            TakeEvent.EventID = EventID.TakeCup;
            TakeEvent.EventObj = result;
            EventController.Instance.PushEvent(TakeEvent);
        }
    }

    public interface IResponseMethod
    {
        /// <summary>
        /// 登录回到
        /// </summary>
        void SignInResponseMethod(Hashtable result);

        /// <summary>
        /// 进入线上游戏房间回答
        /// </summary>
        void EnterRandomGameRoomResponseMethod(Hashtable result);

        /// <summary>
        /// 退出线上游戏房间回答
        /// </summary>
        /// <param name="result"></param>
        void ExitGameRoomResponseMethod(Hashtable result);

        /// <summary>
        /// 房间信息全体播报
        /// </summary>
        /// <param name="result"></param>
        void EnterGameRoomMethod(Hashtable result);

        /// <summary>
        /// 退出房间全体播报
        /// </summary>
        /// <param name="result"></param>
        void ExitGameRoomMethod(Hashtable result);

        /// <summary>
        /// 游戏状态通报
        /// </summary>
        /// <param name="result"></param>
        void GameRoomStateMethod(Hashtable result);

        /// <summary>
        /// 客户端场景切换场面完成回答
        /// </summary>
        /// <param name="result"></param>
        void ChangeSceneCompleteResponseMethod(Hashtable result);

        /// <summary>
        /// 扔骰子全体通报(UserId和自己一样可以扔骰子)
        /// </summary>
        /// <param name="result"></param>
        void PlayTurnMethod(Hashtable result);

        /// <summary>
        /// 杯子移动接口
        /// </summary>
        /// <param name="result"></param>
        void DoActionMethod(Hashtable result);

        /// <summary>
        /// 扔骰子回答
        /// </summary>
        /// <param name="result"></param>
        void PlayTurnResponseMethod(Hashtable result);

        /// <summary>
        /// 杯子出场全部通报
        /// </summary>
        /// <param name="result"></param>
        void TakeOffResultMethod(Hashtable result);

        /// <summary>
        /// 杯子移动全体通报
        /// </summary>
        /// <param name="result"></param>
        void MoveResultMethod(Hashtable result);
    }

}
