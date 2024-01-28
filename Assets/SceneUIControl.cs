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

	public GameObject text01;
	public GameObject text02;
	public GameObject pvp;
	public GameObject pve;
	public GameObject ext;
	public GameObject rem;
	public GameObject giveUp;

	public void Start()
	{
/*
		text01 = GameObject.Find("Text 01");
		text02 = GameObject.Find("Text 02");
		pvp = GameObject.Find("PvP");
		pve = GameObject.Find("PvE");
		ext = GameObject.Find("Exit");
		rem = GameObject.Find("Continue");
		giveUp = GameObject.Find("Quit");
*/
		l = PlayerPrefs.GetInt("language", 0);
		langButton.GetComponent<Image>().sprite = l > 0 ? us : br;
		updateLanguage();
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
		updateLanguage();
	}

	private void updateLanguage()
	{
		if (text01)
		{
			if (l > 0)
				text01.GetComponent<TMPro.TextMeshProUGUI>().text = "A long time ago in a distant land, people were prohibited from smiling and laughing. Laughing would result in immediate execution by the hands of the law. However, a few rogue individuals still dueled with their Feather Swords, an ingenious implement to make thy opponent laugh. This illegal form of duel was known as The Last Laugh, for it would most certainly end poorly if one could not hold their laughter.";
			else
				text01.GetComponent<TMPro.TextMeshProUGUI>().text = "Há muito tempo numa terra distante, foi criminalizado o riso. Rir significava execução imediata pelas mãos da justiça. Entretanto, alguns indivíduos ainda engajavam em duelos com suas Espadas Pena, um implemento engenhoso que faz seu oponente gargalhar. Essa forma ilegal de duelo era conhecida como a Pena de Morte, e o combatente incapaz de segurar o riso certamente estaria em maus lençóis.";
		}
		if (text02)
		{
			if (l > 0)
				text02.GetComponent<TMPro.TextMeshProUGUI>().text = "The game plays like rock, paper, scissors. You can attack, defend or feint. Defense defeats attack, attack defeats feint, and feint defeats defense. However, the rounds are in sets of three moves, and you can't use the same movement twice in a set! This adds a little bit more complexity to the rock, paper, scissors idea. Navigate through the menu with the mouse cursor, and play with the 1, 2 and 3 number keys from your keyboard (not numpad). If there are two players, the second player uses numpad keys.";
			else
				text02.GetComponent<TMPro.TextMeshProUGUI>().text = "O jogo é como pedra, papel, tesoura. Você pode atacar, defender, ou fintar. Defesa ganha de ataque, que ganha de finta, que ganha de defesa. Entretanto, cada rodada é composta de três movimentos, e você não pode repetir o movimento na rodada! Isso adiciona um pouco de complexidade à ideia de pedra, papel, tesoura. Navegue pelo menu com o cursor do mouse, e jogue com os botões 1, 2 e 3 do teclado (o teclado padrão, não as teclas numéricas). Se houver dois jogadores, o segundo jogador usa as teclas 1, 2 e 3 do teclado numérico.";
		}
		if (pvp)
		{
			if (l > 0)
				pvp.GetComponent<TMPro.TextMeshProUGUI>().text = "Player vs player";
			else
				pvp.GetComponent<TMPro.TextMeshProUGUI>().text = "Jogador vs jogador";
		}
		if (pve)
		{
			if (l > 0)
				pve.GetComponent<TMPro.TextMeshProUGUI>().text = "Player vs machine";
			else
				pve.GetComponent<TMPro.TextMeshProUGUI>().text = "Jogador vs máquina";
		}
		if (ext)
		{
			if (l > 0)
				ext.GetComponent<TMPro.TextMeshProUGUI>().text = "Exit game";
			else
				ext.GetComponent<TMPro.TextMeshProUGUI>().text = "Sair do jogo";
		}
		if (rem)
		{
			if (l > 0)
				rem.GetComponent<TMPro.TextMeshProUGUI>().text = "Rematch";
			else
				rem.GetComponent<TMPro.TextMeshProUGUI>().text = "Revanche";
		}
		if (giveUp)
		{
			if (l > 0)
				giveUp.GetComponent<TMPro.TextMeshProUGUI>().text = "Return";
			else
				giveUp.GetComponent<TMPro.TextMeshProUGUI>().text = "Voltar";
		}
	}
}
