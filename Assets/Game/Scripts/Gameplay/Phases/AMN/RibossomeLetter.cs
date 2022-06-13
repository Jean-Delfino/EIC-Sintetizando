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
        [SerializeField] List<Image> colorDef = default;

        public void SetRibossomeColor(Color newColor){
            int i;

            for(i = 0; i < colorDef.Count; i++){
                colorDef[i].color = newColor;
            }
        }
    }
}
