using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[Serializable]
public class SerializableQuaternion
{
    public float x;
    public float y;
    public float z;
    public float w;



    public SerializableQuaternion(Quaternion quaternion)
    {
        if (quaternion == null)
            return;
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }

    public Quaternion Quaternion()
    {
        return new Quaternion(x, y, z, w);
    }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}