using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattle : MonoBehaviour
{
	void Start()
	{
		SceneManager.LoadScene("temp", LoadSceneMode.Additive);
	}
}
