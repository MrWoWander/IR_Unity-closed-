using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public void pizda () {
		SceneManager.LoadScene (1);
	}
	public void malafia () {
		Application.Quit();
	}
	public void back () {
		SceneManager.LoadScene (0);
	}
}