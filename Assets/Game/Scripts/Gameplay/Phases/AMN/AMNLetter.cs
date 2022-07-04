using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using GameUserInterface.Text;

//Colocar nome aminoácidos

namespace PhasePart.AMN{
    public class AMNLetter : Letter{
        [Space]
        [Header("AMNLetter new atributes")]
        [Space]

        [SerializeField] Image AMNcolor = default;
        [SerializeField] TextMeshProUGUI amnName = default;

        [Space]
        [Header("Exclusive usage in the AMNQueue")]
        [Space]

        [SerializeField] CanvasGroup cg = default;
        [SerializeField] AMNConnector amnBridge = default;
        
        public void SetAMNColor(Color newColor){
            AMNcolor.color = newColor;
        }

        public Image ReturnAMNLetterImage(){
            return AMNcolor;
        }

        public void SetupAMNName(string nameAMN){
            amnName.text = nameAMN;
        }

        public CanvasGroup GetAMNGroupName(){
            return cg;
        }

        public AMNConnector GetAMNBridge(){
            return amnBridge;
        }
    }
}
