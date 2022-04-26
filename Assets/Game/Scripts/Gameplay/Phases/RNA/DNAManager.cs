using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhasePart.RNA.DNA{
    public class DNAManager : MonoBehaviour{
        private Dictionary<string, string> validationDNADNA = new Dictionary<string, string>(){
            {"A", "T"},
            {"T", "A"},
            {"C", "G"},
            {"G", "C"}
        }; //DNA to the DNA correspondence

        private string finiteDNAString; //Gets it from the RNASpawner
    }
}
