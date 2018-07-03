using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGameScript : MonoBehaviour {

	public void Menu() {
		Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() {
		Application.Quit();
	}

	public void Restart() {
		Time.timeScale = 1;
        SceneManager.LoadScene("Main Level");
    }
}