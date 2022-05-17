using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
=======
using UnityEngine.UI;
>>>>>>> Stashed changes

using GameUserInterface.Text;

namespace PhasePart.AMN{
    public class AMNQueue : MonoBehaviour{
        
<<<<<<< Updated upstream
        [SerializeField] Letter amnPrefab;

        [SerializeField] GameObject transporter;

        Color actualColor;

        public void NewAMNInLine(){
            //Generates a new color
=======
        [SerializeField] Letter amnPrefab = default;
        [SerializeField] RibossomeAnimator transporterList = default;

        private Color actualColor;

        public void NewAMNInLine(bool newRb, string amnName){
            //Generates a new color
            Color newColor;

            do{
                newColor = Util.RandomSolidColor();
            }while(newColor == actualColor);

            actualColor = newColor;

            transporterList.RibossomeExit();

            if(newRb){
                transporterList.RibossomeEnter(actualColor);
            }

            PushAMN(amnName);
>>>>>>> Stashed changes
        }

        public void PushAMN(string amnName){
            Letter hold;

            hold = Instantiate<Letter>(amnPrefab, this.transform);
<<<<<<< Updated upstream
            hold.Setup(amnName);
        }
=======
            hold.gameObject.GetComponent<Image>().color = actualColor;
            hold.Setup(amnName);
        }


>>>>>>> Stashed changes
    }
}
