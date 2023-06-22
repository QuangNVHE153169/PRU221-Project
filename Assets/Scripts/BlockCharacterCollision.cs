using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D characterCollider;
    public Collider2D characterBlockCollider;
    void Start()
    {
        Physics2D.IgnoreCollision(characterCollider, characterBlockCollider,true);  
    }
}
