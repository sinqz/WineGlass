using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapPing : MonoSingletonTemplateScript<MapPing>
{
    public PathCell[] MapSame;//相同颜色赋值
    public PathCell[] MapDifferent;//不同颜色赋值
    public PlayerStart[] MapStar;//起点位置
    private int Num;
    private void Start()
    {
        OnLoad();
    }

    void OnLoad()
    {
        Num = 0;
        //相同颜色赋值
        for (int i = 0; i < MapSame.Length; i++)
        {
            if (i < 6)
            {
                //颜色
                MapSame[i].CellType = colorCell.BULE;
                //下一步
                if (i != 5)
                    MapSame[i].NextCell = MapSame[i + 1];
                //上一步
                if (i == 0)
                    MapSame[i].LastCell = MapDifferent[0];
                else
                    MapSame[i].LastCell = MapSame[i - 1];

            }
            else if (i >= 6 && i < 12)
            {
                MapSame[i].CellType = colorCell.YELLOW;
                //下一步
                if (i != 11)
                    MapSame[i].NextCell = MapSame[i + 1];
                //上一步
                if (i == 6)
                    MapSame[i].LastCell = MapDifferent[13];
                else
                    MapSame[i].LastCell = MapSame[i - 1];

            }
            else if (i >= 12 && i < 18)
            {
                MapSame[i].CellType = colorCell.RAD;
                //下一步
                if (i != 17)
                    MapSame[i].NextCell = MapSame[i + 1];
                //上一步
                if (i == 12)
                    MapSame[i].LastCell = MapDifferent[26];
                else
                    MapSame[i].LastCell = MapSame[i - 1];
            }
            else
            {
                MapSame[i].CellType = colorCell.GREEN;
                //下一步
                if (i != MapSame.Length - 1)
                    MapSame[i].NextCell = MapSame[i + 1];
                //上一步
                if (i == 18)
                    MapSame[i].LastCell = MapDifferent[39];
                else
                    MapSame[i].LastCell = MapSame[i - 1];

            }
        }

        //不同颜色赋值
        for (int i = 0; i < MapDifferent.Length; i++)
        {
            MapDifferent[i].CellType = (colorCell)(i % 4);
            if (i == 0)
                MapDifferent[i].NextCell = MapDifferent[MapDifferent.Length - 1 - i];
            else
                MapDifferent[i].NextCell = MapDifferent[i - 1];

            if (i % 13 == 0)//循环一圈同色格子处理
            {
                MapDifferent[i].JumpCell = null;
                MapDifferent[i].ColorCell = MapSame[Num * 6];
                Num++;
            }

            if (i < 4)//可跳跃的格子
            {
                MapDifferent[i].JumpCell = MapDifferent[MapDifferent.Length - 4 + i];
            }
            else
            {

                if (i == 6)
                    MapDifferent[i].JumpCell = MapDifferent[MapDifferent.Length - i];
                else if (i == 10)
                    MapDifferent[i].JumpCell = MapDifferent[6];
                else if (i == 19)
                    MapDifferent[i].JumpCell = MapDifferent[7];
                else if (i == 23)
                    MapDifferent[i].JumpCell = MapDifferent[19];
                else if (i == 32)
                    MapDifferent[i].JumpCell = MapDifferent[20];
                else if (i == 36)
                    MapDifferent[i].JumpCell = MapDifferent[32];
                else if (i == 45)
                    MapDifferent[i].JumpCell = MapDifferent[33];
                else if (i == 49)
                    MapDifferent[i].JumpCell = MapDifferent[45];
                else if (i % 13 != 0)
                    MapDifferent[i].JumpCell = MapDifferent[i - 4];
            }

            if (i == MapDifferent.Length - 1)//上一步
                MapDifferent[i].LastCell = MapDifferent[0];
            else
                MapDifferent[i].LastCell = MapDifferent[i + 1];

            if (i == 6 || i == 10 || i == 19 || i == 23 || i == 36 || i == 32 || i == 45 || i == 49)
                MapDifferent[i].MagicCell = MagicCell.Airport;
        }

        for (int i = 0; i < MapStar.Length; i++)
        {
            for (int j = 0; j < MapStar[i].MapPlayerStar.Length; j++)
            {
                MapStar[i].MapPlayerStar[j].CellType = (colorCell)(i);
                if (j != 0)
                    MapStar[i].MapPlayerStar[j].NextCell = MapStar[i].MapPlayerStar[0];
                else
                {
                    if (i == 0)
                        MapStar[i].MapPlayerStar[j].NextCell = MapDifferent[MapDifferent.Length - 3];
                    else if (i == 1)
                        MapStar[i].MapPlayerStar[j].NextCell = MapDifferent[10];
                    else if (i == 2)
                        MapStar[i].MapPlayerStar[j].NextCell = MapDifferent[23];
                    else
                        MapStar[i].MapPlayerStar[j].NextCell = MapDifferent[36];
                }
            }
        }

    }


}


[Serializable]
public struct PlayerStart
{
    public PathCell[] MapPlayerStar;
}