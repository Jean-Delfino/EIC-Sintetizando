using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

using GameUserInterface.Text;

//Unity Lerp

namespace PhasePart.AMN{
    //Before it was part of the InputPhase sons, but its not needed anymore
    public class AMNManager : PhaseManagerMono{
        [System.Serializable]
        private class AMNDescriber{
            public AMN value; //A,G,U,C
            public Image table = null; //One quarter of the aminoacids circle
        } 

        [SerializeField] List<AMNDescriber> basics; //The "tables" and their anwsers
        [SerializeField] Letter letterPrefab;
        [SerializeField] Transform letterSpawn;

        private static int numberOfAMN = 7; 
        private int actualCompleted = 0; 

        private static int sizeAMN = 3;

        private static string RNAtoAMN; //Sub product of the RNASpawner completion
        private string nameAMN; //Change in every input
        private int indexOfRNA = 0; //RNA control of the position

        private void Start() {
            SpawnAMN();
            SetAMN();
            //SearchAMN("AUU");
            //SearchAMN("ACG");
        }

        private void SpawnAMN(){
            int i;

            for(i = 0; i < sizeAMN; i++){
                Instantiate<Letter>(letterPrefab, letterSpawn);
                //Here i don't need the set position
            }
        }

        private void SetAMN(){
            int i;
            string RNAstring = "";

            for(i = 0; i < sizeAMN; i++){
                RNAstring += RNAtoAMN[indexOfRNA];
                letterSpawn.GetChild(i).GetComponent<Letter>().Setup(RNAtoAMN[indexOfRNA].ToString());
                indexOfRNA++;
            }

            SearchAMN(RNAstring);
        }

        public static void SetRNAtoAMNString(string RNA){
            AMNManager.RNAtoAMN = RNA;
        }

        private void SearchAMN(string RNAstring){
            int i = 0;
            AMN perc;

            perc = basics.Find(x => {
                return x.value.GetValue() == RNAstring[i].ToString();
            }).value; 

            for(i = 1; i < sizeAMN; i++){

                perc = perc.GetNexts().Find(x => {
                    return Array.Find(x.GetValue().Split(','), y => {
                        return y == RNAstring[i].ToString();
                    }) != null;
                });
            }


            nameAMN = perc.GetAMN(0).GetValue().ToUpper();
            print(nameAMN);
            //ShowAMN(RNAstring); Test
        }

        public bool VerifyAMN(string AMN){
            if(AMN == nameAMN){
                actualCompleted++;
                EndPhase();

                return true;
            }

            return false;
        }

        private void ShowAMN(string phrase){
            int i = 0;

            foreach(Transform child in letterSpawn){
                child.GetComponent<Letter>().Setup(phrase[i].ToString());
                i++;
            }
        }

        public new void EndPhase(){
            if(actualCompleted == numberOfAMN){
                //Here its change phases
                print("ENTROU");
                base.EndPhase();
                return;
            }
            //Move the string to the left
            
            SetAMN();
            return;
        }

        public static int GetSizeAMN(){
            return sizeAMN;
        }

        public static int GetNumberOfAMN(){
            return numberOfAMN;
        }

    }
}

/*
    private void SetLetterAMN(){ //Not used anymore
        int i;

        actualDesc = UnityEngine.Random.Range(0, basics.Count);

        string structure = basics[actualDesc].value.GetValue();
        string[] hold;

        AMN perc = basics[actualDesc].value;

        for(i = 1; i < sizeAMN; i++){
            perc = perc.GetAMN(UnityEngine.Random.Range(0, perc.GetNextsCount()));
            hold = perc.GetValue().Split(','); //Can be more than one value
            structure += hold[UnityEngine.Random.Range(0, hold.Length)];
        }

        nameAMN = perc.GetAMN(0).GetValue(); //Aminoacid
        ShowAMN(structure);
    }
*/
