using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.IO;

public class Annotator
{ 
    public static void Annotate(List<Koma> komaList, string fileName)
    {
        var picAnnotation = new PictureAnnotation();
        var komaAnnotationList = new List<KomaAnnotation>();
        foreach(var koma in komaList)
        {
            var bndBox = new BoundingBox(koma.Xmin, koma.Ymin, koma.Xmax, koma.Ymax);
            var name = koma.GetClassName();
            komaAnnotationList.Add(new KomaAnnotation(name, bndBox));
        }
        picAnnotation.KomaAnnotationList = komaAnnotationList;
        SerializeAnnotation(fileName, picAnnotation);
    }

    private static void SerializeAnnotation(string savePath, PictureAnnotation pinAnnotation)
    {
        using (var streamWriter = new StreamWriter(savePath, false, Encoding.UTF8))
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);
            new XmlSerializer(typeof(PictureAnnotation)).Serialize(streamWriter, pinAnnotation, ns);
        }
    }
}
