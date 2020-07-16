using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LitJson;
using System;
using NetWorkAndData.APIS;

public class NetWorkPlayerLogic : PlayerLogic
{

    /// <summary>
    /// 杯子移动
    /// </summary>
    public void MoveNextCup(string[] pos)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            Debug.Log(pos[i]);
            for (int k = 0; k < ActorPathAction.Instance.ActorDic.Count; k++)
            {
                if (pos[i] == ActorPathAction.Instance.ActorDic[k].name)
                {
                    MoveAnim(ActorPathAction.Instance.ActorDic[k]);
                    //EpheMeralActor.Actions.Enqueue(() => { MoveAnim(ActorPathAction.Instance.ActorDic[k]); });
                }
            }
        }
        //StartCoroutine(LoadQueue());
    }


    IEnumerator LoadQueue()
    {
        while (EpheMeralActor.Actions.Count != 0 && EpheMeralActor.Actions != null)
        {
            EpheMeralActor.Actions.Dequeue()?.Invoke();
            yield return new WaitForSeconds(1f);
        }
    }




    /// <summary>
    /// 杯子出场
    /// </summary>
    public void TaskNextCup()
    {
        Debug.Log("杯子出场");
        MoveAnim(StarPos.NextCell);
    }

    public override void MoveAnim(PathCell NowStandCell)
    {
        NowStand = NowStandCell;
        if (NowStand != null)
        {
            Vector3 dirMove = NowStand.rect.anchoredPosition3D;
            transform.DOLocalJump(dirMove, MoveJump, 1, 1);
        }
    }
}
