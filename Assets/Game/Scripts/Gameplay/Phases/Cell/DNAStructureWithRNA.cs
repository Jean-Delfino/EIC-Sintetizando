using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameUserInterface.Text;

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
        
        private int separation = -1;
        private string separationString;


        public void SetupStructure(int quantity, string text){
            int i;
            Letter hold;

            Transform firstHalfDNA = firstHalfDNAObject.transform.GetChild(1);
            Transform secondHalfDNA = secondHalfDNAObject.transform.GetChild(1);
            Transform insideDNARNA = insideRNADNAObject.transform.GetChild(1);

            if(layoutSonAddition == -1){
                layoutSonAddition = firstHalfDNA.childCount;
            }

            for(i = 0; i < quantity; i++){
                hold = Instantiate<Letter>(prefabDisplay, firstHalfDNA);
                hold.Setup(text[i].ToString());

                Instantiate<Letter>(prefabDisplay, secondHalfDNA); //Invisible at this point
                Instantiate<Letter>(prefabDisplay, insideDNARNA); //Don't need to be set, because the RNA will do it
            }
        }

        private void SetGenericStructure(Transform structure, string text){
            int i;

            for(i = 0; i < firstQuantity; i++){
                ChangeStructureLetter(structure, i + layoutSonAddition, text[i].ToString());
            }
            for(i = 0; i < separation; i++){
                ChangeStructureLetter(structure, i + firstQuantity, separationString);
            }
            for(i = i + firstQuantity; i < structure.childCount; i++){
                ChangeStructureLetter(structure, i + layoutSonAddition, text[i - separation].ToString());
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
            Transform insideDNARNA = insideRNADNAObject.transform.GetChild(1);
            
            insideDNARNA.GetChild(index + layoutSonAddition).GetComponent<Letter>().Setup(text);
        }


        public void ChangeVisibilitySecondHalfDNA(bool state){
            secondHalfDNAObject.SetActive(state);
        }  

        public void SetSeparation(int separation){
            this.separation = separation;
        }

        public void SetSeparationString(string separationString){
            this.separationString = separationString;
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
