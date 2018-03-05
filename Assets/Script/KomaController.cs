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
    
    private List<PlacementInfo> placements;
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
        komaList = new List<GameObject>();
        komaList.Add(ousho);
        komaList.Add(hisya);
        komaList.Add(kaku);
        komaList.Add(kin);
        komaList.Add(gin);
        komaList.Add(kei);
        komaList.Add(kyou);
        komaList.Add(fu);

        placements = new List<PlacementInfo>();
        for (int suji = 1; suji < 10; suji++)
        {
            for (int dan = 1; dan < 10; dan++)
            {
                var turn = GetTurnRandomly();
                var placementInfo = new PlacementInfo(suji, dan);
                placements.Add(placementInfo);
            }
        }

        ary = new int[81];
        for (int komaIdx = 0; komaIdx < 81; komaIdx++)
        {
            ary[komaIdx] = komaIdx;
        }
        ary = ary.OrderBy(i => System.Guid.NewGuid()).ToArray();
    }

    private string GetTurnRandomly()
    {
        int randomVal = r.Next(2);
        return (randomVal == 0) ? "Black" : "White";
    }

    // Update is called once per frame
    void Update () {
        count++;
        if(count % 50 == 0)
        {
            for (int komaNum = 0; komaNum < 8; komaNum++)
            {
                var randomIdx = ary[komaNum];
                komaList[komaNum].transform.position = placements[randomIdx].position;
                komaList[komaNum].transform.rotation = placements[randomIdx].rotation;
            }

            foreach (PlacementInfo plInfo in placements)
            {
                plInfo.Reset();
            }

            ary = ary.OrderBy(i => System.Guid.NewGuid()).ToArray();
        }
	}
}

public class PlacementInfo
{
    public string placementName;
    public string PlacementName
    {
        get
        {
            return placementName;
        }
    }
    public string komaType; // "OnBoard" or "Captured"(盤上/持ち駒) こういうのってC#だとenumにするんだっけ
    public string KomaType
    {
        get
        {
            return komaType;
        }
    }
    public string Turn; // "Black" or "White"(先手/後手)
    public string Status; //"Promoted" or "Raw"
    public int suji;
    public int dan;

    public Vector3 position;
    public Quaternion rotation;

    private float baseX = 14.2f;
    private float baseY = 1.0f;
    private float baseZ = -9.5f;

    private float distanceX = 1.357f;
    private float distanceZ = 1.463f;

    private float statusCompensationY = 0.3f;
    private float turnCompensationZ = 1.0f;

    public PlacementInfo(int suji, int dan)
    {
        this.suji = suji;
        this.dan = dan;

        this.komaType = "OnBoard";
        this.placementName = suji.ToString() + dan.ToString();
        this.Turn = SetTurn();
        this.Status = SetStatus();
        this.position = CalculatePosition(suji, dan, this.Turn, this.Status);
        this.rotation = CalculateRotation(this.Turn, this.Status);
    }

    public PlacementInfo(string turn, string name)
    {
        this.komaType = "Captured";
        this.placementName = "name";
        this.Turn = turn;
    }

    public string SetTurn()
    {
        int randomVal = Random.Range(0, 2);
        return (randomVal == 0) ? "Black" : "White";
    }

    public string SetStatus()
    {
        int randomVal = Random.Range(0, 2);
        return (randomVal == 0) ? "Raw" : "Promoted";
    }

    public void Reset()
    {
        this.Turn = SetTurn();
        this.Status = SetStatus();
        this.position = CalculatePosition(this.suji, this.dan, this.Turn, this.Status);
        this.rotation = CalculateRotation(this.Turn, this.Status);
    }

    private Vector3 CalculatePosition(int suji, int dan, string turn, string status)
    {
        var compensationY = (status == "Raw") ? this.statusCompensationY : 0.0f;
        var compensationZ = ((turn == "White") == (status == "Raw")) ? this.turnCompensationZ : 0.0f;

        var x = baseX + (suji - 1) * distanceX;
        var y = baseY + compensationY;
        var z = baseZ + (dan - 1) * distanceZ + compensationZ;

        return new Vector3(x, y, z);
    }

    private Quaternion CalculateRotation(string turn, string status)
    {
        var rotationX = (status == "Promoted") ? 0 : 180;
        var rotationY = (turn == "White") ? 0 : 180;

        return Quaternion.Euler(rotationX, rotationY, 0);
    }
}
