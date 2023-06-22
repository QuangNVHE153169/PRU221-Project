using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]

public class FixedJoystickData
{
    public SerializableVector2 joystickPosition;
    public SerializableQuaternion joystickRotation;
    public bool enabled;
    public string name;

    public FixedJoystickData(FixedJoystick joystick)
    {
        if (joystick == null)
            return;
        joystickPosition = new SerializableVector2(joystick.transform.position);
        joystickRotation = new SerializableQuaternion(joystick.transform.rotation);
        enabled = joystick.enabled;
        name = joystick.name;
    }

    internal void FixedJoystick(FixedJoystick joystick)
    {
        joystick.transform.position = this.joystickPosition.Vector2();
        joystick.transform.rotation = this.joystickRotation.Quaternion();
        joystick.name = name;
        joystick.enabled = enabled;

    }
}
