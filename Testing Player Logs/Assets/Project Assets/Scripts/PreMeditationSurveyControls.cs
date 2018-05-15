using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreMeditationSurveyControls : MonoBehaviour {
    public GameObject G_GameController = null;

    private int G_Question1Value = 0;
    private GameObject G_Question1ValueDisplay = null;
    private Slider G_Question1Slider = null;

    private int G_Question2Value = 0;
    private GameObject G_Question2ValueDisplay = null;
    private Slider G_Question2Slider = null;

    private InputField G_Question3InputField = null;


    // Use this for initialization
    void Start ()
    {
        G_Question1ValueDisplay = GameObject.Find("Mood Survey Pre-Meditation").transform.Find("Question 1").Find("Current Value").gameObject;
        G_Question1Slider = GameObject.Find("Mood Survey Pre-Meditation").transform.Find("Question 1").Find("Slider").gameObject.GetComponent<Slider>();

        G_Question2ValueDisplay = GameObject.Find("Mood Survey Pre-Meditation").transform.Find("Question 2").Find("Current Value").gameObject;
        G_Question2Slider = GameObject.Find("Mood Survey Pre-Meditation").transform.Find("Question 2").Find("Slider").gameObject.GetComponent<Slider>();

        G_Question3InputField = GameObject.Find("Mood Survey Pre-Meditation").transform.Find("Question 3").Find("InputField").gameObject.GetComponent<InputField>();
    }

    public void UpdateHowAreYouFeelingToday(Slider slider)
    {
        int value = (int)slider.value;
        switch(value)
        {
            case 1: // Poor
                G_Question1ValueDisplay.GetComponent<Text>().text = "Poor";
                G_Question1Value = value;
                break;
            case 2: // Could be better
                G_Question1ValueDisplay.GetComponent<Text>().text = "Could be better";
                G_Question1Value = value;
                break;
            case 3: // OK
                G_Question1ValueDisplay.GetComponent<Text>().text = "OK";
                G_Question1Value = value;
                break;
            case 4: // Pretty Good
                G_Question1ValueDisplay.GetComponent<Text>().text = "Pretty Good";
                G_Question1Value = value;
                break;
            case 5: // Great!
                G_Question1ValueDisplay.GetComponent<Text>().text = "Great!";
                G_Question1Value = value;
                break;
            default:
                break;
        }
    }

    public void UpdateHowStressedDoYouFeel(Slider slider)
    {
        int value = (int)slider.value;
        switch(value)
        {
            case 0: // Not At All
                G_Question2ValueDisplay.GetComponent<Text>().text = "Not At All";
                G_Question2Value = value;
                break;
            case 1: // Normal Stuff
                G_Question2ValueDisplay.GetComponent<Text>().text = "Normal Stuff";
                G_Question2Value = value;
                break;
            case 2: // Kinda Stressed
                G_Question2ValueDisplay.GetComponent<Text>().text = "Kinda Stressed";
                G_Question2Value = value;
                break;
            case 3: // Feeling Pressured
                G_Question2ValueDisplay.GetComponent<Text>().text = "Feeling Pressured";
                G_Question2Value = value;
                break;
            case 4: // Crumbling
                G_Question2ValueDisplay.GetComponent<Text>().text = "Crumbling";
                G_Question2Value = value;
                break;
            case 5: // Extremely Stressed
                G_Question2ValueDisplay.GetComponent<Text>().text = "Extremely Stressed";
                G_Question2Value = value;
                break;
            default:
                break;
        }
    }

    public void Skip() { }

    public void Reset()
    {
        G_Question1Slider.value = 1;
        G_Question1ValueDisplay.GetComponent<Text>().text = "Poor";
        
        G_Question2Slider.value = 0;
        G_Question2ValueDisplay.GetComponent<Text>().text = "Not At All";
        
        G_Question3InputField.text = "";
    }

    public void Cancel() { }

    public void Submit()
    {
        G_GameController.GetComponent<GameController>().SetGameData("pre_survey_question1", G_Question1Value);
        G_GameController.GetComponent<GameController>().SetGameData("pre_survey_question2", G_Question2Value);
        G_GameController.GetComponent<GameController>().SetGameData("pre_survey_question3", G_Question3InputField.text);
        G_GameController.GetComponent<GameController>().SaveGameData();

        //gameObject.SetActive(false);
    }
}
