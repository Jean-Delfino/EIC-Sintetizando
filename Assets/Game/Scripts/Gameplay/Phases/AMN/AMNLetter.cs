using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GameUserInterface.Text;

//Colocar nome aminoácidos

namespace PhasePart.AMN{
    public class AMNLetter : Letter{
        [SerializeField] Image AMNcolor = default;

        public void SetAMNColor(Color newColor){
            AMNcolor.color = newColor;
        }
    }
}
