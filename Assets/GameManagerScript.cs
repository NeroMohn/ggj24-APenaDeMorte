using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
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
    [SerializeField]private int player2HP = 9;
    [SerializeField]private int player1HP = 9;
    public float animtimer = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        if(!multiplayer){
            actionsDictionary.Add(0, "ATTACK");
            actionsDictionary.Add(1, "DEFEND");
            actionsDictionary.Add(2, "FEINT");
            EnemyActionRandomizer();
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

        else if(roundIsPlaying){
            PlayAnim();
        }

    }

    void GameLoopSinglePlayer(){
        if(playerActionsIndexForChoosing == 0){
            if(Player1ChooseActions()){
                player1ActionsChosen[0] = true;
            }
            
        }
        if(!roundsPlayed[0] && Input.GetKeyDown("space") && player1ActionsChosen[0] && playerActionsIndexForChoosing == 0){
            prohibitedActionsPlayer1[0] = player1Actions[0];
            playerActionsIndexForChoosing ++;
            CheckWhoWins(0);
            roundsPlayed[0] = true;
            roundIsPlaying = true;
            
            
        }
        if(playerActionsIndexForChoosing == 1){
            if(Player1ChooseActions()){
                player1ActionsChosen[1] = true;
            }
        }
        if(!roundsPlayed[1] && Input.GetKeyDown("space") && player1ActionsChosen[1] && playerActionsIndexForChoosing == 1){
            prohibitedActionsPlayer1[1] = player1Actions[1];
            playerActionsIndexForChoosing ++;
            CheckWhoWins(1);
            roundsPlayed[1] = true;
            roundIsPlaying = true;
            
            
        }
        if(playerActionsIndexForChoosing == 2){
            if(Player1ChooseActions()){
                player1ActionsChosen[2] = true;
            }
        }
        if(!roundsPlayed[2] && Input.GetKeyDown("space") && player1ActionsChosen[2]){
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
        if(playerActionsIndexForChoosing == 0 && !roundIsPlaying){
            if(Player1ChooseActions()){
                player1ActionsChosen[0] = true;
            }
            if(Player2ChooseActions()){
                player2ActionsChosen[0] = true;
            }
            
        }
        if(!roundsPlayed[0] && Input.GetKeyDown("space") && player1ActionsChosen[0] && player2ActionsChosen[0] && playerActionsIndexForChoosing == 0){
            prohibitedActionsPlayer1[0] = player1Actions[0];
            prohibitedActionsPlayer2[0] = player2Actions[0];
            playerActionsIndexForChoosing ++;
            CheckWhoWins(0);
            roundsPlayed[0] = true;
            roundIsPlaying = true;
            
            
        }
        if(playerActionsIndexForChoosing == 1){
            if(Player1ChooseActions()){
                player1ActionsChosen[1] = true;
            }
            if(Player2ChooseActions()){
                player2ActionsChosen[1] = true;
            }
        }
        if(!roundsPlayed[1] && Input.GetKeyDown("space") && player1ActionsChosen[1] && player2ActionsChosen[1] && playerActionsIndexForChoosing == 1){
            prohibitedActionsPlayer1[1] = player1Actions[1];
            prohibitedActionsPlayer2[1] = player2Actions[1];
            playerActionsIndexForChoosing ++;
            CheckWhoWins(1);
            roundsPlayed[1] = true;
            roundIsPlaying = true;
            
            
        }
        if(playerActionsIndexForChoosing == 2){
            if(Player1ChooseActions()){
                player1ActionsChosen[2] = true;
            }
            if(Player2ChooseActions()){
                player2ActionsChosen[2] = true;
            }
        }
        if(!roundsPlayed[2] && Input.GetKeyDown("space") && player1ActionsChosen[2] && player2ActionsChosen[2]){
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
            Debug.Log(player2Actions[i]);
        }
    }

    bool Player1ChooseActions(){
        if(Input.GetKeyDown("1")){
            for(int i =0; i<3; i++){
                    if(prohibitedActionsPlayer1[i] == "ATTACK"){
                        Debug.Log("AÇÃO JÁ USADA PREVIAMENTE");
                        return false;
                    }
            }
            player1Actions[playerActionsIndexForChoosing] = "ATTACK";
            //prohibitedActions[playerActionsIndexForChoosing] = "ATTACK";
            Debug.Log("AÇÃO ESCOLHIDA: ATAQUE");
            //playerActionsIndexForChoosing ++;
            return true;
        }
        else if(Input.GetKeyDown("2")){
            for(int i =0; i<3; i++){
                    if(prohibitedActionsPlayer1[i] == "DEFEND"){
                        Debug.Log("AÇÃO JÁ USADA PREVIAMENTE");
                        return false;
                    }
            }
            player1Actions[playerActionsIndexForChoosing] = "DEFEND";
            //prohibitedActions[playerActionsIndexForChoosing] = "DEFEND";
            Debug.Log("AÇÃO ESCOLHIDA: DEFESA");
            //playerActionsIndexForChoosing ++;
            return  true;
        }
        else if(Input.GetKeyDown("3")){
            for(int i =0; i<3; i++){
                    if(prohibitedActionsPlayer1[i] == "FEINT"){
                        Debug.Log("AÇÃO JÁ USADA PREVIAMENTE");
                        return false;
                    }
            }
            player1Actions[playerActionsIndexForChoosing] = "FEINT";
            //prohibitedActions[playerActionsIndexForChoosing] = "FEINT";
            Debug.Log("AÇÃO ESCOLHIDA: FINTA");
            //playerActionsIndexForChoosing ++;
            return true;
        }
        return false;
    }

    bool Player2ChooseActions(){
        if(Input.GetKeyDown(KeyCode.Keypad1)){
            for(int i =0; i<3; i++){
                    if(prohibitedActionsPlayer2[i] == "ATTACK"){
                        Debug.Log("AÇÃO JÁ USADA PREVIAMENTE");
                        return false;
                    }
            }
            player2Actions[playerActionsIndexForChoosing] = "ATTACK";
            //prohibitedActions[playerActionsIndexForChoosing] = "ATTACK";
            Debug.Log("AÇÃO ESCOLHIDA: ATAQUE");
            //playerActionsIndexForChoosing ++;
            return true;
        }
        else if(Input.GetKeyDown(KeyCode.Keypad2)){
            for(int i =0; i<3; i++){
                    if(prohibitedActionsPlayer2[i] == "DEFEND"){
                        Debug.Log("AÇÃO JÁ USADA PREVIAMENTE");
                        return false;
                    }
            }
            player2Actions[playerActionsIndexForChoosing] = "DEFEND";
            //prohibitedActions[playerActionsIndexForChoosing] = "DEFEND";
            Debug.Log("AÇÃO ESCOLHIDA: DEFESA");
            //playerActionsIndexForChoosing ++;
            return  true;
        }
        else if(Input.GetKeyDown(KeyCode.Keypad3)){
            for(int i =0; i<3; i++){
                    if(prohibitedActionsPlayer2[i] == "FEINT"){
                        Debug.Log("AÇÃO JÁ USADA PREVIAMENTE");
                        return false;
                    }
            }
            player2Actions[playerActionsIndexForChoosing] = "FEINT";
            //prohibitedActions[playerActionsIndexForChoosing] = "FEINT";
            Debug.Log("AÇÃO ESCOLHIDA: FINTA");
            //playerActionsIndexForChoosing ++;
            return true;
        }
        return false;
    }

    void CheckWhoWins(int actionsIndex){
        Debug.Log(actionsIndex);
        string player1Action = player1Actions[actionsIndex];
        string player2Action = player2Actions[actionsIndex];
        switch (player1Action)
        {
            case "ATTACK":
            
            switch (player2Action)
            {
                case "ATTACK":
                roundWinner = "DRAW";
                break;

                case "DEFEND":
                roundWinner = "PLAYER 2";
                player1HP--;
                break;

                case "FEINT":
                roundWinner = "PLAYER 1";
                player2HP--;
                break;
            }
            break;

            case "DEFEND":
            switch (player2Action)
            {
                case "ATTACK":
                roundWinner = "PLAYER 1";
                player2HP--;
                break;

                case "DEFEND":
                roundWinner = "DRAW";
                break;

                case "FEINT":
                roundWinner = "PLAYER 2";
                player1HP--;
                break;
            }
            break;

            case "FEINT":
            switch (player2Action)
            {
                case "ATTACK":
                roundWinner = "PLAYER 2";
                player1HP--;
                break;

                case "DEFEND":
                roundWinner = "PLAYER 1";
                player2HP--;
                break;

                case "FEINT":
                roundWinner = "DRAW";
                break;
            }
            break;

        }

        Debug.Log(roundWinner);
    }  

    void PlayAnim(){
        if(animtimer <= 0){
            roundIsPlaying = false;
            animtimer = 1.5f;
            return;
        }
        animtimer-= Time.deltaTime;
    }
}
