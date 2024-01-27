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
    // Start is called before the first frame update
    void Start()
    {
        actionsDictionary.Add(0, "Attack");
        actionsDictionary.Add(1, "Defend");
        actionsDictionary.Add(2, "Feint");
        EnemyActionRandomizer();
    }

    // Update is called once per frame
    void Update()
    {
        


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
}
