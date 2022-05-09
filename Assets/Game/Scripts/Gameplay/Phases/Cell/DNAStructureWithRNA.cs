using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameUserInterface.Text;

namespace PhasePart.RNA.DNA{
    public class DNAStructureWithRNA : MonoBehaviour{
        [SerializeField] Letter prefabDisplay; 
        [SerializeField] GameObject DNAHold = default;

        [Space]
        [SerializeField] Transform firstHalfDNA = default;

        [Space]
        [SerializeField] Transform secondHalfDNA = default;
        [SerializeField] GameObject secondHalfDNAObject = default; 

        [Space]
        [SerializeField] Transform insideDNARNA = default;

        int layoutSonAddition;

        public void SetupStructure(int quantity, string text){
            int i;
            Letter hold;

            layoutSonAddition = firstHalfDNA.childCount;

            for(i = 0; i < quantity; i++){
                hold = Instantiate<Letter>(prefabDisplay, firstHalfDNA);
                hold.Setup(text[i].ToString());

                Instantiate<Letter>(prefabDisplay, secondHalfDNA); //Invisible at this point
                Instantiate<Letter>(prefabDisplay, insideDNARNA); //Don't need to be set, because the RNA will do it
            }
        }

        public void ChangeAllSecondHalf(string text){
            int i;

            for(i = 0; i < secondHalfDNA.childCount; i++){
                secondHalfDNA.GetChild(i + layoutSonAddition).GetComponent<Letter>().Setup(text[i].ToString());
            }
        }

        public void ChangeAllFirstHalf(string text){
            int i;

            for(i = 0; i < firstHalfDNA.childCount; i++){
                firstHalfDNA.GetChild(i + layoutSonAddition).GetComponent<Letter>().Setup(text[i].ToString());
            }
        }

        public void ChangeRNAinDNAStructure(int index, string text){
            //print("ENTROU CHANGE " + "INDEX = " + index + " " + text);
            insideDNARNA.GetChild(index + layoutSonAddition).GetComponent<Letter>().Setup(text);
        }

        public void ChangeVisibilitySecondHalfDNA(){
            secondHalfDNAObject.SetActive(true);
        }

        public GameObject GetHolderOfStructure(){
            return DNAHold;
        }

        //Animations

        
    }
}
