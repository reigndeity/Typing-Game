using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Typer : MonoBehaviour
{
    [Header("Word Container")]
    public WordBank wordBank = null;

    public TextMeshProUGUI wordOutput = null;

    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    public Animator textAnimator;

    public AudioSource audioSource;

    public bool canType;
    void Start()
    {
        canType = true;
        SetCurrentWord();
        textAnimator = FindObjectOfType<Animator>();
        ReturnNormalText();
        audioSource = GetComponent<AudioSource>();
        
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
        wordBank.CorrectAnswer();
    }
    
    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
        {
            string keysPressed = Input.inputString;
            audioSource.Play();
            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
            else
            {
                Debug.Log("PRESS ONE KEY AT A TIME STUPID");
                textAnimator.SetBool("isRight", false);
                GameManager.instance.WrongLetter();
                Invoke("ReturnNormalText", 0.21f);
            }
                
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            
            RemoveLetter();
            if (IsWordComplete())
                SetCurrentWord();
                
        }
        else
        {
            textAnimator.SetBool("isRight", false);
            GameManager.instance.WrongLetter();
            Debug.Log("WRONG KEY DUMBASS");
            Invoke("ReturnNormalText", 0.21f);
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        ReturnNormalText();
        return remainingWord.Length == 0;
    }
    public void ReturnNormalText()
    {
        textAnimator.SetBool("isRight", true);
    }
}
