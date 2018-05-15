using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceLoader : MonoBehaviour
{
    /* VARIABLE DECLARATIONS =================================================*/
    // Public Variables
    // Private Variables
    /*====  /VARIABLE DECLARATIONS ====*/


    /* UNITY SPECIFIC FUNCTIONS ==============================================*/
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*====  /UNITY SPECIFIC FUNCTIONS ====*/


    /* MY FUNCTIONS ==========================================================*/
    /*--------------------------------------------------------------------------
     * Description      : asdf
     */
    public void ExampleFunction()
    {
    } /*-- /ExampleFunction() declaration--*/


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
    } /*-- /ExitApplication() declaration--*/
    /*====  /MY FUNCTIONS ====*/
} /*====  /public class PreferenceLoader : MonoBehaviour ====*/