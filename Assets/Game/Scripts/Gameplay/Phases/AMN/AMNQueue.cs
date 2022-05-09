using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameUserInterface.Text;

namespace PhasePart.AMN{
    public class AMNQueue : MonoBehaviour{
        
        [SerializeField] Letter amnPrefab;

        [SerializeField] GameObject transporter;

        Color actualColor;

        public void NewAMNInLine(){
            //Generates a new color
        }

        public void PushAMN(string amnName){
            Letter hold;

            hold = Instantiate<Letter>(amnPrefab, this.transform);
            hold.Setup(amnName);
        }
    }
}
