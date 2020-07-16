using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendController : MonoBehaviour
{
    public GameObject Init;//添加好友
    public GameObject InitFriend;//搜索好友成功
    public GameObject InitNoFriend;//搜索好友失败
    public InputField Field;//好友ID输入框

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 复制自己的ID号到粘贴板
    /// </summary>
    public void CopyIDMethod()
    {
        Debug.Log("复制成功");
    }
}
