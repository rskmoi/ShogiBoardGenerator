using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Placements
{
    private List<PlacementInfo> placementList;
    private int blackCapturedCount;
    private int whiteCapturedCount;
    private int[] randomIndices;
    System.Random r = new System.Random(1);

    public Placements()
    {
        this.placementList = new List<PlacementInfo>();
        InitializeBoard();
        // InitializeKomadai();
        this.blackCapturedCount = 0;
        this.whiteCapturedCount = 0;
        InitializeRandomIndices();
    }

    /// <summary>
    /// 今後の予定
    /// MakePlaceRandomlyで持ち駒の位置情報もplaceMentlistに格納する
    /// ↓
    /// GetNextPlacementIndfoみたいなイテレーション的なメソッドをつくってとってってもらう
    /// </summary>
    /// <param name="komanumMax"></param>
    /// <param name="komaList"></param>
    public void MakePlaceRandomly(int komanumMax, List<GameObject>  komaList)
    {
        ShuffleIndices();
        var copiedRandomindices = new int[40];
        Array.Copy(this.randomIndices, 0, copiedRandomindices, 0, 40);

        for(int komanum=0; komanum < komanumMax; komanum++)
        {
            var randomIdx = copiedRandomindices[komanum];
            komaList[komanum].transform.position = placementList[randomIdx].position;
            komaList[komanum].transform.rotation = placementList[randomIdx].rotation;
        }
    }

    public void Reset()
    {
        foreach (PlacementInfo plInfo in placementList)
        {
            plInfo.Reset();
        }
        this.blackCapturedCount = 0;
        this.whiteCapturedCount = 0;
    }

    private void InitializeRandomIndices()
    {
        this.randomIndices = new int[81];
        for (int komaIdx = 0; komaIdx < 81; komaIdx++)
        {
            randomIndices[komaIdx] = komaIdx;
        }
    }

    private void ShuffleIndices()
    {
        this.randomIndices = this.randomIndices.OrderBy(i => System.Guid.NewGuid()).ToArray();
    }

    private void InitializeBoard()
    {
        for (int suji = 1; suji < 10; suji++)
        {
            for (int dan = 1; dan < 10; dan++)
            {
                var turn = GetTurnRandomly();
                var placementInfo = new PlacementInfo(suji, dan);
                placementList.Add(placementInfo);
            }
        }
    }

    private void InitializeKomadai()
    {
        foreach(string turn in new List<string> {"Black","White" })
        {
            for(int i=0; i < 8; i++)
            {
                var placementInfo = new PlacementInfo(turn);
            }
        }
    }

    private string GetTurnRandomly()
    {
        int randomVal = r.Next(2);
        return (randomVal == 0) ? "Black" : "White";
    }
}
