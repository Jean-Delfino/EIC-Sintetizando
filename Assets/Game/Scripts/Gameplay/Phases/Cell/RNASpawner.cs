using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

using PhasePart.AMN;

/*
    Spawn all the RNA, based on the original DNA string of the protein
    Has a reference to the NucleusManager because the Nucleus Manager should be the original
    Every visual change in the RNA will be reflect in the Nucleus

    Before string
        sub = DNAtranscriptionBeg + DNA + DNAtranscriptionEnd[Random.Range(0 , DNAtranscriptionEnd.Length)];

    Now
        sub = DNA + DNAtranscriptionEnd[Random.Range(0 , DNAtranscriptionEnd.Length)];
*/

namespace PhasePart.RNA{
    public class RNASpawner : InputPhase{
        [Space]
        [Header("RNA Manager Atributes")]
        [Space]
        [SerializeField] CellNucleusManager originalPlace; //Where the RNA starts (the nucleus)

        private int quantity; //Needs to be multiple of 3, and it will because of the AMN

        [SerializeField] RNA prefab = default;
        [SerializeField] TextWithInput middleRNA = default;
        [SerializeField] string nonUsableCharacter = "-";

        [SerializeField] Transform RNASpawn = default;
        
        [Space]
        [Header("Colors used in the lights")]
        [Space]
        //Color for the player input
        [SerializeField] Color defColor = default;
        [SerializeField] Color whenRight = default;
        [SerializeField] Color whenWrong = default;
        private Dictionary<string, string> validationDNARNA = new Dictionary<string, string>(){
            {"A", "U"},
            {"T", "A"},
            {"C", "G"},
            {"G", "C"}
        }; //DNA to RNA Correspondence

        private string[] answers; //Save of the answers of the RNA given by the player
        private int nextPhase = 0;

        private void Start(){
            answers = new string[quantity];
            SetInputOperation();
        }

        private void InstantiateRNAUsingString(int actualNumber, string dataString,TextWithInput prefabT){
            int i;
            TextWithInput hold;

            for(i = 0 ; i < dataString.Length ; i++){
                hold = Instantiate<TextWithInput>(prefabT, RNASpawn);
                hold.SetPosition(actualNumber + i); //Puts its original position, so i can build the "replic" vector
                hold.Setup(dataString[i].ToString());
            }
        }

        private string InstantiateAllRNABasedOnDNA(){
            return InstantiateAllRNABasedOnDNA(originalPlace.CutDNAString());
        }

        //Get the additional information
        public string InstantiateAllRNABasedOnDNA(string sub){
            string ending = originalPlace.GetAEndingString();
            string beginning = originalPlace.GetBeginningString();

            string separationString = string.Concat(Enumerable.Repeat(
                    nonUsableCharacter, CellNucleusManager.GetNumberOfCharacterToEnd()));
            
            SetInputData(RNASpawn); //Protected function of all the InputPhase manager

            InstantiateRNAUsingString(0, beginning, prefab);

            //Instantiate beginning dot
            InstantiateRNAUsingString(-CellNucleusManager.GetNumberOfCharacterToEnd(), 
                separationString, middleRNA);

            InstantiateRNAUsingString(beginning.Length, sub, prefab);

            //Instantiate end dot
            InstantiateRNAUsingString(-CellNucleusManager.GetNumberOfCharacterToEnd(), 
                separationString, middleRNA);
            
            //Instantiate one of the ending DNA string
            InstantiateRNAUsingString(quantity + beginning.Length,ending, prefab);

            return ending;
        }

        public void InstantiateAllRNARandom(){
            int i;
            RNA hold;

            SetInputData(RNASpawn); //Protected function of all the InputPhase manager
      
            for(i = 0 ; i < quantity ; i++){
                hold = Instantiate<RNA>(prefab, RNASpawn);
                hold.SetPosition(i); //Puts its original position, so i can build the "replica" vector
                hold.RandomStart();
            }
        }

        private void DestroyAllRNA(){
            foreach(Transform child in RNASpawn){
                Destroy(child.gameObject);
            }
        }

        public void ResetActualValuesInRNA(){
            RNA hold;
            foreach(Transform child in RNASpawn){
                hold = child.GetComponent<RNA>();
                if(GetValueValidation(hold.GetValue()) == hold.GetValueInputText()){
                    hold.SetValue("", defColor);
                    nextPhase--;
                }
            }
        }

        // public void ResetValuesInRNA(){
        //     int i = 0;
        //     int qtdNonUsable = CellNucleusManager.GetNumberOfCharacterToEnd();

        //     for(i = 0; i < originalQuantity; i++){
        //         RNASpawn.GetChild(i).GetComponent<RNA>().SetValue("", defColor);
        //         SetCorrespondentValidation(i, "");
        //     }

        //     for(i = i + qtdNonUsable; i < RNASpawn.childCount; i++){
        //         RNASpawn.GetChild(i).GetComponent<RNA>().SetValue("", defColor);
        //         SetCorrespondentValidation(i-qtdNonUsable, "");
        //     }
        // }

        public void ChangeQuantityToNextPhase(int increase){
            nextPhase += increase;  
            EndPhase(); //To end the game when the player filled everything
        }

        public new void EndPhase(){
            if(nextPhase == quantity){
                //Here its change phases
                AMNManager.SetRNAtoAMNString(Util.ConvertToString(answers));
                base.EndPhase();
            }

            return;
        }

        public void ConfirmPhase(){ //Can be used, but don make much sense
            RNA childComp;

            foreach(Transform child in RNASpawn){
                childComp = child.GetComponent<RNA>();
                answers[childComp.GetOriginalPosition()] = childComp.GetValueInputText();
            }

            EndPhase();
        }

        // public void StartNewWaveDNAString(){ //Here we don't have the problem of "destroying" the DNA
        //     string sub = originalPlace.CutDNAString();
        //     string ending = originalPlace.GetAEndingString();

        //     int qtdNonUsable = CellNucleusManager.GetNumberOfCharacterToEnd();

        //     sub += ending;

        //     int i = 0;

        //     ResetValuesInRNA();
        //     originalPlace.ChangeDNAStructure(sub + ending);

        //     for(i = 0; i < originalQuantity; i++){
        //         RNASpawn.GetChild(i).GetComponent<RNA>().RNASetup(sub[i].ToString());
        //     }

        //     for(i = i + qtdNonUsable; i < RNASpawn.childCount; i++){
        //         RNASpawn.GetChild(i).GetComponent<RNA>().RNASetup(sub[i - qtdNonUsable].ToString());
        //     }
        // }

        public void StartNewWave(){ //First version
            nextPhase = 0;
            DestroyAllRNA();
            InstantiateAllRNARandom();
        }

        public void StartNewWaveByActualSize(){ //SecondVersion
            ResetActualValuesInRNA();

            int childCnt = RNASpawn.childCount; //Necessary reference
            
            DestroyAllRNA();

            for(; childCnt > 0 ; childCnt--){
                Instantiate<RNA>(prefab, RNASpawn);
            }

        }

        public void StartNewWaveDontDestroy(){ //ThirdVersion, random
            ResetActualValuesInRNA();

            foreach(Transform child in RNASpawn){
                child.GetComponent<RNA>().RandomStart();
            }

            //FilterRNABasedOnLetter(actualLetter);
        } //Best performance

        public void SetCorrespondentValidation(int index, string value){
            answers[index] = value; //Look RNA original position

            originalPlace.ChangeRNAinDNAStructure(index, value);
        }


        public void SetQuantity(int quantity){
            this.quantity = quantity;
        }
        public Color GetColorDef(){
            return defColor;
        }
        public Color GetColorRight(){
            return whenRight;
        }
        public Color GetColorWrong(){
            return whenWrong;
        }
        public string GetValueValidation(string keyPas){
            return validationDNARNA[keyPas];
        }
        public int GetValidationCount(){
            return validationDNARNA.Count;
        }       
        public string GetKeyByIndex(int index){
            return validationDNARNA.Keys.ElementAt(index);
        }

        public string[] GetDictionaryKeys(){
            return validationDNARNA.Keys.ToArray();
        }

        public int GetDictionaryKeysCount(){
            return validationDNARNA.Keys.Count;
        }

        public string GetNonUsableCharacter(){
            return this.nonUsableCharacter;
        }

    }
}