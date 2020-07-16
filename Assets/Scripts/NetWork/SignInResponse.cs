using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWorkAndData.APIS
{
    public class SignInResponse
    {
        private static Dictionary<string, JsonData> SignDict = new Dictionary<string, JsonData>();
        private static SignInResponse instance = null;
        private SignInResponse(Hashtable result)
        {
            JsonData data = JsonMapper.ToObject(result.toJson());
            SignDict.Add(APIS.nickName, data[APIS.userInfo][APIS.nickName]);
            SignDict.Add(APIS.userId, data[APIS.userInfo][APIS.userId]);
            SignDict.Add(APIS.accounts, data[APIS.userInfo][APIS.accounts]);
            SignDict.Add(APIS.type, data[APIS.userInfo][APIS.accounts][0][APIS.type]);
            SignDict.Add(APIS.accountId, data[APIS.userInfo][APIS.accounts][0][APIS.accountId]);
            SignDict.Add(APIS.removeAd, data[APIS.userInfo][APIS.removeAd]);
            SignDict.Add(APIS.expendInvCount, data[APIS.userInfo][APIS.expendInvCount]);
            SignDict.Add(APIS.amounts, data[APIS.userInfo][APIS.amounts]);
            SignDict.Add(APIS.coin, data[APIS.userInfo][APIS.amounts][APIS.coin]);
            SignDict.Add(APIS.totalWinFirstCount, data[APIS.userInfo][APIS.totalWinFirstCount]);
            SignDict.Add(APIS.continuedSignInCount, data[APIS.userInfo][APIS.continuedSignInCount]);
            SignDict.Add(APIS.online, data[APIS.userInfo][APIS.online]);
            SignDict.Add(APIS.signInReward, data[APIS.userInfo][APIS.signInReward]);
            SignDict.Add(APIS.items, data[APIS.userInfo][APIS.items]);
            SignDict.Add(APIS.lastLoginTime, data[APIS.userInfo][APIS.lastLoginTime]);
            SignDict.Add(APIS.friends, data[APIS.userInfo][APIS.friends]);
            SignDict.Add(APIS.maxInvCount, data[APIS.userInfo][APIS.maxInvCount]);
            SignDict.Add(APIS.uid, data[APIS.userInfo][APIS.uid]);
        }
        public static SignInResponse mSignInResponse(Hashtable result)
        {
            if (instance == null)
                instance = new SignInResponse(result);
            return instance;
        }

        public static JsonData GetSign(string KeySign)
        {
            return SignDict[KeySign];
        }

        public static void GetSign()
        {
            foreach (var sigh in SignDict.Values)
            {
                Debug.Log(sigh.ToString());
            }
        
        }
    }
}

