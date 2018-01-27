using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterPlayer cp = collision.gameObject.GetComponent<CharacterPlayer>();
    }
}
