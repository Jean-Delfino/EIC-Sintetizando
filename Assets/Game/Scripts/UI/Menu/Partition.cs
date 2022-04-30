using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kinda unecessary, because the buttons in Unity can do this, just a reminder 

namespace Menu{
    public class Partition : MonoBehaviour{
        protected int index;
        void Start(){
            index = transform.GetSiblingIndex();
        }
        public void Resume(){
            FindObjectOfType<SelectionManager>(true).ChangePartitionByIndex(index);
        }
        
    }
}
