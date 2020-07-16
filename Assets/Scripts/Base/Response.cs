

using EZFramework;
using System.Collections;
using UnityEngine;

namespace NetWorkAndData.APIS
{
    public class Response
    {
        IResponseMethod IRes = new ResponseMethod();

        public void OnRecvMethod(ProtocolTypes type, Hashtable result)
        {
            Debug.Log(result.toJson());
            if (result.ContainsKey("error"))
            { if (result["error"].ToString() != "success") { return; } }
            switch (type)
            {
                case ProtocolTypes.SignInResponse:
                    IRes.SignInResponseMethod(result);
                    break;
                case ProtocolTypes.AccountBindResponse:
                    break;
                case ProtocolTypes.VerifiedResponse:
                    break;
                case ProtocolTypes.SignInRewardListResponse:
                    break;
                case ProtocolTypes.StoreItemResponse:
                    break;
                case ProtocolTypes.ChangeNickNameResponse:
                    Actor.nickName = EpheMeralActor.nickName;
                    break;
                case ProtocolTypes.FriendAddResponse:
                    break;
                case ProtocolTypes.FriendListResponse:
                    break;
                case ProtocolTypes.SignInRewardResponse:
                    break;
                case ProtocolTypes.StoreBuyItemResponse:
                    break;
                case ProtocolTypes.RandomBoxChoiceResponse:
                    break;
                case ProtocolTypes.RankingListResponse:
                    break;
                case ProtocolTypes.DiamondToRandomBoxPointResponse:
                    break;
                case ProtocolTypes.AchievementProgressResponse:
                    break;
                case ProtocolTypes.AchievementListResponse:
                    break;
                case ProtocolTypes.AchievementRewardResponse:
                    break;
                case ProtocolTypes.RemoveAdResponse:
                    break;
                case ProtocolTypes.GameLogResponse:
                    break;
                case ProtocolTypes.OpenAdResponse:
                    break;
                case ProtocolTypes.CreateGameRoomResponse:
                    break;
                case ProtocolTypes.EnterGameRoomResponse:
                    break;
                case ProtocolTypes.EnterGameRoom:
                    IRes.EnterGameRoomMethod(result);
                    break;
                case ProtocolTypes.ExitGameRoomResponse:
                    IRes.ExitGameRoomResponseMethod(result);
                    break;
                case ProtocolTypes.ExitGameRoom:
                    IRes.ExitGameRoomMethod(result);
                    break;
                case ProtocolTypes.CheckVersionResponse:
                    break;
                case ProtocolTypes.EnterRandomGameRoomResponse:
                    IRes.EnterRandomGameRoomResponseMethod(result);
                    break;
                case ProtocolTypes.GameRoomState:
                    IRes.GameRoomStateMethod(result);
                    break;
                case ProtocolTypes.ChangeSceneCompleteResponse:
                    IRes.ChangeSceneCompleteResponseMethod(result);
                    break;
                case ProtocolTypes.PlayTurn:
                    IRes.PlayTurnMethod(result);
                    break;
                case ProtocolTypes.PlayTurnResponse:
                    IRes.PlayTurnResponseMethod(result);
                    break;
                case ProtocolTypes.DoAction:
                    IRes.DoActionMethod(result);
                    break;
                case ProtocolTypes.TakeOffResponse:
                    break;
                case ProtocolTypes.TakeOffResult:
                    IRes.TakeOffResultMethod(result);
                    break;
                case ProtocolTypes.MoveResponse:
                    break;
                case ProtocolTypes.MoveResult:
                    IRes.MoveResultMethod(result);
                    break;
                case ProtocolTypes.EndGame:
                    break;
            }
        }
    }

}

