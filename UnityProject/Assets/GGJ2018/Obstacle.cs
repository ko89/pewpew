using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject _destructionPrefab;
    public float _destructionDelay;

	// Update is called once per frame
	public void Hit (object shot)
    {
        StartCoroutine(Desintegrate());		
	}

    public IEnumerator Desintegrate()
    {
        GameObject.Instantiate<GameObject>(_destructionPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_destructionDelay);
        Destroy(this.gameObject);
    }
}
