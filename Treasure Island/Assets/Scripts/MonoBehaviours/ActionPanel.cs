using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour {

    private GameObject descriptionPanel;
    private GameObject answerPanel;
    private GameObject[] answerButtons;

    private void Awake()
    {
        descriptionPanel = transform.GetChild(0).gameObject;
        answerPanel = transform.GetChild(1).gameObject;

        answerButtons = new GameObject[4];
        for (int i = 0; i < answerPanel.transform.childCount; i++)
        {
            answerButtons[i] = answerPanel.transform.GetChild(i).gameObject;
        }

    }

    public void DisplayAnswers (string[] answers)
    {
        descriptionPanel.SetActive(false);

        for (int i = 0; i < answers.Length; i++)
        {
            if (!System.String.IsNullOrEmpty(answers[i]))
            {
                answerButtons[i].transform.GetChild(0).GetComponent<Text>().text = answers[i];
                answerButtons[i].GetComponent<Button>().interactable = true;
            } else
            {
                answerButtons[i].transform.GetChild(0).GetComponent<Text>().text = "";
                answerButtons[i].GetComponent<Button>().interactable = false;
            }
            
        }

        answerPanel.SetActive(true);
    }

    public void DisplayDescription (string text)
    {
        answerPanel.SetActive(false);

        descriptionPanel.GetComponent<Text>().text = text;

        descriptionPanel.SetActive(true);
    }

    public void DisplayNone ()
    {
        answerPanel.SetActive(false);
        descriptionPanel.SetActive(false);
    }

}
