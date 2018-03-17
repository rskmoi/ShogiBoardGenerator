using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class KomaAnnotation
{
    public KomaAnnotation() { }
    public KomaAnnotation(string name, BoundingBox bndBox)
    {
        this.Name = name;
        this.BndBox = bndBox;
    }
    [XmlElement(ElementName = "name")]
    public string Name { get; set; }
    [XmlElement(ElementName = "bndbox")]
    public BoundingBox BndBox { get; set; }
}
