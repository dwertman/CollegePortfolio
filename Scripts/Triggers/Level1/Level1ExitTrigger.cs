using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1ExitTrigger : MonoBehaviour
{

    private SceneFader fader;

    private void Awake()
    {
        fader = FindObjectOfType<SceneFader>();
    }

    // Loads next scene (should be third level) with a fade effect
    void OnTriggerEnter(Collider collider)
    {
        fader.FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
