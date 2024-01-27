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
    private string roundWinner; //"PLAYER", "ENEMY" ou "DRAW"
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
            PlayerChooseActions();
        }
        else if(Input.GetKeyDown("space")){

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

    void PlayerChooseActions(){
        if(Input.GetKeyDown("1")){
            playerActions[playerActionsIndexForChoosing] = "ATTACK";
            playerActionsIndexForChoosing ++;
        }
        else if(Input.GetKeyDown("2")){
            playerActions[playerActionsIndexForChoosing] = "DEFEND";
            playerActionsIndexForChoosing ++;
        }
        else if(Input.GetKeyDown("3")){
            playerActions[playerActionsIndexForChoosing] = "FEINT";
            playerActionsIndexForChoosing ++;
        }
    }

    void CheckWhoWins(int round){
        string playerAction = playerActions[round];
        string enemyAction = enemyActions[round];
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
                break;

                case "FEINT":
                roundWinner = "PLAYER";
                break;
            }
            break;

            case "DEFEND":
            switch (enemyAction)
            {
                case "ATTACK":
                roundWinner = "PLAYER";
                break;

                case "DEFEND":
                roundWinner = "DRAW";
                break;

                case "FEINT":
                roundWinner = "ENEMY";
                break;
            }
            break;

            case "FEINT":
            switch (enemyAction)
            {
                case "ATTACK":
                roundWinner = "ENEMY";
                break;

                case "DEFEND":
                roundWinner = "PLAYER";
                break;

                case "FEINT":
                roundWinner = "DRAW";
                break;
            }
            break;

        }
    }  
}
