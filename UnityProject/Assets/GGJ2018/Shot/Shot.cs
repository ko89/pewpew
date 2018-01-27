using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public string _characterID;
    public float _travelDistance = 0;
    public int _bounceCount = 0;
    public string CharacterID { get { return _characterID; } set { _characterID = value; } }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _bounceCount++;
        /*
        CharacterCatcher cc = collision.gameObject.GetComponent<CharacterCatcher>();
        if (cc != null)
            Destroy(this.gameObject);*/
    }

    public void FixedUpdate()
    {
        _travelDistance += GetComponent<Rigidbody2D>().velocity.magnitude * Time.fixedDeltaTime;
    }
}
