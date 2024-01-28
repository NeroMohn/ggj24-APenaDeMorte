using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	public GameObject characters;
	public Animator charactersAnimator;
	public string animationToBePlayed = "Idle";
	public bool multiplayer;
	public string[] player1Actions = new string[3];
	public string[] player2Actions = new string[3];
	public Dictionary<int, string> actionsDictionary = new Dictionary<int, string>();
	private int firstEnemyAction;
	private int secondEnemyAction;
	private int thirdEnemyAction;
	public int playerActionsIndexForChoosing = 0;
	public bool[] player1ActionsChosen = new bool[3];
	public bool[] player2ActionsChosen = new bool[3];
	public string[] prohibitedActionsPlayer1 = new string[3];
	public string[] prohibitedActionsPlayer2 = new string[3];
	public bool allActionsPerformed = false;
	public string roundWinner; //"PLAYER", "ENEMY" ou "DRAW"
	public bool roundIsPlaying = false;
	public bool[] roundsPlayed = new bool[3];
	[SerializeField]private int player2HP = 5;
	[SerializeField]private int player1HP = 5;
	public float tickleTimer = 1.5f;
	private float endingTimer = 0.5f;
	public bool tickleSoundIsPlaying = false;
	public bool laughIsPlaying = false;

    private FMODSFX _sfxHandler = new FMODSFX();

    // Start is called before the first frame update
    void Start()
	{
		if (!GameObject.Find("Gambiarra"))
			multiplayer = true;
		else
			multiplayer = GameObject.Find("Gambiarra").transform.position.x > 0;
		characters = GameObject.Find("Characters");
		charactersAnimator = characters.GetComponent<Animator>();
		if(!multiplayer){
			Debug.Log("PARTIDA SINGLE PLAYER");
			actionsDictionary.Add(0, "ATTACK");
			actionsDictionary.Add(1, "DEFEND");
			actionsDictionary.Add(2, "FEINT");
			EnemyActionRandomizer();
		}
		else{
			Debug.Log("PARTIDA MULTIPLAYER");
		}
		
	}

	// Update is called once per frame
	void Update()
	{
		if(!roundIsPlaying && !multiplayer){
			GameLoopSinglePlayer();
		}
		else if(!roundIsPlaying && multiplayer){
			GameLoopMultiplayer();
		}

		if(allActionsPerformed){
			ClearStats();
		}

		if(roundIsPlaying){
			PlayAnim();
		}

	}

	void GameLoopSinglePlayer(){
		if(player1HP <=0){
			if(endingTimer <= 0 && !laughIsPlaying){
				_sfxHandler.PlayFemaleLaugh();
				laughIsPlaying = true;
			}
			endingTimer -= Time.deltaTime;
			return;
		}
		else if(player2HP <=0 && !laughIsPlaying){
			if(endingTimer <= 0){
				_sfxHandler.PlayMaleLaught();
				laughIsPlaying = true;
			}
			endingTimer -= Time.deltaTime;
		}
		if(playerActionsIndexForChoosing == 0 && !roundIsPlaying){
			//Debug.Log("INICIANDO ROUND 1");
			if(!player1ActionsChosen[0]){
				if(Player1ChooseActions()){
					_sfxHandler.PlayPlayerChoice();
				player1ActionsChosen[0] = true;
				}
			}
			
			
		}
		if(!roundsPlayed[0]  && player1ActionsChosen[0] && playerActionsIndexForChoosing == 0){
			prohibitedActionsPlayer1[0] = player1Actions[0];
			playerActionsIndexForChoosing ++;
			CheckWhoWins(0);
			roundsPlayed[0] = true;
			roundIsPlaying = true;
			
			
		}
		if(playerActionsIndexForChoosing == 1 && !roundIsPlaying){
			//Debug.Log("INICIANDO ROUND 2");
			if(!player1ActionsChosen[1]){
				if(Player1ChooseActions()){
					_sfxHandler.PlayPlayerChoice();
				player1ActionsChosen[1] = true;
				}
			}
			
		}
		if(!roundsPlayed[1] && player1ActionsChosen[1] && playerActionsIndexForChoosing == 1){
			prohibitedActionsPlayer1[1] = player1Actions[1];
			playerActionsIndexForChoosing ++;
			CheckWhoWins(1);
			roundsPlayed[1] = true;
			roundIsPlaying = true;
			
			
		}
		if(playerActionsIndexForChoosing == 2 && !roundIsPlaying){
			//Debug.Log("INICIANDO ROUND 3");
			if(!player1ActionsChosen[2]){
				if(Player1ChooseActions()){
					_sfxHandler.PlayPlayerChoice();
				player1ActionsChosen[2] = true;
				}
			}
			
		}
		if(!roundsPlayed[2]  && player1ActionsChosen[2]){
			prohibitedActionsPlayer1[2] = player1Actions[2];
			playerActionsIndexForChoosing ++;
			CheckWhoWins(2);
			roundsPlayed[2] = true;
			roundIsPlaying = true;
			
			allActionsPerformed = true;
		}
		if(allActionsPerformed){
			for( int i =0; i<3; i++){
				player1ActionsChosen[i] =  false;
				player1Actions[i] = "";
				player2Actions[i] = "";
				prohibitedActionsPlayer1[i] = "";
				roundsPlayed[i] = false;
			}
			firstEnemyAction =-1;
			secondEnemyAction =-1;
			thirdEnemyAction =-1;
			EnemyActionRandomizer();
			playerActionsIndexForChoosing = 0;
			allActionsPerformed = false;
		}
	}

	void GameLoopMultiplayer(){
		/*if (player2HP <= 0 || player1HP <= 0){
			
			return;
		}*/
		if(player1HP <=0){
			if(endingTimer <= 0 && !laughIsPlaying){
				_sfxHandler.PlayFemaleLaugh();
				laughIsPlaying = true;
			}
			endingTimer -= Time.deltaTime;
			return;
		}
		else if(player2HP <=0 && !laughIsPlaying){
			if(endingTimer <= 0){
				_sfxHandler.PlayMaleLaught();
				laughIsPlaying = true;
			}
			endingTimer -= Time.deltaTime;
		}
		if(playerActionsIndexForChoosing == 0 && !roundIsPlaying){
			//Debug.Log("INICIANDO ROUND 1");
			if(!player1ActionsChosen[0]){
				if(Player1ChooseActions()){
					_sfxHandler.PlayPlayerChoice();
				player1ActionsChosen[0] = true;
				}
			}

			if(!player2ActionsChosen[0]){
				if(Player2ChooseActions()){
					_sfxHandler.PlayPlayerChoice();
				player2ActionsChosen[0] = true;
				}
			}
			
			
		}
		if(!roundsPlayed[0]  && player1ActionsChosen[0] && player2ActionsChosen[0] && playerActionsIndexForChoosing == 0){
			prohibitedActionsPlayer1[0] = player1Actions[0];
			prohibitedActionsPlayer2[0] = player2Actions[0];
			playerActionsIndexForChoosing ++;
			CheckWhoWins(0);
			roundsPlayed[0] = true;
			roundIsPlaying = true;
			
			
		}
		if(playerActionsIndexForChoosing == 1 && !roundIsPlaying){
			//Debug.Log("INICIANDO ROUND 2");
			if(!player1ActionsChosen[1]){
				if(Player1ChooseActions()){
					_sfxHandler.PlayPlayerChoice();
				player1ActionsChosen[1] = true;
				}
			}
			
			if(!player2ActionsChosen[1]){
				if(Player2ChooseActions()){
					_sfxHandler.PlayPlayerChoice();
				player2ActionsChosen[1] = true;
				}
			}
			
		}
		if(!roundsPlayed[1]  && player1ActionsChosen[1] && player2ActionsChosen[1] && playerActionsIndexForChoosing == 1){
			prohibitedActionsPlayer1[1] = player1Actions[1];
			prohibitedActionsPlayer2[1] = player2Actions[1];
			playerActionsIndexForChoosing ++;
			CheckWhoWins(1);
			roundsPlayed[1] = true;
			roundIsPlaying = true;
			
			
		}
		if(playerActionsIndexForChoosing == 2 && !roundIsPlaying){
			//Debug.Log("INICIANDO ROUND 3");
			if(!player1ActionsChosen[2]){
				if(Player1ChooseActions()){
					_sfxHandler.PlayPlayerChoice();
				player1ActionsChosen[2] = true;
				}
			}
			
			if(!player2ActionsChosen[2]){
				if(Player2ChooseActions()){
					_sfxHandler.PlayPlayerChoice();
				player2ActionsChosen[2] = true;
				}
			}
			
		}
		if(!roundsPlayed[2]  && player1ActionsChosen[2] && player2ActionsChosen[2]){
			prohibitedActionsPlayer1[2] = player1Actions[2];
			playerActionsIndexForChoosing ++;
			CheckWhoWins(2);
			roundsPlayed[2] = true;
			roundIsPlaying = true;
			
			allActionsPerformed = true;
		}

	}

	void ClearStats(){
		for( int i =0; i<3; i++){
			player1ActionsChosen[i] =  false;
			player2ActionsChosen[i] = false;
			player1Actions[i] = "";
			player2Actions[i] = "";
			prohibitedActionsPlayer1[i] = "";
			prohibitedActionsPlayer2[i] = "";
			roundsPlayed[i] = false;
			}
		if(!multiplayer){
			firstEnemyAction =-1;
			secondEnemyAction =-1;
			thirdEnemyAction =-1;
			EnemyActionRandomizer();
		} 
		
		playerActionsIndexForChoosing = 0;
		allActionsPerformed = false;
	}

	void EnemyActionRandomizer(){
		Debug.Log("SORTEANDO AÇÕES DO COMPUTADOR");
		int randomEnemyActionValue;
		randomEnemyActionValue = Random.Range(0,3);
		firstEnemyAction = randomEnemyActionValue;

		while(randomEnemyActionValue == firstEnemyAction){
			randomEnemyActionValue = Random.Range(0,3);
			secondEnemyAction = randomEnemyActionValue;
		}

		thirdEnemyAction = 0;
		while(thirdEnemyAction == firstEnemyAction || thirdEnemyAction == secondEnemyAction){
			thirdEnemyAction ++;
		}

		player2Actions[0] = actionsDictionary[firstEnemyAction];
		player2Actions[1] = actionsDictionary[secondEnemyAction];
		player2Actions[2] = actionsDictionary[thirdEnemyAction];

		for(int i = 0; i<3; i++){
			Debug.Log("AÇÃO"+ i+ "DO COMPUTADOR:" + player2Actions[i]);
		}
	}

	bool Player1ChooseActions(){
		if(Input.GetKeyDown("1")){
			for(int i =0; i<3; i++){
					if(prohibitedActionsPlayer1[i] == "ATTACK"){
						Debug.Log("AÇÃO JÁ USADA PREVIAMENTE PELO PLAYER 1");
						return false;
					}
			}
			player1Actions[playerActionsIndexForChoosing] = "ATTACK";
			//prohibitedActions[playerActionsIndexForChoosing] = "ATTACK";
			Debug.Log("AÇÃO ESCOLHIDA PELO PLAYER 1: ATAQUE");
			//playerActionsIndexForChoosing ++;
			return true;
		}
		else if(Input.GetKeyDown("2")){
			for(int i =0; i<3; i++){
					if(prohibitedActionsPlayer1[i] == "DEFEND"){
						Debug.Log("AÇÃO JÁ USADA PREVIAMENTE PELO PLAYER 1");
						return false;
					}
			}
			player1Actions[playerActionsIndexForChoosing] = "DEFEND";
			//prohibitedActions[playerActionsIndexForChoosing] = "DEFEND";
			Debug.Log("AÇÃO ESCOLHIDA PELO PLAYER 1: DEFESA");
			//playerActionsIndexForChoosing ++;
			return  true;
		}
		else if(Input.GetKeyDown("3")){
			for(int i =0; i<3; i++){
					if(prohibitedActionsPlayer1[i] == "FEINT"){
						Debug.Log("AÇÃO JÁ USADA PREVIAMENTE PELO PLAYER 1");
						return false;
					}
			}
			player1Actions[playerActionsIndexForChoosing] = "FEINT";
			//prohibitedActions[playerActionsIndexForChoosing] = "FEINT";
			Debug.Log("AÇÃO ESCOLHIDA PELO PLAYER 1: FINTA");
			//playerActionsIndexForChoosing ++;
			return true;
		}
		return false;
	}

	bool Player2ChooseActions(){
		if(Input.GetKeyDown(KeyCode.Keypad1)){
			for(int i =0; i<3; i++){
					if(prohibitedActionsPlayer2[i] == "ATTACK"){
						Debug.Log("AÇÃO JÁ USADA PREVIAMENTE PELO PLAYER 2");
						return false;
					}
			}
			player2Actions[playerActionsIndexForChoosing] = "ATTACK";
			//prohibitedActions[playerActionsIndexForChoosing] = "ATTACK";
			Debug.Log("AÇÃO ESCOLHIDA PELO PLAYER 2: ATAQUE");
			//playerActionsIndexForChoosing ++;
			return true;
		}
		else if(Input.GetKeyDown(KeyCode.Keypad2)){
			for(int i =0; i<3; i++){
					if(prohibitedActionsPlayer2[i] == "DEFEND"){
						Debug.Log("AÇÃO JÁ USADA PREVIAMENTE PELO PLAYER 2");
						return false;
					}
			}
			player2Actions[playerActionsIndexForChoosing] = "DEFEND";
			//prohibitedActions[playerActionsIndexForChoosing] = "DEFEND";
			Debug.Log("AÇÃO ESCOLHIDA PELO PLAYER 2: DEFESA");
			//playerActionsIndexForChoosing ++;
			return  true;
		}
		else if(Input.GetKeyDown(KeyCode.Keypad3)){
			for(int i =0; i<3; i++){
					if(prohibitedActionsPlayer2[i] == "FEINT"){
						Debug.Log("AÇÃO JÁ USADA PREVIAMENTE PELO PLAYER 2");
						return false;
					}
			}
			player2Actions[playerActionsIndexForChoosing] = "FEINT";
			//prohibitedActions[playerActionsIndexForChoosing] = "FEINT";
			Debug.Log("AÇÃO ESCOLHIDA PELO PLAYER 2: FINTA");
			//playerActionsIndexForChoosing ++;
			return true;
		}
		return false;
	}

	void CheckWhoWins(int actionsIndex){
		//Debug.Log(actionsIndex);
		string player1Action = player1Actions[actionsIndex];
		string player2Action = player2Actions[actionsIndex];
		switch (player1Action)
		{
			case "ATTACK":
			
			switch (player2Action)
			{
				case "ATTACK":
				roundWinner = "DRAW";
				animationToBePlayed = "Attack vs Attack";
				//_sfxHandler.PlayDraw();
				break;

				case "DEFEND":
				roundWinner = "PLAYER 2";
				player1HP--;
				//_sfxHandler.PlayAttack();
				animationToBePlayed = "Attack vs Block";
				break;

				case "FEINT":
				roundWinner = "PLAYER 1";
				player2HP--;
                //_sfxHandler.PlayAttack();
                animationToBePlayed = "Attack vs Fender";
				break;
			}
			break;

			case "DEFEND":
			switch (player2Action)
			{
				case "ATTACK":
				roundWinner = "PLAYER 1";
				player2HP--;
				animationToBePlayed = "Block vs Attack";
				//_sfxHandler.PlayAttack();
				break;

				case "DEFEND":
				roundWinner = "DRAW";
				animationToBePlayed = "Block vs Block";
                //_sfxHandler.PlayDraw();
                break;

				case "FEINT":
				roundWinner = "PLAYER 2";
				player1HP--;
				animationToBePlayed = "Block vs Fender";
				//_sfxHandler.PlayAttack();
				break;
                }
			break;

			case "FEINT":
			switch (player2Action)
			{
				case "ATTACK":
				roundWinner = "PLAYER 2";
				player1HP--;
				animationToBePlayed = "Fender vs Attack";
				//_sfxHandler.PlayAttack();
				break;

                    case "DEFEND":
				roundWinner = "PLAYER 1";
				player2HP--;
				animationToBePlayed = "Fender vs Block";
				//_sfxHandler.PlayAttack();
				break;

                    case "FEINT":
				roundWinner = "DRAW";
				animationToBePlayed = "Fender vs Fender";
                //_sfxHandler.PlayDraw();
				break;
                }
			break;

		}

		Debug.Log("VENCEDOR DA RODADA: " + roundWinner);
	}  

	void PlayAnim(){
		/*if(animtimer <= 0){
			roundIsPlaying = false;
			animtimer = 1.5f;
			return;
		}
		animtimer-= Time.deltaTime;*/
		if(tickleTimer <=0 && !tickleSoundIsPlaying){
			if(roundWinner == "DRAW"){
				_sfxHandler.PlayDraw();
			}
			else{
				_sfxHandler.PlayAttack();
			}
			//_sfxHandler.PlayAttack();
			tickleSoundIsPlaying = true;
		}
		tickleTimer -= Time.deltaTime;
		charactersAnimator.Play(animationToBePlayed);
		//Debug.Log(charactersAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
		if (charactersAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationToBePlayed) && charactersAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1){
			Debug.Log("VOLTANDO AO ESTADO IDLE");
			roundIsPlaying = false;
			animationToBePlayed = "Idle";
			charactersAnimator.Play(animationToBePlayed);
			tickleTimer = 1.5f;
			tickleSoundIsPlaying = false;
			}

	}

	
}
