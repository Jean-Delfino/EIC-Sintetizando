using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Basic information of the phase
    See Wait->WaitManager or Wait->MissionDisplay
*/

namespace PhasePart{

    [System.Serializable]
    public class PhaseDescription{
        [SerializeField] string namePhase;
        [SerializeField] string descriptionPhase;
        [SerializeField] GameObject additionalInfo;

        public string GetName(){
            return this.namePhase;
        }

        public string GetDescription(){
            return this.descriptionPhase;
        }

        public GameObject GetAdditionalInfo(){
            return this.additionalInfo;
        }
    }  
}

