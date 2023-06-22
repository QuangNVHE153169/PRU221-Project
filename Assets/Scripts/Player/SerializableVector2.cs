using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]

public class SerializableVector2
{
    public float x;
    public float y;

    public SerializableVector2(Vector2 vector)
    {
        if (vector == null)
            return;
        x = vector.x;
        y = vector.y;
    }

    public Vector2 Vector2()
    {
        return new Vector2(x, y);
    }
}
