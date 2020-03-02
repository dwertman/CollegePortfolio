using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    public GameObject pausemenu;
    //public GameObject playerCanvas;
    public bool cursorVisible = false;

	
	public void resume() {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = cursorVisible;
            Time.timeScale = 1f;
            pausemenu.SetActive(false);
            //playerCanvas.SetActive(true);
        }*/
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = cursorVisible;
            Time.timeScale = 1f;
            pausemenu.SetActive(false);
    }
}
