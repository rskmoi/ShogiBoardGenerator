using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class KomaController : MonoBehaviour {
    private int count;
    private Placements placements;
    private List<Koma> komaList;
    private LightManager lightManager;
  
    // Use this for initialization
    void Start () {
        this.count = 0;
        this.komaList = new List<Koma>();

        for (int i = 1; i < 3; i++)
        {
            var strNum = i.ToString().PadLeft(2, '0');
            var ousho = GameObject.Find("OUSHO_" + strNum);
            var hisha = GameObject.Find("HISHA_" + strNum);
            var kaku = GameObject.Find("KAKU_" + strNum);
            var oushoWindow = GameObject.Find("OUSHO_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            var hishaWindow = GameObject.Find("HISHA_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            var kakuWindow = GameObject.Find("KAKU_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            this.komaList.Add(new Koma("Ousho", ousho, oushoWindow));
            this.komaList.Add(new Koma("Hisha", hisha, hishaWindow));
            this.komaList.Add(new Koma("Kaku", kaku, kakuWindow));
        }

        for (int i = 1; i < 5; i++)
        {
            var strNum = i.ToString().PadLeft(2, '0');
            var kin = GameObject.Find("KIN_" + strNum);
            var gin = GameObject.Find("GIN_" + strNum);
            var keima = GameObject.Find("KEIMA_" + strNum);
            var kyosha = GameObject.Find("KYOSHA_" + strNum);
            var kinWindow = GameObject.Find("KIN_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            var ginWindow = GameObject.Find("GIN_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            var keimaWindow = GameObject.Find("KEIMA_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            var kyoshaWindow = GameObject.Find("KYOSHA_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            this.komaList.Add(new Koma("Kin", kin, kinWindow));
            this.komaList.Add(new Koma("Gin", gin, ginWindow));
            this.komaList.Add(new Koma("Keima", keima, keimaWindow));
            this.komaList.Add(new Koma("Kyosha", kyosha, kyoshaWindow));
        }

        for (int i = 1; i < 19; i++)
        {
            var strNum = i.ToString().PadLeft(2, '0');
            var fu = GameObject.Find("FU_" + strNum);
            var fuWindow = GameObject.Find("FU_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            this.komaList.Add(new Koma("Fu", fu, fuWindow));
        }

        var showWindow = true;

        if (!showWindow)
        {
            foreach (var koma in this.komaList)
            {
                koma.Window.gameObject.SetActive(false);
            }
        }

        this.placements = new Placements();
        this.lightManager = new LightManager();
    }

    // Update is called once per frame
    void Update () {
        count++;
        if (count % 50 == 0)
        {
            this.placements.MakePlaceRandomly(this.komaList);
            var fileName = "C:\\Users\\rei\\Desktop\\anns\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            Annotator.Annotate(this.komaList, fileName + ".xml");
            this.placements.Reset();
            this.lightManager.SetLightRandomly();
        }
    }
}

