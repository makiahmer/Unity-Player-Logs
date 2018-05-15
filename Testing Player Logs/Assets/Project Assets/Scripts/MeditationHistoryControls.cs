using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class MeditationHistoryControls : MonoBehaviour
{
    public bool G_Debug = true;
    public GameObject G_MoodSurveyHistory = null;
    public GameObject G_SurveyDataTemplatePanel = null;
    public GameObject G_LogDetailsPanel = null;
    public int G_LogCount = 10;     // Number of logs to load
    private string G_DataPath = ""; // For us it was: C:/Users/ME!/AppData/LocalLow/DefaultCompany/project_template/gameData.dat

    // Use this for initialization
    void Start()
    {
        // Prepare to gather files
        G_DataPath = Application.persistentDataPath;
        GetLogs();
    }

    public void GetLogs()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(G_DataPath);
        FileInfo[] files = null;

        // Get Files
        if(dirInfo.Exists)
        {
            files = dirInfo.GetFiles("gameData*.dat");
        }

        /* Capture the n most recent meditation experiences to display
         * The for loop is reversed because it appears that the files are 
         * ordered by name. Thus our naming scheme gameData_YYYYMMDD.dat
         * will obtain them with later dates at the end.
         */
        int loadedLogCount = 0;
        Vector3 panelPosition = new Vector3(150, 0, 0);
        for(int i = files.Length; i > 0;)
        {
            i--;
            LoadLog(files[i], ref panelPosition);
            loadedLogCount++;
            if(loadedLogCount > G_LogCount) { break; }
        }
    }

    private void LoadLog(FileInfo log, ref Vector3 panelPosition)
    {
        GameObject logPanel = null;
        string logTimestamp = "";
        float preOverall = 0f;
        float preStress = 0f;
        string preNotes = "";
        float postOverall = 0f;
        float postStress = 0f;
        string postNotes = "";

        // Open File
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = log.OpenRead();
        GameData data = new GameData();

        // Obtain Data from file
        data = (GameData)bf.Deserialize(fs);
        logTimestamp = data.log_timestamp;
        preOverall = data.pre_survey_question1;
        preStress = data.pre_survey_question2;
        preNotes = data.pre_survey_question3;
        postOverall = data.post_survey_question1;
        postStress = data.post_survey_question2;
        postNotes = data.post_survey_question3;
        fs.Close();

        // Copy the template survey data panel to display the data of this log
        logPanel = Object.Instantiate(G_SurveyDataTemplatePanel);
        logPanel.transform.SetParent(G_SurveyDataTemplatePanel.transform.parent);
        logPanel.transform.localPosition = panelPosition;

        // Load the data into the new panel
        logPanel.transform.Find("Pre-Overall").GetComponent<Slider>().value = preOverall;
        logPanel.transform.Find("Pre-Stress").GetComponent<Slider>().value = preStress;
        logPanel.transform.Find("Pre-Notes").GetComponent<Text>().text = preNotes;
        logPanel.transform.Find("Post-Overall").GetComponent<Slider>().value = postOverall;
        logPanel.transform.Find("Post-Stress").GetComponent<Slider>().value = postStress;
        logPanel.transform.Find("Post-Notes").GetComponent<Text>().text = postNotes;
        logPanel.transform.Find("Timestamp").GetComponent<Text>().text = logTimestamp;
        //logPanel.transform.Find("Open Log").GetComponent<Button>();

        // Update our scroll view width and panel positions
        RectTransform rt = G_SurveyDataTemplatePanel.transform.parent.GetComponent<RectTransform>();
        if(panelPosition.x > rt.rect.width)
        {
            rt.sizeDelta = new Vector2(rt.rect.width + 275, rt.rect.height);
        }
        panelPosition.x += 275; // panel widths of 250 + 25 gap between
        logPanel.SetActive(true);
    }

    public void OpenLog(GameObject logPanel)
    {
        string logTimestamp = "";
        float preOverall = 0f;
        float preStress = 0f;
        string preNotes = "";
        float postOverall = 0f;
        float postStress = 0f;
        string postNotes = "";

        preOverall = logPanel.transform.Find("Pre-Overall").GetComponent<Slider>().value;
        preStress = logPanel.transform.Find("Pre-Stress").GetComponent<Slider>().value;
        preNotes = logPanel.transform.Find("Pre-Notes").GetComponent<Text>().text;
        postOverall = logPanel.transform.Find("Post-Overall").GetComponent<Slider>().value;
        postStress = logPanel.transform.Find("Post-Stress").GetComponent<Slider>().value;
        postNotes = logPanel.transform.Find("Post-Notes").GetComponent<Text>().text;
        logTimestamp = logPanel.transform.Find("Timestamp").GetComponent<Text>().text;

        G_LogDetailsPanel.transform.Find("Title").GetComponent<Text>().text = "Log Details for " + logTimestamp;
        switch((int)preOverall)
        {
            case 1: // Poor
                G_LogDetailsPanel.transform.Find("response1").GetComponent<InputField>().text = "Poor";
                break;
            case 2: // Could be better
                G_LogDetailsPanel.transform.Find("response1").GetComponent<InputField>().text = "Could be better";
                break;
            case 3: // OK
                G_LogDetailsPanel.transform.Find("response1").GetComponent<InputField>().text = "OK";
                break;
            case 4: // Pretty Good
                G_LogDetailsPanel.transform.Find("response1").GetComponent<InputField>().text = "Pretty Good";
                break;
            case 5: // Great!
                G_LogDetailsPanel.transform.Find("response1").GetComponent<InputField>().text = "Great!";
                break;
            default:
                G_LogDetailsPanel.transform.Find("response1").GetComponent<InputField>().text = "(No review provided)";
                break;
        }

        switch((int)preStress)
        {
            case 0: // Not At All
                G_LogDetailsPanel.transform.Find("response2").GetComponent<InputField>().text = "Not At All";
                break;
            case 1: // Normal Stuff
                G_LogDetailsPanel.transform.Find("response2").GetComponent<InputField>().text = "Normal Stuff";
                break;
            case 2: // Kinda Stressed
                G_LogDetailsPanel.transform.Find("response2").GetComponent<InputField>().text = "Kinda Stressed";
                break;
            case 3: // Feeling Pressured
                G_LogDetailsPanel.transform.Find("response2").GetComponent<InputField>().text = "Feeling Pressured";
                break;
            case 4: // Crumbling
                G_LogDetailsPanel.transform.Find("response2").GetComponent<InputField>().text = "Crumbling";
                break;
            case 5: // Extremely Stressed
                G_LogDetailsPanel.transform.Find("response2").GetComponent<InputField>().text = "Extremely Stressed";
                break;
            default:
                G_LogDetailsPanel.transform.Find("response1").GetComponent<InputField>().text = "(No review provided)";
                break;
        }

        G_LogDetailsPanel.transform.Find("response3").GetComponent<InputField>().text = (preNotes == "" ? "(No notes provided.)" : preNotes);

        switch((int)postOverall)
        {
            case 1: // Poor
                G_LogDetailsPanel.transform.Find("response4").GetComponent<InputField>().text = "Poor";
                break;
            case 2: // Could be better
                G_LogDetailsPanel.transform.Find("response4").GetComponent<InputField>().text = "Could be better";
                break;
            case 3: // OK
                G_LogDetailsPanel.transform.Find("response4").GetComponent<InputField>().text = "OK";
                break;
            case 4: // Pretty Good
                G_LogDetailsPanel.transform.Find("response4").GetComponent<InputField>().text = "Pretty Good";
                break;
            case 5: // Great!
                G_LogDetailsPanel.transform.Find("response4").GetComponent<InputField>().text = "Great!";
                break;
            default:
                G_LogDetailsPanel.transform.Find("response4").GetComponent<InputField>().text = "(No review provided)";
                break;
        }

        switch((int)postStress)
        {
            case 1: // Much Worse!
                G_LogDetailsPanel.transform.Find("response5").GetComponent<InputField>().text = "Much Worse!";
                break;
            case 2: // A Little Worse
                G_LogDetailsPanel.transform.Find("response5").GetComponent<InputField>().text = "A Little Worse";
                break;
            case 3: // Pretty Much the Same
                G_LogDetailsPanel.transform.Find("response5").GetComponent<InputField>().text = "Pretty Much the Same";
                break;
            case 4: // A Little Better
                G_LogDetailsPanel.transform.Find("response5").GetComponent<InputField>().text = "A Little Better";
                break;
            case 5: // Much Better!
                G_LogDetailsPanel.transform.Find("response5").GetComponent<InputField>().text = "Much Better!";
                break;
            default:
                G_LogDetailsPanel.transform.Find("response5").GetComponent<InputField>().text = "(No review provided)";
                break;
        }

        G_LogDetailsPanel.transform.Find("response6").GetComponent<InputField>().text = (postNotes == "" ? "(No notes provided.)" : postNotes);

        G_LogDetailsPanel.SetActive(true);
    }
}
