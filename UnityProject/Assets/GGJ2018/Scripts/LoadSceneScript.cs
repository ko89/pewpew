using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneScript : MonoBehaviour {


    public void LoadScene(int sceneNumber)
    {
        Application.LoadLevel(sceneNumber);
    }
}
