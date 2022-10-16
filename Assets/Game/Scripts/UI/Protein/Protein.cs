using System.Collections.Generic;

using UnityEngine;

using PhasePart.RNA;
using GameGeneralScripts.Player;

/*
    Part visual of the project, you need to set a protein in the scene
    Then set his parent, this serves to identify his index in the videos of the VideoChoice
    Set proteinName to be used in the json
    Set synthesizedProteinName to be used in a field in the Gameplay

    After all its used the proteinName to get the DNA string from a json that holds all the DNA strings
*/

namespace ProteinPart.InfoProtein{
    public class Protein : MonoBehaviour{

        [SerializeField] Transform maxParent = default;

        private static VideoChoice videoChoice;

        [SerializeField] ProteinDescription proteinDescription; //Using it instead of JSON

        [SerializeField] string proteinName; //Could use the name of the gameObject, used in json
        [SerializeField] string synthesizedProteinName; //Different from the protein name

        [System.Serializable]
        public class ProteinDeclaration {
            public string name;  
            public string value;
        }
        
        [System.Serializable]
        public class PD{
            public List<ProteinDeclaration> proteinValues;
        }

        public static void Setup(VideoChoice vc){
            videoChoice = vc;
        }
        
        public void OnClickSendVideo(){
            videoChoice.ChooseProtein(maxParent.GetSiblingIndex());

            CellNucleusManager.SetDNAString(proteinDescription.proteinDNA); //Sends the protein to the gameplay

            FindObjectOfType<PlayerInfo>().SetProteinName(synthesizedProteinName); 

            SynthesizingProteinShow.SetProtein(proteinDescription); //Send the info to be seen in the gameplay
        }


    }
}

