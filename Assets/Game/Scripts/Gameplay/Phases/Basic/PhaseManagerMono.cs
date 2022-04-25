using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Before it was a placeholder, now it hold the basic information of the phase
    And all the phaseMono use the EndPhase when they meet certain objective
*/

namespace PhasePart{
    public class PhaseManagerMono : MonoBehaviour , PhaseManager{
        [SerializeField] PhaseDescription pd;
        [SerializeField] List<string> textInstructions = default; //Used in the Marking

        public void EndPhase(){
            FindObjectOfType<GameplayManager>(true).IncreacePhase();
        }

        public PhaseDescription GetPhaseDescription(){
            return this.pd;
        }

        public List<string> GetTextInstructions(){
            return this.textInstructions;
        }
    }
}
