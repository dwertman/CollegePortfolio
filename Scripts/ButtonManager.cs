using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public UIManager UIManager;

    // Start is called before the first frame update
    public void saveGame()
    {
        Debug.Log("Game Saved!");
    }

    public void quitGame()
    {
        Debug.Log("Game Saved!");
        Application.Quit();
    }

    public void returnToGame()
    {
        UIManager.closeUI();
    }

    //public void addScore()
    //{
    //    UIManager.fishName.text = UIManager.fish.;
    //}
}
