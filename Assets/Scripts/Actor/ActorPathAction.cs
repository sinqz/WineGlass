using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPathAction : MonoSingletonTemplateScript<ActorPathAction>
{
    [HideInInspector] public List<PathCell> ActorDic = new List<PathCell>();
    public void OnLoad()
    {
        Init();
    }
    private void Init()
    {
        foreach (Transform actorPath in transform)
        {
            ActorDic.Add(actorPath.GetComponent<PathCell>());
            actorPath.GetComponent<PathCell>().Init();
        }
        if (ActorDic == null)
            return;
        for (int k = 0; k < 52; k++)
        {
            ActorDic[k].NextCell = ActorDic[k + 1 > 51 ? 0 : k + 1];
            ActorDic[k].LastCell = ActorDic[k - 1 < 0 ? 51 : k - 1];
            ActorDic[k].JumpCell = ActorDic[k + 4 > 51 ? k - 48 : k + 4];
            ActorDic[k].MagicCell = MagicCell.Jump;
            if (k % 13 == 0)
                ActorDic[k].MagicCell = MagicCell.Airport;
        }
        for (int L = 52; L < ActorDic.Count; L++)
        {
            ActorDic[L].NextCell = ActorDic[L + 1 > ActorDic.Count - 1 ? 0 : L + 1];
            ActorDic[L].LastCell = ActorDic[L - 1 < 0 ? ActorDic.Count - 1 : L - 1];
            if (L == 56 || L == 61 || L == 66 || L == 71)
                ActorDic[L].NextCell = null;
        }
        //单独处理的格子
        ActorDic[4].JumpCell = ActorDic[16];
        ActorDic[17].JumpCell = ActorDic[29];
        ActorDic[30].JumpCell = ActorDic[42];
        ActorDic[43].JumpCell = ActorDic[3];
        ActorDic[52].LastCell = ActorDic[10];
        ActorDic[57].LastCell = ActorDic[23];
        ActorDic[62].LastCell = ActorDic[36];
        ActorDic[69].LastCell = ActorDic[49];
        ActorDic[10].ColorCell = ActorDic[52];
        ActorDic[23].ColorCell = ActorDic[57];
        ActorDic[36].ColorCell = ActorDic[62];
        ActorDic[49].ColorCell = ActorDic[69];
    }
}
