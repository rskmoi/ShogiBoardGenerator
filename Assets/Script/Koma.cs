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
        return this.turn + this.status + this.Name;
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
        this.window.transform.position = windowPosition;
        this.xmin = UIPositionToAnnotationPositionX(windowPosition.x - 23.0f);
        this.ymin = UIPositionToAnnotationPositionX(windowPosition.y - 23.0f);
        this.xmax = UIPositionToAnnotationPositionX(windowPosition.x + 23.0f);
        this.ymax = UIPositionToAnnotationPositionX(windowPosition.y + 23.0f);
    }

    private int UIPositionToAnnotationPositionX(float uiPosition)
    {
        var ratio = (uiPosition - (-400.0f)) / 800.0f;
        var annotationPositionX = Mathf.FloorToInt(640.0f * ratio);
        return annotationPositionX;
    }

    private int UIPositionToAnnotationPositionY(float uiPosition)
    {
        var ratio = (300.0f - uiPosition) / 600.0f;
        var annotationPositionY = Mathf.FloorToInt(480.0f * ratio);
        return annotationPositionY;
    }

}
