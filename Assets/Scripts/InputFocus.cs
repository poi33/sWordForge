using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFocus : MonoBehaviour {

    private InputField innText;
    public string target;

	// Use this for initialization
	void Start () {
        innText = GetComponent<InputField>();
        innText.Select();
        //innText.ActivateInputField();
	}
	
	// Update is called once per frame
	void Update () {
        if (innText.isFocused == false)
        {
            innText.Select();
        }
	}

    public void OnInput(string text)
    {
        if (innText.text.ToLower().Equals(target))
        {
            Debug.Log("Super!");
        }
    }
}
