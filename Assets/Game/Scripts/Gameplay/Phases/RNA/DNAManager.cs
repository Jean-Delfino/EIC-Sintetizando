using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhasePart.RNA.DNA{
    public class DNAManager : PhaseManagerMono{
        [SerializeField] DNAStructureWithRNA dnaSetupReference; //Visual part of the DNA

        private Dictionary<string, string> validationDNADNA = new Dictionary<string, string>(){
            {"A", "T"},
            {"T", "A"},
            {"C", "G"},
            {"G", "C"}
        }; //DNA to the DNA correspondence

        private string finiteDNAString; //Gets it from the RNASpawner

        private void Start() {
           dnaSetupReference.ChangeAllSecondHalf(CreateComplementarDNA()); 
           dnaSetupReference.ChangeVisibilitySecondHalfDNA();
           base.EndPhase();
        }

        public void TurnDNAOn(){
            this.dnaSetupReference.GetHolderOfStructure().SetActive(true);
        }
        public void SetFiniteDNAString(string finiteDNAString){
            this.finiteDNAString = finiteDNAString;
        }

        public void SetupStructure(int quantity, string firstCut){
            dnaSetupReference.SetupStructure(quantity, firstCut);
        }

        public void ChangeFirstHalf(string cut){
            SetFiniteDNAString(cut);
            dnaSetupReference.ChangeAllFirstHalf(cut);
        }

        public void ChangeRNAinDNAStructure(int index, string text){
            dnaSetupReference.ChangeRNAinDNAStructure(index, text);
        }

        private string CreateComplementarDNA(){
            string complement = "";
            int i;

            for(i = 0; i < finiteDNAString.Length; i++){
                complement += validationDNADNA[finiteDNAString[i].ToString()];
            }

            return complement;
        }
    }
}
