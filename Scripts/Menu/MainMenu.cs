using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenuCanvas;
    public GameObject helpMenuCanvas;

    private SceneFader fader;

    private void Awake()
    {
        fader = FindObjectOfType<SceneFader>();
    }

    // Loads next scene (should be first level) with a fade effect
    public void Play()
    {
        fader.FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Switches between MainMenu and HelpMenu
    public void SwitchMenu_MainHelp()
    {
        if (mainMenuCanvas.GetComponent<GraphicRaycaster>().enabled)
            fader.CrossFadeObjects(mainMenuCanvas, helpMenuCanvas);
        else if (helpMenuCanvas.GetComponent<GraphicRaycaster>().enabled)
            fader.CrossFadeObjects(helpMenuCanvas, mainMenuCanvas);

        //mainMenuCanvas.SetActive(!mainMenuCanvas.activeSelf);
        //helpMenuCanvas.SetActive(!helpMenuCanvas.activeSelf);
    }

    // Quits game
    public void Exit()
    {
        Debug.Log("Quit"); // For in editor only

        Application.Quit(); // Closes application
    }
}
