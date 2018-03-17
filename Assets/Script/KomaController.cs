using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class KomaController : MonoBehaviour {
    private int count;
    private Placements placements;
    private List<GameObject> komaList;
    private List<RectTransform> windowList;
    private LightManager lightManager;
  
    // Use this for initialization
    void Start () {
        this.count = 0;
        this.komaList = new List<GameObject>();
        this.windowList = new List<RectTransform>();

        for (int i = 1; i < 3; i++)
        {
            var strNum = i.ToString().PadLeft(2, '0');
            var ousho = GameObject.Find("OUSHO_" + strNum);
            var hisha = GameObject.Find("HISHA_" + strNum);
            var kaku = GameObject.Find("KAKU_" + strNum);
            var oushoWindow = GameObject.Find("OUSHO_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            var hishaWindow = GameObject.Find("HISHA_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            var kakuWindow = GameObject.Find("KAKU_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            this.komaList.Add(ousho);
            this.komaList.Add(hisha);
            this.komaList.Add(kaku);
            this.windowList.Add(oushoWindow);
            this.windowList.Add(hishaWindow);
            this.windowList.Add(kakuWindow);
        }

        for (int i = 1; i < 5; i++)
        {
            var strNum = i.ToString().PadLeft(2, '0');
            var kin = GameObject.Find("KIN_" + strNum);
            var gin = GameObject.Find("GIN_" + strNum);
            var kei = GameObject.Find("KEIMA_" + strNum);
            var kyou = GameObject.Find("KYOSHA_" + strNum);
            var kinWindow = GameObject.Find("KIN_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            var ginWindow = GameObject.Find("GIN_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            var keiWindow = GameObject.Find("KEIMA_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            var kyouWindow = GameObject.Find("KYOSHA_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            this.komaList.Add(kin);
            this.komaList.Add(gin);
            this.komaList.Add(kei);
            this.komaList.Add(kyou);
            this.windowList.Add(kinWindow);
            this.windowList.Add(ginWindow);
            this.windowList.Add(keiWindow);
            this.windowList.Add(kyouWindow);
        }

        for (int i = 1; i < 19; i++)
        {
            var strNum = i.ToString().PadLeft(2, '0');
            var fu = GameObject.Find("FU_" + strNum);
            var fuWindow = GameObject.Find("FU_" + strNum + "_WINDOW").GetComponent<RectTransform>();
            this.komaList.Add(fu);
            this.windowList.Add(fuWindow);
        }

        var showWindow = false;
        if (!showWindow)
        {
            foreach (var window in this.windowList)
            {
                window.gameObject.SetActive(false);
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
            this.placements.MakePlaceRandomly(this.komaList, this.windowList);
            this.placements.Reset();
            this.lightManager.SetLightRandomly();

            // windowObj.SetActive(false);
        }
    }
}

