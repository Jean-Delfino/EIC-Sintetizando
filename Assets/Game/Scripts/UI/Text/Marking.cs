using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    Spawn several "goals" based on the quantity wanted

    Use in the gameplay by the GameplayManager
*/

namespace GameUserInterface.Text{
    public class Marking : MonoBehaviour{
        [SerializeField] GameObject goal;

        int currentGoal;

        public void SpawnGoals(int quantity){ //Just for test
            int i;

            for(i = 0; i < quantity; i++){
                Instantiate<GameObject>(goal , this.transform);
            }
            currentGoal = 0;
        }

        public void SpawnGoal(List<string> texts){
            Instantiate<GameObject>(goal , this.transform).GetComponent<InfoEditableComponent>().Setup(texts,0);
        }

        public void ShowGoal(int index){
            this.transform.GetChild(currentGoal).GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
        } //Just a prototype
    }
}
