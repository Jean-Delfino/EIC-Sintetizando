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
        [SerializeField] DNAStructureWithRNA dnaSetupReference; //Visual part of the DNA

        const string DNAtranscriptionBeg = "TAC"; //Always the beg of the DNA
        string[] DNAtranscriptionEnd = {"ATT", "ATC", "ACT"}; //The end of the DNA
        static string DNAString; //Original DNA string of the protein

        static bool random = false; //Sets if the start is a random protein or not

        private int quantity;
        private void Start(){
            quantity = AMNManager.GetNumberOfAMN() * AMNManager.GetSizeAMN();

            rnaReference.SetQuantity(quantity); //Easier to work with

            if(random){
                rnaReference.InstantiateAllRNARandom();
            }

            string firstCut = CutDNAString();

            rnaReference.InstantiateAllRNABasedOnDNA(firstCut); //Just to set it first
            dnaSetupReference.SetupStructure(quantity, firstCut); //Instantiate everything need in the visual DNAPart
            EndPhase();
        }

        public void ChangeDNAStructure(string cut){
            dnaSetupReference.ChangeAllFirstHalf(cut);
        }

        public void ChangeRNAinDNAStructure(int index, string text){
            dnaSetupReference.ChangeRNAinDNAStructure(index, text);
        }

        //Need to do all the animation of the game
        public new void EndPhase(){
            base.EndPhase();
        }

        public string CutDNAString(){
            string sub;

            do{
                //Cuts a part of the DNA to make the substring
                sub = Util.RandomSubString(DNAString, quantity, 0, (DNAString.Length - quantity));
                print("sub : " + sub);
            }while(Util.FindOcorrence(sub, DNAtranscriptionEnd, AMNManager.GetSizeAMN()));

            return sub;
        }

        public void SetRandom(bool state){
            random = state;
        }
        public static void SetDNAString(string proteinDNA){
            DNAString = proteinDNA;
        }

        
    }
}
