using UnityEngine;
using UnityEditor;

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

    public PlacementInfo(string turn)
    {
        this.komaType = "Captured";
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
        if (this.komaType == "OnBoard")
        {
            this.Turn = SetTurn();
            this.Status = SetStatus();
            this.position = CalculatePosition(this.suji, this.dan, this.Turn, this.Status);
            this.rotation = CalculateRotation(this.Turn, this.Status);
        }
        else if(this.komaType == "Captured")
        {

        }
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
