using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCatcher : MonoBehaviour
{

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Shot s = collision.gameObject.GetComponent<Shot>();

        if (s != null)
        {
            //transform.Find("Explosion").gameObject.SetActive(true);
            transform.parent.gameObject.SendMessage("Hit", s);
        }
    }
}
