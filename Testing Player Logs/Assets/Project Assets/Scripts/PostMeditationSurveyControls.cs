using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostMeditationSurveyControls : MonoBehaviour {
    public GameObject G_GameController = null;

    private int G_Question1Value = 0;
    private GameObject G_Question1ValueDisplay = null;
    private Slider G_Question1Slider = null;
    private Slider G_PreMeditationResponse1Slider = null;
    private GameObject G_PreMeditationResponse1ValueDisplay = null;
    private int G_PreMeditationResponse1Value = 0;

    private int G_Question2Value = 0;
    private GameObject G_Question2ValueDisplay = null;
    private Slider G_Question2Slider = null;
    private Slider G_PreMeditationResponse2Slider = null;
    private GameObject G_PreMeditationResponse2ValueDisplay = null;
    private int G_PreMeditationResponse2Value = 0;

    private InputField G_Question3InputField = null;
    private InputField G_PreMeditationResponse3InputField = null;
    private string G_PreMeditationResponse3Value = "";


    // Use this for initialization
    void Start ()
    {
        // Get Objects for Question & Response 1
        G_Question1ValueDisplay = GameObject.Find("Mood Survey Post-Meditation").transform.Find("Question 1").Find("Current Value").gameObject;
        G_Question1Slider = GameObject.Find("Mood Survey Post-Meditation").transform.Find("Question 1").Find("Slider").gameObject.GetComponent<Slider>();
        G_PreMeditationResponse1Slider = GameObject.Find("Mood Survey Post-Meditation").transform.Find("Pre-Meditation Response 1").Find("Slider").gameObject.GetComponent<Slider>();
        G_PreMeditationResponse1ValueDisplay = GameObject.Find("Mood Survey Post-Meditation").transform.Find("Pre-Meditation Response 1").Find("Current Value").gameObject;
        G_GameController.GetComponent<GameController>().GetGameData("pre_survey_question1", ref G_PreMeditationResponse1Value);

        // Get Objects for Question & Response 2
        G_Question2ValueDisplay = GameObject.Find("Mood Survey Post-Meditation").transform.Find("Question 2").Find("Current Value").gameObject;
        G_Question2Slider = GameObject.Find("Mood Survey Post-Meditation").transform.Find("Question 2").Find("Slider").gameObject.GetComponent<Slider>();
        G_PreMeditationResponse2Slider = GameObject.Find("Mood Survey Post-Meditation").transform.Find("Pre-Meditation Response 2").Find("Slider").gameObject.GetComponent<Slider>();
        G_PreMeditationResponse2ValueDisplay = GameObject.Find("Mood Survey Post-Meditation").transform.Find("Pre-Meditation Response 2").Find("Current Value").gameObject;
        G_GameController.GetComponent<GameController>().GetGameData("pre_survey_question2", ref G_PreMeditationResponse2Value);

        // Get Objects for Question & Response 3
        G_Question3InputField = GameObject.Find("Mood Survey Post-Meditation").transform.Find("Question 3").Find("InputField").gameObject.GetComponent<InputField>();
        G_PreMeditationResponse3InputField = GameObject.Find("Mood Survey Post-Meditation").transform.Find("Pre-Meditation Response 3").Find("InputField").gameObject.GetComponent<InputField>();
        G_GameController.GetComponent<GameController>().GetGameData("pre_survey_question3", ref G_PreMeditationResponse3Value);
    }

    
    void LoadSurveyResponses()
    {
        G_GameController.GetComponent<GameController>().GetGameData("pre_survey_question1", ref G_PreMeditationResponse1Value);
        switch(G_PreMeditationResponse1Value)
        {
            case 1: // Poor
                G_PreMeditationResponse1ValueDisplay.GetComponent<Text>().text = "Poor";
                G_PreMeditationResponse1Slider.value = G_PreMeditationResponse1Value;
                break;
            case 2: // Could be better
                G_PreMeditationResponse1ValueDisplay.GetComponent<Text>().text = "Could be better";
                G_PreMeditationResponse1Slider.value = G_PreMeditationResponse1Value;
                break;
            case 3: // OK
                G_PreMeditationResponse1ValueDisplay.GetComponent<Text>().text = "OK";
                G_PreMeditationResponse1Slider.value = G_PreMeditationResponse1Value;
                break;
            case 4: // Pretty Good
                G_PreMeditationResponse1ValueDisplay.GetComponent<Text>().text = "Pretty Good";
                G_PreMeditationResponse1Slider.value = G_PreMeditationResponse1Value;
                break;
            case 5: // Great!
                G_PreMeditationResponse1ValueDisplay.GetComponent<Text>().text = "Great!";
                G_PreMeditationResponse1Slider.value = G_PreMeditationResponse1Value;
                break;
            default:
                break;
        }

        G_GameController.GetComponent<GameController>().GetGameData("pre_survey_question2", ref G_PreMeditationResponse2Value);
        switch(G_PreMeditationResponse2Value)
        {
            case 1: // Much Worse!
                G_PreMeditationResponse2ValueDisplay.GetComponent<Text>().text = "Much Worse!";
                G_PreMeditationResponse2Slider.value = G_PreMeditationResponse2Value;
                break;
            case 2: // A Little Worse
                G_PreMeditationResponse2ValueDisplay.GetComponent<Text>().text = "A Little Worse";
                G_PreMeditationResponse2Slider.value = G_PreMeditationResponse2Value;
                break;
            case 3: // Pretty Much the Same
                G_PreMeditationResponse2ValueDisplay.GetComponent<Text>().text = "Pretty Much the Same";
                G_PreMeditationResponse2Slider.value = G_PreMeditationResponse2Value;
                break;
            case 4: // A Little Better
                G_PreMeditationResponse2ValueDisplay.GetComponent<Text>().text = "A Little Better";
                G_PreMeditationResponse2Slider.value = G_PreMeditationResponse2Value;
                break;
            case 5: // Much Better!
                G_PreMeditationResponse2ValueDisplay.GetComponent<Text>().text = "Much Better!";
                G_PreMeditationResponse2Slider.value = G_PreMeditationResponse2Value;
                break;
            default:
                break;
        }

        G_GameController.GetComponent<GameController>().GetGameData("pre_survey_question3", ref G_PreMeditationResponse3Value);
        G_PreMeditationResponse3InputField.text = G_PreMeditationResponse3Value;
    }

    public void UpdateHowAreYouFeelingNow(Slider slider)
    {
        int value = (int)slider.value;
        switch(value)
        {
            case 1: // Poor
                G_Question1ValueDisplay.GetComponent<Text>().text = "Much Worse!";
                G_Question1Value = value;
                break;
            case 2: // Could be better
                G_Question1ValueDisplay.GetComponent<Text>().text = "A Little Worse";
                G_Question1Value = value;
                break;
            case 3: // OK
                G_Question1ValueDisplay.GetComponent<Text>().text = "Pretty Much the Same";
                G_Question1Value = value;
                break;
            case 4: // Pretty Good
                G_Question1ValueDisplay.GetComponent<Text>().text = "A Little Better";
                G_Question1Value = value;
                break;
            case 5: // Great!
                G_Question1ValueDisplay.GetComponent<Text>().text = "Much Better!";
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
        G_GameController.GetComponent<GameController>().SetGameData("post_survey_question1", G_Question1Value);
        G_GameController.GetComponent<GameController>().SetGameData("post_survey_question2", G_Question2Value);
        G_GameController.GetComponent<GameController>().SetGameData("post_survey_question3", G_Question3InputField.text);
        G_GameController.GetComponent<GameController>().SaveGameData();

        // gameObject.SetActive(false);
    }
}
