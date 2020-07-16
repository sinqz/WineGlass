using System;
/// <summary>
/// 请求注册登录
/// </summary>
namespace NetWorkAndData
{

    [Serializable]
    public class SignInRequest:ProtocolBase
    {
        public SignInRequest()
        {
            this.type = ProtocolTypes.SignInRequest;
        }
        /// <summary>
        ///登录类型
        /// </summary>
        public ConstSignInType signInType = ConstSignInType.guest;

        /// <summary>
        /// 登录账号
        /// </summary>
        public string accountId;

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickName;

        /// <summary>
        /// Share SDK unionId
        /// </summary>
        public string unionId;

        /// <summary>
        /// 省份
        /// </summary>
        public string province;

        /// <summary>
        /// 城市
        /// </summary>
        public string city;
    }


    /// <summary>
    /// 登录选择平台
    /// </summary>
    public enum ConstSignInType
    {
       
        /// <summary>
        /// 游客
        /// </summary>
        guest,
        /// <summary>
        /// 微信
        /// </summary>
        weixin,
        /// <summary>
        /// 苹果
        /// </summary>
        apple,
        /// <summary>
        /// facebook
        /// </summary>
        facebook,
        /// <summary>
        /// QQ
        /// </summary>
        qq

    }
}
