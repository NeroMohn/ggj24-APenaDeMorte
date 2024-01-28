using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ImageClick : MonoBehaviour, IPointerClickHandler
{
	private int state;
	private float timer;
	private GameObject text01;
	private GameObject img01;
	private GameObject text02;
	private GameObject img02;
	private Color textColor01;
	private Color textColor02;
	private Color imgColor01;
	private Color imgColor02;

	public void Start()
	{
		state = 0;
		timer = 0.0f;
		text01 = GameObject.Find("Text 01");
		img01 = GameObject.Find("Image 01");
		text02 = GameObject.Find("Text 02");
		img02 = GameObject.Find("Image 02");
		textColor01 = new Color(0, 0, 0, 0);
		textColor02 = new Color(0, 0, 0, 1);
		imgColor01 = new Color(1, 1, 1, 0);
		imgColor02 = new Color(1, 1, 1, 1);
	}

	public void Update()
	{
		timer += Time.deltaTime;
		if (state == 0)
		{
			text01.GetComponent<TMPro.TextMeshProUGUI>().color = Color.Lerp(textColor01, textColor02, timer / 1.5f);
			img01.GetComponent<Image>().color = Color.Lerp(imgColor01, imgColor02, timer / 1.5f);
			text02.GetComponent<TMPro.TextMeshProUGUI>().color = textColor01;
			img02.GetComponent<Image>().color = imgColor01;
			text01.transform.localPosition = new Vector3(-300.0f + timer * 100.0f / 1.5f, 0, 0);
			img01.transform.localPosition = new Vector3(400.0f - timer * 100.0f / 1.5f, -120, 0);
			if (timer > 1.5f)
			{
				state++;
				timer = 0.0f;
			}
		}
		else if (state == 2)
		{
			text01.GetComponent<TMPro.TextMeshProUGUI>().color = textColor01;
			img01.GetComponent<Image>().color = imgColor01;
			text02.GetComponent<TMPro.TextMeshProUGUI>().color = Color.Lerp(textColor01, textColor02, timer / 1.5f);
			img02.GetComponent<Image>().color = Color.Lerp(imgColor01, imgColor02, timer / 1.5f);
			text02.transform.localPosition = new Vector3(200, 172.0f - timer * 100.0f / 1.5f, 0);
			img02.transform.localPosition = new Vector3(-300, -100.0f + timer * 100.0f / 1.5f, 0);
			if (timer > 1.5f)
			{
				state++;
				timer = 0.0f;
			}
		}
		else if (state == 1 || state == 3)
		{
			if (state == 1)
			{
				text01.GetComponent<TMPro.TextMeshProUGUI>().color = textColor02;
				img01.GetComponent<Image>().color = imgColor02;
				text02.GetComponent<TMPro.TextMeshProUGUI>().color = textColor01;
				img02.GetComponent<Image>().color = imgColor01;
				text01.transform.localPosition = new Vector3(-200, 0, 0);
				img01.transform.localPosition = new Vector3(300, -120, 0);
			}
			else
			{
				text01.GetComponent<TMPro.TextMeshProUGUI>().color = textColor01;
				img01.GetComponent<Image>().color = imgColor01;
				text02.GetComponent<TMPro.TextMeshProUGUI>().color = textColor02;
				img02.GetComponent<Image>().color = imgColor02;
				text02.transform.localPosition = new Vector3(200, 72, 0);
				img02.transform.localPosition = new Vector3(-300, 0, 0);
			}
			if (timer > 6.0f)
			{
				state++;
				timer = 0.0f;
			}
		}
		else
			SceneManager.LoadScene("TitleScene");
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
		timer = 0.0f;
		if (++state > 3)
			SceneManager.LoadScene("TitleScene");
	}
}
