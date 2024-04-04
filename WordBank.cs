using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line for UI components
using System.Linq;

public class WordBank : MonoBehaviour
{
    public AudioSource audioSource;
    private Typer typer;
    private List<string> originalWords = new List<string>()
    {
        "Despacito", 
        "Among",
        "Sussy",
        "Amazing",
        "Imposter",
        "Oniichan",
        "University",
        "Twenty",
        "Seven",
        "Inch",
        "Monitor",
        "Classmates",
        "Never",
        "Gonna",
        "Give",
        "You",
        "Up",
        "Omae",
        "Wamou",
        "Shindeiru",
    };

    public List<string> workingWords = new List<string>();
    public Image progressBar; // Reference to your image UI component

    private void Awake()
    {
        workingWords.AddRange(originalWords);
        ShuffleWords(workingWords);
        ConvertToLower(workingWords);
        UpdateProgressBar(); // Call this method to update the progress bar initially


        audioSource = GetComponent<AudioSource>();
    }

    private void ShuffleWords(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i, list.Count);
            string temporary = list[i];

            list[i] = list[random];
            list[random] = temporary;
        }
    }

    private void ConvertToLower(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
            list[i] = list[i].ToLower();
    }

    public string GetWord()
    {
        string newWord = string.Empty;
        if (workingWords.Count != 0)
        {
            newWord = workingWords.Last();
            workingWords.Remove(newWord);
            UpdateProgressBar(); // Call method to update progress bar
        }
        else
        {
            GameManager.instance.GameOver();
            progressBar.fillAmount = 1.0f;
        }

        return newWord;
    }

    private void UpdateProgressBar()
    {
        // Calculate progress and update the fill amount of the image
        float progress = 1.0f - ((float)workingWords.Count) / originalWords.Count;
        progressBar.fillAmount = progress - 0.10f;
    }

    public void CorrectAnswer()
    {
        audioSource.Play();
    }
}
