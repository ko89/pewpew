using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCatcher : MonoBehaviour {

    public CharacterPlayer _character;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Shot s = collision.gameObject.GetComponent<Shot>();

        if(s != null)
        {
            StartCoroutine(SuckIn(s, collision.gameObject));
        }

    }

    public IEnumerator SuckIn(Shot s, GameObject go)
    {

        if (s._travelDistance < 0.8 || !s._isValid)
            yield break;

        s._isValid = false;
        _character.RecieveSoul();
        yield return new WaitForFixedUpdate();
        while(true)
        {
            Vector3 targetPos = _character._mouthCenter.transform.position;
            Vector3 sourcePos = s.transform.position;
            float dist = (targetPos - sourcePos).magnitude;
            if (dist < 0.05)
                break;

            float vel = 1 / (dist * dist);
            vel = Mathf.Min(vel, 5);
            go.GetComponent<Rigidbody2D>().velocity = vel*(targetPos - sourcePos).normalized;
            yield return new WaitForFixedUpdate();
        }


        go.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

    }
}
