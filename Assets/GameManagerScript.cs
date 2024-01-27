using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private string[] playerActions = new string[3];
    private string[] enemyActions = new string[3];
    private Dictionary<int, string> actionsDictionary = new Dictionary<int, string>();
    private int firstEnemyAction;
    private int secondEnemyAction;
    private int thirdEnemyAction;
    private int playerActionsIndexForChoosing = 0;
    private bool[] playerActionsChosen = new bool[3];
    private string[] prohibitedActions = new string[3];
    private bool roundEnd = false;
    private string roundWinner; //"PLAYER", "ENEMY" ou "DRAW"
    [SerializeField]private int enemyHP = 9;
    [SerializeField]private int playerHP = 9;
    // Start is called before the first frame update
    void Start()
    {
        actionsDictionary.Add(0, "ATTACK");
        actionsDictionary.Add(1, "DEFEND");
        actionsDictionary.Add(2, "FEINT");
        EnemyActionRandomizer();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerActionsIndexForChoosing == 0){
            if(PlayerChooseActions()){
                playerActionsChosen[0] = true;
            }
            
        }
        else if(Input.GetKeyDown("space") && playerActionsChosen[0]){
            CheckWhoWins(playerActionsIndexForChoosing);
            playerActionsIndexForChoosing++;
        }
        else if(playerActionsIndexForChoosing == 1){
            if(PlayerChooseActions()){
                playerActionsChosen[1] = true;
            }
        }
        else if(Input.GetKeyDown("space") && playerActionsChosen[1]){
            CheckWhoWins(playerActionsIndexForChoosing);
            playerActionsIndexForChoosing++;
        }
        else if(playerActionsIndexForChoosing == 2){
            if(PlayerChooseActions()){
                playerActionsChosen[2] = true;
            }
        }
        else if(Input.GetKeyDown("space") && playerActionsChosen[2]){
            CheckWhoWins(2);
            roundEnd = true;
        }
        else if(roundEnd){
            for( int i =0; i<3; i++){
                playerActionsChosen[i] =  false;
                playerActions[i] = "";
                enemyActions[i] = "";
            }
            firstEnemyAction =-1;
            secondEnemyAction =-1;
            thirdEnemyAction =-1;
            EnemyActionRandomizer();
            playerActionsIndexForChoosing = 0;
            roundEnd = false;
        }



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

        enemyActions[0] = actionsDictionary[firstEnemyAction];
        enemyActions[1] = actionsDictionary[secondEnemyAction];
        enemyActions[2] = actionsDictionary[thirdEnemyAction];

        for(int i = 0; i<3; i++){
            Debug.Log(enemyActions[i]);
        }
    }

    bool PlayerChooseActions(){
        if(Input.GetKeyDown("1")){
            for(int i =0; i<3; i++){
                    if(prohibitedActions[i] == "ATTACK"){
                        Debug.Log("AÇÃO JÁ USADA PREVIAMENTE");
                        return false;
                    }
            }
            playerActions[playerActionsIndexForChoosing] = "ATTACK";
            prohibitedActions[playerActionsIndexForChoosing] = "ATTACK";
            Debug.Log("AÇÃO ESCOLHIDA: ATAQUE");
            playerActionsIndexForChoosing ++;
            return true;
        }
        else if(Input.GetKeyDown("2")){
            for(int i =0; i<3; i++){
                    if(prohibitedActions[i] == "DEFEND"){
                        Debug.Log("AÇÃO JÁ USADA PREVIAMENTE");
                        return false;
                    }
            }
            playerActions[playerActionsIndexForChoosing] = "DEFEND";
            prohibitedActions[playerActionsIndexForChoosing] = "DEFEND";
            Debug.Log("AÇÃO ESCOLHIDA: DEFESA");
            playerActionsIndexForChoosing ++;
            return  true;
        }
        else if(Input.GetKeyDown("3")){
            for(int i =0; i<3; i++){
                    if(prohibitedActions[i] == "FEINT"){
                        Debug.Log("AÇÃO JÁ USADA PREVIAMENTE");
                        return false;
                    }
            }
            playerActions[playerActionsIndexForChoosing] = "FEINT";
            prohibitedActions[playerActionsIndexForChoosing] = "FEINT";
            Debug.Log("AÇÃO ESCOLHIDA: FINTA");
            playerActionsIndexForChoosing ++;
            return true;
        }
        return false;
    }

    void CheckWhoWins(int actionsIndex){
        string playerAction = playerActions[actionsIndex];
        string enemyAction = enemyActions[actionsIndex];
        switch (playerAction)
        {
            case "ATTACK":
            switch (enemyAction)
            {
                case "ATTACK":
                roundWinner = "DRAW";
                break;

                case "DEFEND":
                roundWinner = "ENEMY";
                playerHP--;
                break;

                case "FEINT":
                roundWinner = "PLAYER";
                enemyHP--;
                break;
            }
            break;

            case "DEFEND":
            switch (enemyAction)
            {
                case "ATTACK":
                roundWinner = "PLAYER";
                enemyHP--;
                break;

                case "DEFEND":
                roundWinner = "DRAW";
                break;

                case "FEINT":
                roundWinner = "ENEMY";
                playerHP--;
                break;
            }
            break;

            case "FEINT":
            switch (enemyAction)
            {
                case "ATTACK":
                roundWinner = "ENEMY";
                playerHP--;
                break;

                case "DEFEND":
                roundWinner = "PLAYER";
                enemyHP--;
                break;

                case "FEINT":
                roundWinner = "DRAW";
                break;
            }
            break;

        }
    }  
}
