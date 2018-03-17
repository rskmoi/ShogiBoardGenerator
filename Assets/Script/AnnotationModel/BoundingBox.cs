using UnityEngine;
using UnityEditor;
using System.Xml.Serialization;

public class BoundingBox
{
    public BoundingBox() { }
    public BoundingBox(int xmin, int ymin, int xmax, int ymax)
    {
        this.Xmin = xmin;
        this.Ymin = ymin;
        this.Xmax = xmax;
        this.Ymax = ymax;
    }

    [XmlElement(ElementName = "xmin")]
    public int Xmin { get; set; }
    [XmlElement(ElementName = "ymin")]
    public int Ymin { get; set; }
    [XmlElement(ElementName = "xmax")]
    public int Xmax { get; set; }
    [XmlElement(ElementName = "ymax")]
    public int Ymax { get; set; }
}