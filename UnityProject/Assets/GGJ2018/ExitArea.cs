using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitArea : MonoBehaviour {


    int numPlayer = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterPlayer cp = collision.gameObject.GetComponent<CharacterPlayer>();
        if (cp != null)
            numPlayer++;

        if (numPlayer == 2)
            SceneManager.LoadScene(2);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CharacterPlayer cp = collision.gameObject.GetComponent<CharacterPlayer>();
        if (cp != null)
            numPlayer--;
    }

}
