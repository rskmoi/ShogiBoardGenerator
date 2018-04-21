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
    public Vector3 rotation3d;
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
        this.rotation3d = CalculateRotation(this.Turn, this.Status);
        this.rotation = Quaternion.Euler(this.rotation3d);
    }

    public PlacementInfo(string turn)
    {
        this.komaType = "Captured";
        this.Turn = turn;
        this.Status = "Raw";
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
            this.rotation3d = CalculateRotation(this.Turn, this.Status);
            this.rotation = Quaternion.Euler(this.rotation3d);
        }
        else if(this.komaType == "Captured")
        {

        }
    }

    private Vector3 CalculatePosition(int suji, int dan, string turn, string status)
    {
        var compensationY = (status == "Raw") ? this.statusCompensationY : 0.0f;

        var compensationZ = (turn == "White") ? this.turnCompensationZ : 0.0f;
        

        var x = baseX + (suji - 1) * distanceX + GetRandomNoiseX();
        var y = baseY + compensationY;
        var z = baseZ + (dan - 1) * distanceZ + compensationZ + GetRandomNoiseZ();

        return new Vector3(x, y, z);
    }

    private Vector3 CalculateRotation(string turn, string status)
    {
        int rotationX;
        int rotationY;

        if((status == "Promoted") && (turn == "Black"))
        {
            rotationX = 0;
            rotationY = 0;
        }
        else if((status == "Promoted") && (turn == "White"))
        {
            rotationX = 0;
            rotationY = 180;
        }
        else if ((status == "Raw") && (turn == "Black"))
        {
            rotationX = 180;
            rotationY = 180;
        }
        else
        {
            rotationX = 180;
            rotationY = 0;
        }

        return new Vector3(rotationX, rotationY + GetRandomNoiseRotationY(), 0);
    }

    private float GetRandomNoiseX()
    {
        return UnityEngine.Random.Range(-0.1f, 0.1f);
    }

    private float GetRandomNoiseZ()
    {
        return UnityEngine.Random.Range(-0.05f, 0.05f);
    }

    private float GetRandomNoiseRotationY()
    {
        return UnityEngine.Random.Range(-2, 2);
    }
}
