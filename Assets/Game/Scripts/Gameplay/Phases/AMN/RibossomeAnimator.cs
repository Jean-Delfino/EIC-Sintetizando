using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameUserInterface.Animation;

namespace PhasePart.AMN{
    /*
        Control all the animations of the Ribossome, self explanatory
    */
    public class RibossomeAnimator : AnimatorUser{
        public void RibossomeExit(){
            //Takes out the first ribossome of the list
        }

        public void RibossomeEnter(Color newRibossomeColor){
            //Create a new Ribossome
            //Kinda strange, but there's a Ribossome inactive
            //I set its color, then set it active, the animation work then with this Ribossome


            //In the ending of the animation it create another ribossome
            //Set it in the correct position and in inactive
        }
    }
}
