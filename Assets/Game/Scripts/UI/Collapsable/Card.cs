using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUserInterface.Collapsable{
    public class Card : MonoBehaviour{
        //Each card has a number attached to the phase
        //This will be handled in the "gameplayManager"
        //When the last card is destroyed the collapsable destroy itself too
        [SerializeField] int numberPhase;

        //For 2D
        private void OnDestroy() {
            this.transform.parent.GetComponent<BodyCard>().CheckEnd();
        }
        public void OnClick(){
            if(FindObjectOfType<GameplayManager>().Check(numberPhase)){
                Destroy(this.gameObject);
            }
        }

        public int GetValue(){
            return numberPhase;
        }
    }
}
