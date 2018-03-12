using UnityEngine;
using System.Collections;

public class LightManager
{
    private GameObject lightObj;

    public LightManager()
    {
        this.lightObj = GameObject.Find("Directional Light");
    }

    public void SetLightRandomly()
    {
        var r = Random.Range(0.8f, 1.0f);
        var g = Random.Range(0.7f, 1.0f);
        var b = Random.Range(0.5f, 0.9f);
        this.lightObj.GetComponent<Light>().color = new Color(r, g, b, 1.0f);
        var x = Random.Range(0.0f, 40.0f);
        var y = Random.Range(2.0f, 20.0f);
        var z = Random.Range(-24.0f, 16.0f);
        this.lightObj.GetComponent<Light>().transform.position = new Vector3(x, y, z);
    }
}
