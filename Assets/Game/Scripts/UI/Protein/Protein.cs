using System.Collections;
using System.Collections.Generic;
using System.IO;

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

namespace ProteinPart{
    public class Protein : MonoBehaviour{

        [SerializeField] Transform maxParent = default;

        private static VideoChoice videoChoice;
        //static string path = "/Assets/Game/Data/Proteinas.json"; 
        static string path = "Assets/Game/Data/Proteinas.json"; 

        
        //static string path; 

        [SerializeField] string proteinName; //Could use the name of the gameObject, used in json
        [SerializeField] string synthesizedProteinName; //Diferent from the protein name
        private string proteinValue; //Could use ProteinDeclaration, but them everything would be public

        [System.Serializable]
        public class ProteinDeclaration {
            public string name;  
            public string value;
        }
        
        [System.Serializable]
        public class PD{
            public List<ProteinDeclaration> proteinValues;
        }
        //Assets/Game/Data/Proteinas.json

        private void Awake() {
            //Assets/Resources/Data/Proteinas.json
            //path = Application.persistentDataPath + "/Assets/Game/Data/Proteinas.json"; 
            //path = Application.persistentDataPath + "Assets/Resources/Proteinas.json";
            //path = "D:/Conta EIC/Area de trabalho/Sintetizando/EIC-Sintetizando/Assets/Resources/Proteinas.json";
            //path = Path.Combine (Application.streamingAssetsPath, "Resources/Proteinas.json");
        }
        void Start() {
            //Debug.Log("PATH =  " + path);
            GetDNAString();
        }

        public static void Setup(VideoChoice vc){
            videoChoice = vc;
        }

        private void PathCorrection(){
            if (Application.platform == RuntimePlatform.WebGLPlayer){
                //Change the path string
            }
        }
        
        public void OnClickSendVideo(){
            videoChoice.ChooseProtein(maxParent.GetSiblingIndex());
            //RNASpawner.SetDNAString(proteinValue);
            CellNucleusManager.SetDNAString(proteinValue); //Sends the protein to the gameplay

            FindObjectOfType<PlayerInfo>().SetProteinName(synthesizedProteinName);
        }

        //Get all the protein
        //Choose only the needed
        //Could this part be in a singleton
        public void GetDNAString(){
            //string content = File.ReadAllText(Application.persistentDataPath + path);
            string content = File.ReadAllText(path);


            PD myPD = JsonUtility.FromJson<PD>(content);
            
            proteinValue = (myPD.proteinValues.Find(x => {
                return x.name == proteinName;
            }).value); 
        }
    }
}
