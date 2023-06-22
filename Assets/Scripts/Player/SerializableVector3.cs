using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]

public class SerializableVector3
{
    public float x;
    public float y;
    public float z;

    [JsonIgnore]
    public Vector3 UnityVector
    {
        get
        {
            return new Vector3(x, y, z);
        }
    }

    public SerializableVector3(Vector3 v)
    {
        if (v == null)
            return;
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public static Vector3 DeserializableVector3(SerializableVector3 serializableVector3)
    {
        return new Vector3(serializableVector3.x, serializableVector3.y, serializableVector3.z);
    }
    public Vector3 Vector3()
    {
        return new Vector3(x, y, z);
    }

    public static List<SerializableVector3> GetSerializableList(List<Vector3> vList)
    {
        List<SerializableVector3> list = new List<SerializableVector3>(vList.Count);
        for (int i = 0; i < vList.Count; i++)
        {
            list.Add(new SerializableVector3(vList[i]));
        }
        return list;
    }

    public static List<Vector3> GetSerializableList(List<SerializableVector3> vList)
    {
        List<Vector3> list = new List<Vector3>(vList.Count);
        for (int i = 0; i < vList.Count; i++)
        {
            list.Add(vList[i].UnityVector);
        }
        return list;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
