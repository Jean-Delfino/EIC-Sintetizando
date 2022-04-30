using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Used in the gameplay by the object InfoDisplay
*/

namespace GameUserInterface.Text{
    public class InfoDisplay : MonoBehaviour{
        public void IncreaceMessageInfo(int increace){
            FindObjectOfType<GameplayManager>().IncreaceMessage(increace);
        }

        public void DecreaceMessageInfo(int increace){
            FindObjectOfType<GameplayManager>().DecreaceMessage(increace);
        }
    }
}
