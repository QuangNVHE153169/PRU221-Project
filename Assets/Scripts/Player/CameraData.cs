using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[System.Serializable]

public class CameraData
{
    public string cameraName;
    public SerializableQuaternion rotation;
    public SerializableVector3 position;

    public CameraData(Camera camera)
    {
        if (camera == null)
            return;
        this.cameraName = camera.name;
        position = new SerializableVector3(camera.transform.position);
        rotation = new SerializableQuaternion(camera.transform.rotation);
    }

    public Camera Camera()
    {
        Camera camera = GameObject.Find(this.cameraName).GetComponent<Camera>();
        camera.transform.position = this.position.Vector3();
        camera.transform.rotation = this.rotation.Quaternion();
        return camera;
    }
}