using UnityEngine;
using System.Collections;

public class Koma
{
    private string name;
    public string Name
    {
        get
        {
            return this.name;
        }
    }
    private GameObject komaObj;
    public GameObject KomaObj
    {
        get
        {
            return komaObj;
        }
    }
    private RectTransform window;
    public RectTransform Window
    {
        get
        {
            return window;
        }
    }

    private int xmin;
    public int Xmin
    {
        get
        {
            return xmin;
        }
    }
    private int ymin;
    public int Ymin
    {
        get
        {
            return ymin;
        }
    }
    private int xmax;
    public int Xmax
    {
        get
        {
            return xmax;
        }
    }
    private int ymax;
    public int Ymax
    {
        get
        {
            return ymax;
        }
    }
    private string turn;
    public string Turn
    {
        get
        {
            return this.turn;
        }
    }
    private string status;
    public string Status
    {
        get
        {
            return this.status;
        }
    }

    public Koma(string name, GameObject komaObj, RectTransform window)
    {
        this.name = name;
        this.komaObj = komaObj;
        this.window = window;
    }

    public string GetClassName()
    {
        return KomaUtil.GetClassName(this.Name, this.turn, this.status);
    }

    public void SetTurn(string turn)
    {
        this.turn = turn;
    }

    public void SetStatus(string status)
    {
        this.status = status;
    }

    public void SetKomaPosition(Vector3 komaPosition)
    {
        this.komaObj.transform.position = komaPosition;
    }

    public void SetKomaRotation(Quaternion komaRotation)
    {
        this.komaObj.transform.rotation = komaRotation;
    }

    public void SetWindowPosition(Vector3 windowPosition)
    {
        this.window.position = windowPosition;
        this.xmin = Mathf.FloorToInt(windowPosition.x - 18.4f);
        this.ymin = Mathf.FloorToInt(windowPosition.y - 18.4f);
        this.xmax = Mathf.FloorToInt(windowPosition.x + 18.4f);
        this.ymax = Mathf.FloorToInt(windowPosition.y + 18.4f);
    }
}
