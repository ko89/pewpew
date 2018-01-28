using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterPlayer characterA;
    public CharacterPlayer characterB;


    public int numLifes;


    private static GameManager m_Instance;
    public static GameManager Instance
    { 
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<GameManager>();
            return m_Instance;
        }
    }

    public CharacterPlayer HollowPlayer
    {
        get
        {
            return characterA._hasSoul ? characterB : characterA;
        }
    }

    public void ResetSoul()
    {

    }


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal static void DoGameOver()
    {
        
    }
}
