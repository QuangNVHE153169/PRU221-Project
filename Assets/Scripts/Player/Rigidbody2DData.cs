using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[System.Serializable]

public class Rigidbody2DData
{
    public SerializableVector2 position;
    public SerializableVector2 velocity;
    public Rigidbody2DData(Rigidbody2D rigidbody2D)
    {
        if (rigidbody2D == null)
            return;
        position = new SerializableVector2(rigidbody2D.position);
        velocity = new SerializableVector2(rigidbody2D.velocity);
    }

    internal void Rigidbody2D(Player player)
    {
        player.rb2d.position = this.position.Vector2();
        player.rb2d.velocity = this.velocity.Vector2();
    }
    public Rigidbody2D Rigidbody2D()
    {
        Rigidbody2D rigidbody2D = new Rigidbody2D();
        rigidbody2D.position = this.position.Vector2();
        rigidbody2D.velocity = this.velocity.Vector2();
        return rigidbody2D;
    }
}
