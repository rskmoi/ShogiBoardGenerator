using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[XmlRoot(ElementName = "annotation")]
public class PictureAnnotation
{
    [XmlElement(ElementName = "object")]
    public List<KomaAnnotation> KomaAnnotationList { get; set; }
}