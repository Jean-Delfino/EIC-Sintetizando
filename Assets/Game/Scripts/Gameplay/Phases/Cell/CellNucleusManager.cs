using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PhasePart.AMN;
using PhasePart.RNA.DNA;

/*
    Script responsible for all the actions in the nucleus, as: spawn of the RNA, animation of the RNA
    to the RNASpawner, animation of nucleus expansion, animation of rna going through a hole, 
    cell nucleus contraction

    In the CellNucleus there is RNA, but they are fake, just Letters, because i don't need the entire script
    This makes everything easier

    The cellNucleus is a refatoration of the code present in the RNASpawner
*/

namespace PhasePart.RNA{
    public class CellNucleusManager : PhaseManagerMono{
        //[SerializeField] GameObject visualDNA = default; //Prototype
        [SerializeField] RNASpawner rnaReference; //Sets the RNA and the RNA sets it
        [SerializeField] DNAManager dnaReference; // The original DNA

        private const string DNAtranscriptionBeg = "TAC"; //Always the beg of the DNA
        private string[] DNAtranscriptionEnd = {"ATT", "ATC", "ACT"}; //The end of the DNA

        private static int numberOfCharacterToEnd = 3;

        private static string DNAString; //Original DNA string of the protein

        private static bool random = false; //Sets if the start is a random protein or not

        private int quantity;

        private void Start(){
            quantity = AMNManager.GetNumberOfAMN() * AMNManager.GetSizeAMN();

            rnaReference.SetQuantity(quantity); //Easier to work with

            if(random){
                rnaReference.InstantiateAllRNARandom();
            }
            
            //dnaReference.TurnDNAOn(); //Turn visible all the DNA structure, visible now

            //Separate the DNA and Expand the nucleus 
            DNAAnimations();
        }

        private async void DNAAnimations(){
            string firstCut = CutDNAString();
            string nonUsableCharacter = rnaReference.GetNonUsableCharacter();
            int i;

            dnaReference.SetFiniteDNAString(firstCut); //Puts cutted DNA on DNA
            dnaReference.SetupStructure(quantity, firstCut); //Instantiate everything need in the visual DNAPart

            //The ending of the DNA that its used in the RNASpawner
            string additional = rnaReference.InstantiateAllRNABasedOnDNA(firstCut); //Just to set it first
            dnaReference.SetupStructure(additional.Length, additional);
            
            for(i = 0; i < numberOfCharacterToEnd; i++){ //Dots
                ChangeRNAinDNAStructure(quantity + i, nonUsableCharacter);
                ChangeDNAinDNAStructure(quantity + i, nonUsableCharacter);
            }

            SetSeparationInDNA(nonUsableCharacter, quantity);
            dnaReference.ChangeSecondHalf(additional.Substring(numberOfCharacterToEnd)); //Complementar DNA
            
            await dnaReference.RNAVisibility(); //RNA visible
            await dnaReference.DNASeparation(); 

            await dnaReference.DNANucleusVisibility(true);

            EndPhase();
        }

        private void SetSeparationInDNA(string separationString, int firstQuantity){
            dnaReference.SetSeparationInDNAStructure(numberOfCharacterToEnd, separationString, firstQuantity);
        }


        public static int GetNumberOfCharacterToEnd(){
            return numberOfCharacterToEnd;
        }

        public void ChangeDNAStructure(string cut){
            dnaReference.ChangeFirstHalf(cut);
        }

        public void ChangeRNAinDNAStructure(int index, string text){
            dnaReference.ChangeRNAinDNAStructure(index, text);
        }

        public void ChangeDNAinDNAStructure(int index, string text){
            dnaReference.ChangeSecondHalf(index, text);
        }

        //Need to do all the animation of the game
        public new void EndPhase(){
            base.EndPhase();
        }

        public string CutDNAString(){
            string sub;

            do{
                do{
                    //Cuts a part of the DNA to make the substring
                    sub = Util.RandomSubString(DNAString, quantity, 0, (DNAString.Length - quantity));
                }while(Util.FindOcorrence(sub, DNAtranscriptionEnd, AMNManager.GetSizeAMN()));
            }while(!DNAWithAllBases(sub, rnaReference.GetDictionaryKeysCount())); 
            //Tests if it have at least one of all the bases (A,T,C,G)

            return sub;
        }

        public string GetAEndingString(){
            return DNAtranscriptionEnd[UnityEngine.Random.Range(0, DNAtranscriptionBeg.Length)];
        }

        private bool DNAWithAllBases(string cut, int number){
            int i;
            List<char> bases = new List<char>();

            for(i = 0; i < cut.Length; i++){
                if(!bases.Contains(cut[i])){
                    bases.Add(cut[i]);
                    if(bases.Count == number){
                        return true;
                    }
                }
            }

            return false;
        }

        public void SetRandom(bool state){
            random = state;
        }
        public static void SetDNAString(string proteinDNA){
            DNAString = proteinDNA;
        }
    }
}
