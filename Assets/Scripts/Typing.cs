using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{

    //public Text typedText;
    public Orders orderScript;
    public Text placeholder;
    public Text scoreLabel;

    private AudioSource hit;

    //Score muliplyer = (letters in word) * letterScore
    private int letterScore = 1;
    private int score = 0;

    private string word = "";
    private string wordRemaining;
    private int ordersTyped = 0;

    // Use this for initialization
    void Start()
    {
        hit = GetComponent<AudioSource>();
        wordRemaining = word;
        if (placeholder.text.Equals(word) == false)
        {
            placeholder.text = word;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && word.Length != 0)
        {
            if (Regex.IsMatch(Input.inputString, "([A-Za-z]+)") == true && wordRemaining.Length > 0)
            {
                string textPressed = Input.inputString;

                char letter = wordRemaining.ToCharArray()[0];
                if (textPressed[0] == letter)
                {
                    //Play soundclip
                    hit.Play();

                    wordRemaining = wordRemaining.Substring(1, wordRemaining.Length - 1).Trim();
                    placeholder.text = wordRemaining;
                }
                else
                {
                    transform.root.Find("Shake").GetComponent<ScreenShake>().ShakeScreen(0.2f);
                }
                if (wordRemaining.Length == 0)
                {
                    DoneTyping();
                    GetNewWord();
                }
            }
        }
        else
        {
            GetNewWord();
        }
    }

    private void DoneTyping()
    {
        orderScript.FinishedOrder(word);
        //Reset it for getnew
        //score += word.Length * letterScore;
        score += letterScore;
        scoreLabel.text = "Gold: " + score;
        ordersTyped++;
        word = "";
    }

    private void GetNewWord()
    {
        if (word.Length == 0)
        {
            //could return ""
            int amount = Mathf.FloorToInt(ordersTyped / 10f);
            orderScript.suffixLevel = 4 + amount;
            word = orderScript.GetNextOrder();

            if (word.Length != 0)
            {
                wordRemaining = word;
                placeholder.text = word;
            }
        }
    }
}
