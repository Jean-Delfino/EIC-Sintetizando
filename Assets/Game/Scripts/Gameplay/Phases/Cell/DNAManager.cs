using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhasePart.RNA.DNA{
    public class DNAManager : PhaseManagerMono{
        [Space]
        [Header("DNA Manager Atributes")]
        [Space]
        [SerializeField] DNAStructureWithRNA dnaSetupReference = default; //Visual part of the DNA
        [SerializeField] CellAnimator cellReferece = default; //Call all the animations

        private Dictionary<string, string> validationDNADNA = new Dictionary<string, string>(){
            {"A", "T"},
            {"T", "A"},
            {"C", "G"},
            {"G", "C"}
        }; //DNA to the DNA correspondence

        private string finiteDNAString; //Gets it from the RNASpawner

        private void Start() {
            dnaSetupReference.ChangeAllSecondHalf(CreateComplementarDNA()); //Create the complement of the DNA
            dnaSetupReference.ChangeVisibilitySecondHalfDNA(); 

            cellReferece.RNAEscapeNucleus();//Animation of the RNA passing the hole

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

        public void DNANucleusVisibility(bool state){
            if(state){
                cellReferece.ExpandCellNucleus();
                return;
            }

            cellReferece.ShrinkCellNucleus();
        }
    }
}
