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

        int layoutSonAddition;

        public void SetupStructure(int quantity, string text){
            int i;
            Letter hold;

            Transform firstHalfDNA = firstHalfDNAObject.transform.GetChild(1);
            Transform secondHalfDNA = secondHalfDNAObject.transform.GetChild(1);
            Transform insideDNARNA = insideRNADNAObject.transform.GetChild(1);

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
            Transform secondHalfDNA = secondHalfDNAObject.transform.GetChild(1);

            for(i = 0; i < secondHalfDNA.childCount; i++){
                secondHalfDNA.GetChild(i + layoutSonAddition).GetComponent<Letter>().Setup(text[i].ToString());
            }
        }

        public void ChangeAllFirstHalf(string text){
            int i;
            Transform firstHalfDNA = firstHalfDNAObject.transform.GetChild(1);

            for(i = 0; i < firstHalfDNA.childCount; i++){
                firstHalfDNA.GetChild(i + layoutSonAddition).GetComponent<Letter>().Setup(text[i].ToString());
            }
        }

        public void ChangeRNAinDNAStructure(int index, string text){
            Transform insideDNARNA = insideRNADNAObject.transform.GetChild(1);
            
            insideDNARNA.GetChild(index + layoutSonAddition).GetComponent<Letter>().Setup(text);
        }


        public void ChangeVisibilitySecondHalfDNA(bool state){
            secondHalfDNAObject.SetActive(state);
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
