using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

/*
    It has the responsability over the DNA in the cell
    It changes the DNA based on the RNA too.

    It has the responsability of controlling the animation of the DNA/RNA
*/

namespace PhasePart.RNA.DNA{
    public class DNAManager : PhaseManagerMono{
        [Space]
        [Header("DNA Manager Atributes")]
        [Space]
        [SerializeField] DNAStructureWithRNA dnaSetupReference = default; //Visual part of the DNA
        [SerializeField] CellAnimator cellReference = default; //Call all the animations

        private Dictionary<string, string> validationDNADNA = new Dictionary<string, string>(){
            {"A", "T"},
            {"T", "A"},
            {"C", "G"},
            {"G", "C"}
        }; //DNA to the DNA correspondence

        private string finiteDNAString; //Gets it from the RNASpawner

        private void Start() {
            ChangeSecondHalf();
            dnaSetupReference.ChangeVisibilitySecondHalfDNA(true);

            //Animation of the RNA passing the hole
            RNAEscapeAnimation();
        }

        private async void RNAEscapeAnimation(){
            await Task.Delay(Util.ConvertToMili(cellReference.RNAEscapeNucleus()));
            cellReference.SetAnimatorStatus(false);
            base.EndPhase();
        }

        public void TurnDNAOn(){
            this.dnaSetupReference.GetHolderOfStructure().SetActive(true);
        }
        
        public void SetFiniteDNAString(string finiteDNAString){
            this.finiteDNAString = finiteDNAString;
        }

        //Dna setup settings
        public void SetupStructure(int quantity, string firstCut, bool add){
            dnaSetupReference.SetupStructure(quantity, firstCut, add);
        }

        public void ChangeFirstHalf(string cut){
            SetFiniteDNAString(cut);
            dnaSetupReference.ChangeAllFirstHalf(cut);
        }

        public void ChangeSecondHalf(){
            //Create the complement of the DNA
            dnaSetupReference.ChangeAllSecondHalf(CreateComplementaryDNA()); 
        }

        public void ChangeSecondHalf(int index, string text){
            dnaSetupReference.ChangeSecondHalf(index, text);
        }

        public void ChangeRNAinDNAStructureByChildPosition(int index, string text){
            dnaSetupReference.ChangeRNAinDNAStructureByChildPosition(index, text);
        }

        public void ChangeRNAinDNAStructure(int index, string text){
            dnaSetupReference.ChangeRNAinDNAStructure(index, text);
        }

        //Uses the Validation table
        private string CreateComplementaryDNA(){
            return CreateComplementaryDNA(finiteDNAString);
        }

        private string CreateComplementaryDNA(string textDNA){
            string complement = "";
            int i;

            for(i = 0; i < textDNA.Length; i++){
                complement += validationDNADNA[textDNA[i].ToString()];
            }

            return complement;
        }


        //Animations
        public async Task RNAVisibility(){
            GameObject dnaRna = dnaSetupReference.GetRNADNA();
            
            dnaRna.SetActive(true);

            Util.ChangeAlphaCanvasImageAnimation(dnaRna.GetComponent<CanvasGroup>(),
                1f, 1f);

            await Task.Delay(Util.ConvertToMili(1f));
        }

        public async Task DNASeparation(){
            GameObject dnaSecond = dnaSetupReference.GetSecondHalf();

            Util.ChangeAlphaCanvasImageAnimation(dnaSecond.GetComponent<CanvasGroup>(),
                0f, 1f);
            
            await Task.Delay(Util.ConvertToMili(1f));

            dnaSecond.SetActive(false);

            Util.ChangeAlphaCanvasImageAnimation(dnaSecond.GetComponent<CanvasGroup>(),
                1f, 0f);
        }

        public async Task DNANucleusVisibility(bool state){
            if(state){
                await Task.Delay(Util.ConvertToMili(cellReference.ExpandCellNucleus()));
                return;
            }

            await Task.Delay(Util.ConvertToMili(cellReference.ShrinkCellNucleus()));
            return;
        }

        public void SetSeparationInDNAStructure(int separation){
            dnaSetupReference.SetSeparation(separation);
        }

        public void SetQuantity(int quantity){
            dnaSetupReference.SetQuantity(quantity);
        }
    }
}
