using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[System.Serializable]

public class TransformData
{
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public string tag;
    public string name;

    public TransformData(Transform transform)
    {
        if (transform == null)
            return;
        position = new SerializableVector3(transform.position);
        rotation = new SerializableQuaternion(transform.rotation);
        tag = transform.tag;
        name = transform.name;
    }

    internal void Transform(Transform transform)
    {
        transform.position = this.position.Vector3();
        transform.rotation = this.rotation.Quaternion();
        transform.tag = tag;
        transform.name = name;
    }
}
