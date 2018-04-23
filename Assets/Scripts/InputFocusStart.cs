using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputFocusStart : MonoBehaviour {

    private InputField innText;
    public string target;
	// Use this for initialization
	void Start () {
        innText = GetComponent<InputField>();
        innText.Select();
        innText.ActivateInputField();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInput()
    {
        if (innText.text.ToLower().Equals(target))
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Game");
    }
}
