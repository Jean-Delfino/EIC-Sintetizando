using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameUserInterface.Text;

namespace PhasePart.AMN{
    public class AMNStack : MonoBehaviour{
        
        [SerializeField] Letter amnPrefab;

        public void PushAMN(string amnName){
            Letter hold;

            hold = Instantiate<Letter>(amnPrefab, this.transform);
            hold.Setup(amnName);
        }
    }
}
