using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameUserInterface.Text;

namespace PhasePart.RNA.DNA{
    public class DNAStructureWithRNA : MonoBehaviour{
        [SerializeField] Letter prefabDisplay; 
        [SerializeField] Transform firstHalfDNA;
        [SerializeField] Transform secondHalfDNA;
        [SerializeField] Transform insideDNARNA;

        public void SetupStructure(int quantity, string text){
            int i;
            Letter hold;

            for(i = 0; i < quantity; i++){
                hold = Instantiate<Letter>(prefabDisplay, firstHalfDNA);
                hold.Setup(text[i].ToString());

                Instantiate<Letter>(prefabDisplay, secondHalfDNA); //Invisible at this point
                Instantiate<Letter>(prefabDisplay, insideDNARNA); //Don't need to be set, because the RNA will do it
            }
        }

        public void ChangeAllFirstHalf(string text){
            int i;

            for(i = 0; i < firstHalfDNA.childCount; i++){
                firstHalfDNA.GetChild(i).GetComponent<Letter>().Setup(text[i].ToString());
            }
        }

        public void ChangeRNAinDNAStructure(int index, string text){
            print("ENTROU CHANGE " + "INDEX = " + index + " " + text);
            insideDNARNA.GetChild(index).GetComponent<Letter>().Setup(text);
        }

        public void ChangeVisibilitySecondHalfDNA(){
            secondHalfDNA.gameObject.SetActive(true);
        }
    }
}
