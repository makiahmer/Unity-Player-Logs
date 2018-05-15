/*
 * This script was based off of a tutorial.
 * Ref[1]: https://unity3d.com/learn/tutorials/topics/scripting/persistence-saving-and-loading-data
 * 
 * This script must be attached to a root GameObject.
 * Ref[2]: http://answers.unity3d.com/answers/456306/view.html
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    /* VARIABLE DECLARATIONS =================================================*/
    // Public Variables
    public bool G_Debug = true;
    public static GameController G_GameController;
    public float G_Health;
    public float G_XP;
    private GameData G_GameData = new GameData();

    // Private Variables
    private int G_CurrentScene = 0;
    private string G_DataPath = ""; // For us it was: C:/Users/ME!/AppData/LocalLow/DefaultCompany/project_template/gameData.dat
    /*====  /VARIABLE DECLARATIONS ====*/


    /* UNITY SPECIFIC FUNCTIONS ==============================================*/
    private void Awake()
    {
        G_DataPath = Application.persistentDataPath + "/gameData_" + System.DateTime.Now.ToString("yyyyMMdd") + ".dat";
        G_CurrentScene = SceneManager.GetActiveScene().buildIndex;

        if (G_GameController == null)
        {   // If there is no game controller create one.
            DontDestroyOnLoad(gameObject);
            G_GameController = this;
        }
        else if (G_GameController != this)
        {   // If this isn't the the current game controller destroy it, we will
            // use 'this' one.
            Destroy(gameObject);
        }

        if (G_Debug) { print(G_DataPath); }
    } /*-- /Awake() declaration --*/

    private void Update()
    {
        /*
         * U - Player XP++
         * J - Player XP--
         * I - SaveGameData()
         * K - LoadGameData()
         * O - Change Scenes
         */
        if (Input.GetKeyDown(KeyCode.U))
        {
            ++G_XP;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            --G_XP;
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            SaveGameData();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            LoadGameData();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            switch (G_CurrentScene)
            {
                case 0:
                    SceneManager.LoadSceneAsync(1);
                    break;
                case 1:
                    SceneManager.LoadSceneAsync(0);
                    break;
                default:
                    break;
            }
        }
    } /*-- /Update() declaration --*/

    private void OnGUI ()
    {
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 80, 200
                , 30), "Current Scene: " + SceneManager.GetActiveScene().name);
        GUI.Label(new Rect(10, 10, 100, 30), "Health: " + G_Health);
        GUI.Label(new Rect(10, 40, 100, 30), "XP: " + G_XP);
    } /*-- /OnGUI() declaration --*/
    /*====  /UNITY SPECIFIC FUNCTIONS ====*/


    /* MY FUNCTIONS ==========================================================*/
    /*--------------------------------------------------------------------------
     * Description      : Saves desired data to a binary file for later use.
     */
    public void SaveGameData()
    {
        // Grab/create objects to handle data transfer
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(G_DataPath, FileMode.OpenOrCreate);
        //GameData data = new GameData();

        //// Capture data to write
        //data.health = G_Health;
        //data.xp = G_XP;

        GameData data = G_GameData;
        data.log_timestamp = System.DateTime.Now.ToString("g");

        // Write data to our file
        bf.Serialize(file, data);
        file.Close();

        print("File Saved: " + System.DateTime.Now.ToString("yyyyMMdd.HHmm"));
    } /*-- /SaveGameData() declaration --*/


    /*--------------------------------------------------------------------------
     * Description      : Loads game data from a previously binary file.
     */
    public void LoadGameData()
    {
        if(!File.Exists(G_DataPath)) { return; }

        // Grab/create objects to handle data transfer
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(G_DataPath, FileMode.Open);
        GameData data = new GameData();

        // Obtain data from the file
        data = (GameData)bf.Deserialize(file);

        G_GameData = data;

        // Store data for later use
        G_Health = data.health;
        G_XP = data.xp;
        file.Close();

        print("File Loaded: " + System.DateTime.Now.ToString("yyyyMMdd.HHmm"));
    } /*-- /LoadGameData() declaration --*/


    /*--------------------------------------------------------------------------
     * Description      : Sets a parameter of the GameData class.
     * Parameters       : @member   - the member to set.
     *                  : @value    - the value to set the member to.
     */
    public void SetGameData(string member, int value)
    {
        switch(member)
        {
            case "pre_survey_question1":
                G_GameData.pre_survey_question1 = (float)value;
                break;
            case "pre_survey_question2":
                G_GameData.pre_survey_question2 = (float)value;
                break;
            case "post_survey_question1":
                G_GameData.post_survey_question1 = (float)value;
                break;
            case "post_survey_question2":
                G_GameData.post_survey_question2 = (float)value;
                break;
            default:
                break;
        }
    } /*-- /SetGameData(string, float) declaration --*/

    public void SetGameData(string member, float value)
    {
        switch(member)
        {
            case "pre_survey_question1":
                G_GameData.pre_survey_question1 = value;
                break;
            case "pre_survey_question2":
                G_GameData.pre_survey_question2 = value;
                break;
            case "post_survey_question1":
                G_GameData.post_survey_question1 = value;
                break;
            case "post_survey_question2":
                G_GameData.post_survey_question2 = value;
                break;
            default:
                break;
        }
    } /*-- /SetGameData(string, float) declaration --*/

    public void SetGameData(string member, string value)
    {
        switch(member)
        {
            case "pre_survey_question3":
                G_GameData.pre_survey_question3 = value;
                break;
            case "post_survey_question3":
                G_GameData.post_survey_question3 = value;
                break;
            default:
                break;
        }
    } /*-- /SetGameData(string, string) declaration --*/


    /*--------------------------------------------------------------------------
     * Description      : Sets a parameter of the GameData class.
     * Parameters       : @member     - the member to get.
     *                  : @return_val - a reference to the container storing 
     *                  :               the value of the member.
     */
    public void GetGameData(string member, ref int return_val)
    {
        switch(member)
        {
            case "pre_survey_question1":
                return_val = (int)G_GameData.pre_survey_question1;
                break;
            case "pre_survey_question2":
                return_val = (int)G_GameData.pre_survey_question2;
                break;
            case "post_survey_question1":
                return_val = (int)G_GameData.post_survey_question1;
                break;
            case "post_survey_question2":
                return_val = (int)G_GameData.post_survey_question2;
                break;
            default:
                return_val = (int)-1f;
                break;
        }
    } /*-- /SetGameData(string, int) declaration --*/

    public void GetGameData(string member, ref float return_val)
    {
        switch(member)
        {
            case "pre_survey_question1":
                return_val = G_GameData.pre_survey_question1;
                break;
            case "pre_survey_question2":
                return_val = G_GameData.pre_survey_question2;
                break;
            case "post_survey_question1":
                return_val = G_GameData.post_survey_question1;
                break;
            case "post_survey_question2":
                return_val = G_GameData.post_survey_question2;
                break;
            default:
                return_val = -1f;
                break;
        }
    } /*-- /SetGameData(string, float*) declaration --*/

    public void GetGameData(string member, ref string return_val)
    {
        switch(member)
        {
            case "pre_survey_question3":
                return_val = G_GameData.pre_survey_question3;
                break;
            case "post_survey_question3":
                return_val = G_GameData.post_survey_question3;
                break;
            default:
                return_val = "";
                break;
        }
    } /*-- /SetGameData(string, string) declaration --*/


    /*--------------------------------------------------------------------------
     * Description      : Exits the application.
     */
    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    } /*-- /ExitApplication() declaration --*/
    /*====  /MY FUNCTIONS ====*/
} /*====  /public class GameController : MonoBehaviour ====*/


/*
 * This class is used to write the desired data to a file.
 */
[Serializable] class GameData
{
    public float health;
    public float xp;

    public string log_timestamp;

    public float pre_survey_question1;
    public float pre_survey_question2;
    public string pre_survey_question3;

    public float post_survey_question1;
    public float post_survey_question2;
    public string post_survey_question3;
} /*====  /[Serializable] class GameData ====*/
