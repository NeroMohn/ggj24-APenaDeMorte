using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class temp : MonoBehaviour
{
	private bool pve;

	void Start()
	{
		pve = GameObject.Find("Gambiarra").transform.position.x > 0;
		if (pve)
			Debug.Log("Fight CPU");
		else
			Debug.Log("Fight player");
		SceneManager.LoadScene("Rematch", LoadSceneMode.Additive);
	}
}
