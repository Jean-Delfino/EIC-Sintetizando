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
        private int originalQuantity; //Reference to make easy to work

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

        private string[] anwsers; //Save of the anwsers of the RNA given by the player
        private int nextPhase = 0;

        private void Start(){
            originalQuantity = quantity;

            //The amount of dna + the ending string dna
            quantity += (AMNManager.GetSizeAMN());

            anwsers = new string[quantity];
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

            string additionalString = string.Concat(Enumerable.Repeat(
                    nonUsableCharacter, CellNucleusManager.GetNumberOfCharacterToEnd()));
            
            SetInputData(RNASpawn); //Protected function of all the InputPhase manager

            InstantiateRNAUsingString(0, sub, prefab);

            //Instantiate 3 dots
            InstantiateRNAUsingString(-CellNucleusManager.GetNumberOfCharacterToEnd(), 
                additionalString, middleRNA);
            
            //Instantiate one of the ending DNA string
            InstantiateRNAUsingString(quantity,ending, prefab);

            return additionalString + ending;
        }

        public void InstantiateAllRNARandom(){
            int i;
            RNA hold;

            SetInputData(RNASpawn); //Protected function of all the InputPhase manager
      
            for(i = 0 ; i < quantity ; i++){
                hold = Instantiate<RNA>(prefab, RNASpawn);
                hold.SetPosition(i); //Puts its original position, so i can build the "replic" vector
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

        public void ResetValuesInRNA(){
            int i = 0;

            foreach(Transform child in RNASpawn){
                child.GetComponent<RNA>().SetValue("", defColor);
                SetCorrespondentValidation(i, "");
                i++;
            }
        }

        public void ChangeQuantityToNextPhase(int increace){
            nextPhase += increace;  
            EndPhase(); //To end the game when the player filled everything
            //print("Next phase = " + nextPhase);
        }

        public new void EndPhase(){
            //print("Next phase = " + nextPhase);
            //if(nextPhase == 1){ //Test only

            if(nextPhase == quantity){
                print("ENTROU AQUI END PHASE");
                //Here its change phases
                AMNManager.SetRNAtoAMNString(Util.ConvertToString(anwsers));
                base.EndPhase();
            }

            return;
        }

        public void ConfirmPhase(){ //Can be used, but don make much sense
            RNA childComp;

            foreach(Transform child in RNASpawn){
                childComp = child.GetComponent<RNA>();
                anwsers[childComp.GetOriginalPosition()] = childComp.GetValueInputText();
            }

            EndPhase();
        }

        public void FilterCorrectRNA(){ //Remove RNA already correct, put them in the right position on the vector too
            RNA childComp;
            foreach(Transform child in RNASpawn){
                childComp = child.GetComponent<RNA>();
                if(childComp.GetValueInputValidation()){
                    anwsers[childComp.GetOriginalPosition()] = childComp.GetValueInputText();
                    Destroy(child.gameObject);
                }
            }

            EndPhase();
        }

        //NEED TO BE CHANGED
        public void StartNewWaveDNAString(){ //Here we don't have the problem of "destroying" the DNA
            string sub = originalPlace.CutDNAString();
            int i = 0;

            ResetValuesInRNA();
            //print("Next phase = " + nextPhase);
            originalPlace.ChangeDNAStructure(sub);

            foreach(Transform child in RNASpawn){
                child.GetComponent<RNA>().RNASetup(sub[i].ToString());
                i++;
            }
        }

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

            //FilterRNABasedOnLetter(actualLetter);
        }

        public void StartNewWaveDontDestroy(){ //ThirdVersion, random
            ResetActualValuesInRNA();

            foreach(Transform child in RNASpawn){
                child.GetComponent<RNA>().RandomStart();
            }

            //FilterRNABasedOnLetter(actualLetter);
        } //Best performance

        public void SetCorrespondentValidation(int index, string value){
            anwsers[index] = value; 
            //This change the CellNucleusManager
            if(index > originalQuantity - 1){
                index += CellNucleusManager.GetNumberOfCharacterToEnd();
            }

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

/*
    private Dictionary<string, string[]> validation = new Dictionary<string, string[]>(){
        {"A", new string[] {"U", "T"}},
        {"T", new string[] {"A"}},
        {"C", new string[] {"G"}},
        {"G", new string[] {"C"}}
    };
    */
    /*
    public void GenerateFilters(){ //Spawn all the letter filters
        GameObject holder = Instantiate<GameObject>(letterPrefab , filterSpawn);
        holder.GetComponent<Letter>().Setup(defaultFilter);

        foreach(KeyValuePair<string, string> item in validation){
            holder = Instantiate<GameObject>(letterPrefab , filterSpawn);
            holder.GetComponent<Letter>().Setup(item.Key);
        }
    }

    public void FilterRNABasedOnLetter(string chosen){ //Separate RNA based on their Letter
        actualLetter = chosen;
        if(chosen == defaultFilter){
            foreach(Transform child in RNASpawn){
                child.gameObject.SetActive(true);
            }

            return;
        }

        foreach(Transform child in RNASpawn){
            if(child.GetComponent<RNA>().GetValueText() != chosen){
                child.gameObject.SetActive(false);
                continue;
            }
            child.gameObject.SetActive(true);
        }
    }*/

    /*
        private void Start() {
            //actualLetter = defaultFilter;

            quantity = AMNManager.GetNumberOfAMN() * AMNManager.GetSizeAMN();
            anwsers = new string[quantity];

            //GenerateFilters();
            if(!random){
                InstantiateAllRNABasedOnDNA();
                return;
            }

            InstantiateAllRNARandom();
        }*/

        //Filters that make the gameEasier

        //[SerializeField] string defaultFilter = default;
        //[SerializeField] Transform filterSpawn = default;
        //[SerializeField] GameObject letterPrefab = default;
        //private string actualLetter;
