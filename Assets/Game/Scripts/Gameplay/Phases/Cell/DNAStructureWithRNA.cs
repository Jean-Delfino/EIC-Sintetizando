using UnityEngine;

using System.Collections.Generic;

using GameUserInterface.Text;
using PhasePart.AMN;

namespace PhasePart.RNA.DNA{
    public class DNAStructureWithRNA : MonoBehaviour{
        [SerializeField] Letter prefabDisplay; 
        [SerializeField] GameObject DNAHold = default;

        [Space]
        [Header("GameObjects to the Animations")]
        [Space]

        [SerializeField] GameObject firstHalfDNAObject = default; 
        [SerializeField] GameObject secondHalfDNAObject = default; 
        [SerializeField] GameObject insideRNADNAObject = default; 

        private int layoutSonAddition = -1;
        private int firstQuantity = -1;

        private int separationOfDNA;

        private List<Letter> RNAfieldsList = new();

        public void SetupStructure(int quantity, string text, bool addToStructure){
            int i;
            Letter hold;

            Transform firstHalfDNA = firstHalfDNAObject.transform.GetChild(1);
            Transform secondHalfDNA = secondHalfDNAObject.transform.GetChild(1);
            Transform insideRNADNA = insideRNADNAObject.transform.GetChild(1);

            if(layoutSonAddition == -1){
                layoutSonAddition = firstHalfDNA.childCount;
            }

            for(i = 0; i < quantity; i++){
                hold = Instantiate<Letter>(prefabDisplay, firstHalfDNA);
                hold.Setup(text[i].ToString());
            
                hold = Instantiate<Letter>(prefabDisplay, insideRNADNA); //Don't need to be set, because the RNA will do it

                if(addToStructure){
                    RNAfieldsList.Add(hold);
                }

                Instantiate<Letter>(prefabDisplay, secondHalfDNA);
            }
        }

        private void SetGenericStructure(Transform structure, string text){
            int count;
            int index = 0;
            int separationAddition = 0;

            //print("FIRST QUANTITY = " + firstQuantity);

            for(count = 0; count < AMNManager.GetSizeAMN(); count++, index++){
                ChangeStructureLetter(structure, index + separationAddition + layoutSonAddition, text[index].ToString());
            }
            
            for(count = 0, separationAddition += separationOfDNA; count < firstQuantity; count++, index++){
                ChangeStructureLetter(structure, index + separationAddition + layoutSonAddition, text[index].ToString());
            }
            
            for(count = 0, separationAddition += separationOfDNA; count < AMNManager.GetSizeAMN(); count++, index++){
                ChangeStructureLetter(structure, index + separationAddition + layoutSonAddition, text[index].ToString());
            }
        }

        private void ChangeStructureLetter(Transform structure, int index, string text){
            structure.GetChild(layoutSonAddition + index).GetComponent<Letter>().Setup(text);
        }

        public void ChangeSecondHalf(int index, string text){
            Transform secondHalfDNA = secondHalfDNAObject.transform.GetChild(1);

            secondHalfDNA.GetChild(layoutSonAddition + index).GetComponent<Letter>().Setup(text);
        }

        public void ChangeAllSecondHalf(string text){
            Transform secondHalfDNA = secondHalfDNAObject.transform.GetChild(1);

            SetGenericStructure(secondHalfDNA, text);
        }

        public void ChangeAllFirstHalf(string text){
            Transform firstHalfDNA = firstHalfDNAObject.transform.GetChild(1);

            SetGenericStructure(firstHalfDNA, text);
        }

        public void ChangeRNAinDNAStructure(int index, string text){
            RNAfieldsList[index].Setup(text);
        }

        public void ChangeRNAinDNAStructureByChildPosition(int index, string text){
            Transform insideDNARNA = insideRNADNAObject.transform.GetChild(1);
            
            insideDNARNA.GetChild(index + layoutSonAddition).GetComponent<Letter>().Setup(text);
        }

        public void ChangeVisibilitySecondHalfDNA(bool state){
            secondHalfDNAObject.SetActive(state);
        }  

        public void SetSeparation(int separation){
            this.separationOfDNA = separation;
        }

        public void SetQuantity(int firstQuantity){
            this.firstQuantity = firstQuantity;
        }

        public GameObject GetHolderOfStructure(){
            return DNAHold;
        }

        public GameObject GetRNADNA(){
            return insideRNADNAObject;
        }

        public GameObject GetFirstHalf(){
            return firstHalfDNAObject;
        }

        public GameObject GetSecondHalf(){
            return secondHalfDNAObject;
        }
    }
}
