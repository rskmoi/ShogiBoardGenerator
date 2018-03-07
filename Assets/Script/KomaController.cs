using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KomaController : MonoBehaviour {
    public Transform target;
    int count;
    private int[] ary;

    private GameObject ousho;
    private GameObject hisya;
    private GameObject kaku;
    private GameObject kin;
    private GameObject gin;
    private GameObject kei;
    private GameObject kyou;
    private GameObject fu;
    
    private Placements placements;
    private List<GameObject> komaList;
    System.Random r = new System.Random(1);

    // Use this for initialization
    void Start () {
        count = 0;
        ousho = GameObject.Find("OUSHO_01");
        hisya = GameObject.Find("HISHA_01");
        kaku = GameObject.Find("KAKU_01");
        kin = GameObject.Find("KIN_01");
        gin = GameObject.Find("GIN_01");
        kei = GameObject.Find("KEIMA_01");
        kyou = GameObject.Find("KYOSHA_01");
        fu = GameObject.Find("FU_01");
        komaList = new List<GameObject>
        {
            ousho,
            hisya,
            kaku,
            kin,
            gin,
            kei,
            kyou,
            fu
        };
        placements = new Placements();
    }

    // Update is called once per frame
    void Update () {
        count++;
        if(count % 50 == 0)
        {
            this.placements.MakePlaceRandomly(8, komaList);
            this.placements.Reset();
        }
	}
}

