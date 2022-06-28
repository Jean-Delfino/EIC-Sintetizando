using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GameUserInterface.Text;

/*
    No matter what change, Ball = childCount -1. If that is correct them everything will work fine
*/

namespace PhasePart.AMN{
    public class RibossomeLetter : Letter{
        private int stateRib = 0;
        private bool withAMN = true;

        [SerializeField] List<Image> colorDef = default;

        private Color elemColor;

        public void SetRibossomeColor(Color newColor){
            int i;

            for(i = 0; i < colorDef.Count; i++){
                colorDef[i].color = newColor;
            }

            elemColor = newColor;
        }

        public void ConnectTwoLetters(RibossomeLetter other){
            
        }

        public void IncreaceState(){this.stateRib++;}

        public int GetStateRib(){return this.stateRib;}

        public void SetStateRib(int stateRib){this.stateRib = stateRib;}

        public Color GetRibossomeColor(){return this.elemColor;}

        public bool GetAMNPresence(){return withAMN;}

        public void SetAMNPresence(){withAMN = !withAMN;}
        public void SetAMNPresence(bool state){withAMN = state;}
    }
}
