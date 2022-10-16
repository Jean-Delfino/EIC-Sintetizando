using System.Collections.Generic;
using UnityEngine;

/*
    Before it was a placeholder, now it hold the basic information of the phase
    And all the phaseMono use the EndPhase when they meet certain objective
*/

namespace PhasePart{
    public class PhaseManagerMono : MonoBehaviour , PhaseManager{
        [Space]
        [Header("Basic Manager Atributes")]
        [Space]

        [SerializeField] PhaseDescription phaseDescription; //Used in the mission
        [SerializeField] List<string> textInstructions = default; //Used in the Marking
        [SerializeField] GameObject instructions; //More complex information, visual probably

        public void SpawnInstructions(Transform spawn){
            Instantiate<GameObject>(instructions, spawn);
        }

        public GameObject SpawnInstructions(){ //Kinda of a good practice
            instructions.SetActive(true);
            return instructions;
        }

        public void StartAnimation(){
            instructions.GetComponent<Animator>().SetBool("Instruction", true);
        }
        public void StopAnimation(){
            instructions.GetComponent<Animator>().SetBool("Instruction", false);
        }
        public void EndPhase(){
            FindObjectOfType<GameplayManager>(true).IncreacePhase();
        }

        public PhaseDescription GetPhaseDescription(){
            return this.phaseDescription;
        }

        public List<string> GetTextInstructions(){
            return this.textInstructions;
        }

        public GameObject GetInstructions(){
            return this.instructions;
        }
    }
}
