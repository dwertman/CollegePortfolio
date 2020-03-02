using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour {

    private SceneFader fader;

    void Awake()
    {
        fader = FindObjectOfType<SceneFader>();
    }

    // Loads next scene (should be first level) with a fade effect
    void OnTriggerEnter(Collider other)
    {
        fader.FadeToScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
