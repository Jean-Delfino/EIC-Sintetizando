using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/*
    There is a significant interaction with the Close Button
*/

namespace PhasePart.Wait{
    public class InstructionWait : MonoBehaviour{
        [SerializeField] Transform childInstruction = default;
        [SerializeField] Button closeButton = default;

        private int actualChildInstruction = 0;
        private int childCnt;

        private void Start() {
            childCnt = childInstruction.childCount;
        }

        public void IncreaceChildInstruction(){
            if(actualChildInstruction == childCnt - 1){
                closeButton.onClick.Invoke();
                return;
            }

            childInstruction.GetChild(actualChildInstruction).gameObject.SetActive(false);
            actualChildInstruction++;
            childInstruction.GetChild(actualChildInstruction).gameObject.SetActive(true);
        }

        public void DecreaceChildInstruction(){
            if(actualChildInstruction == 0){
                return;
            }

            childInstruction.GetChild(actualChildInstruction).gameObject.SetActive(false);
            actualChildInstruction--;
            childInstruction.GetChild(actualChildInstruction).gameObject.SetActive(true);
        }

        public void CloseGame(){ //The button has this method
            Destroy(this.gameObject);
        }
    }
}
