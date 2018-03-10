using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KomaController : MonoBehaviour {
    private int count;
    private Placements placements;
    private List<GameObject> komaList;
  
    // Use this for initialization
    void Start () {
        this.count = 0;
        this.komaList = new List<GameObject>();

        for(int i = 1; i < 3; i++)
        {
            var strNum = i.ToString().PadLeft(2, '0');
            var ousho = GameObject.Find("OUSHO_" + strNum);
            var hisha = GameObject.Find("HISHA_" + strNum);
            var kaku = GameObject.Find("KAKU_" + strNum);
            this.komaList.Add(ousho);
            this.komaList.Add(hisha);
            this.komaList.Add(kaku);
        }

        for (int i = 1; i < 5; i++)
        {
            var strNum = i.ToString().PadLeft(2, '0');
            var kin = GameObject.Find("KIN_" + strNum);
            var gin = GameObject.Find("GIN_" + strNum);
            var kei = GameObject.Find("KEIMA_" + strNum);
            var kyou = GameObject.Find("KYOSHA_" + strNum);
            this.komaList.Add(kin);
            this.komaList.Add(gin);
            this.komaList.Add(kei);
            this.komaList.Add(kyou);
        }

        for (int i = 1; i < 19; i++)
        {
            var strNum = i.ToString().PadLeft(2, '0');
            var fu = GameObject.Find("FU_" + strNum);
            komaList.Add(fu);
        }
        placements = new Placements();
    }

    // Update is called once per frame
    void Update () {
        count++;
        if (count % 50 == 0)
        {
            this.placements.MakePlaceRandomly(komaList);
            this.placements.Reset();
        }
    }
}

