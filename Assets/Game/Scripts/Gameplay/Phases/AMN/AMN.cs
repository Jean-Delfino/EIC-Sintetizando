using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhasePart.AMN{
    [System.Serializable]
    public class AMN{
        [SerializeField] string value;
        [SerializeField] List<AMN> nexts;

        public string GetValue(){
            return value;
        }
        public AMN GetAMN(int index){
            return nexts[index];
        }
        public int GetNextsCount(){
            return nexts.Count;
        }

        public List<AMN> GetNexts(){
            return this.nexts;
        }
    }
}
