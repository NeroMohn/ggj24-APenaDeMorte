using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class SceneUIControl : MonoBehaviour
{
	private int l;
	public GameObject langButton;
	public Sprite br;
	public Sprite us;

	public void Start()
	{
		l = PlayerPrefs.GetInt("language", 0);
		langButton.GetComponent<Image>().sprite = l > 0 ? us : br;
	}

	public void PvP()
	{
        FMODMenuMusic.ExitMainMenu();
        SceneManager.LoadScene("GambiarraPvP");
	}

	public void PvE()
	{
		FMODMenuMusic.ExitMainMenu();
        SceneManager.LoadScene("GambiarraPvE");
	}

	public void Rematch()
	{
		SceneManager.LoadScene(GameObject.Find("Gambiarra").transform.position.x > 0 ? "GambiarraPvE" : "GambiarraPvP");
	}

	public void Quit()
	{
		SceneManager.LoadScene("TitleScene");
	}

	public void exitGame()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
		Application.Quit();
	}

	public void toggleLanguage()
	{
		l = l > 0 ? 0 : 1;
		PlayerPrefs.SetInt("language", l);
		PlayerPrefs.Save();
		GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
		langButton.GetComponent<Image>().sprite = l > 0 ? us : br;
	}
}
