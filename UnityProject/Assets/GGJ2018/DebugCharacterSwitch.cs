using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCharacterSwitch : MonoBehaviour
{
    public string _switchButton = "Jump";


    public CharacterPlayer characterA;
    public CharacterPlayer characterB;

	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown(_switchButton) || Input.GetKeyDown(KeyCode.Tab))
        {
            ControlScheme temp = characterA._controlScheme;
            characterA._controlScheme = characterB._controlScheme;
            characterB._controlScheme = temp;
        }
		
	}
}
