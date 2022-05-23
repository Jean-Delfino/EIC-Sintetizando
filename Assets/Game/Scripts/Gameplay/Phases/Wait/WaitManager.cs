using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;

/*
    When there were letters there was the possibility of more them one wait manager
    Now it just the base class, and it might be used some day 
*/

namespace PhasePart.Wait{
    public class WaitManager : MonoBehaviour{
        private int numberPhase;

        protected void SetNumberPhase(int numberPhase){
            this.numberPhase = numberPhase;
        }

        protected async Task<bool> WaitCheck(){
            bool number = (bool) await FindObjectOfType<GameplayManager>().Check(numberPhase);
            return number;
        }
    } 
}  
