using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kinda unecessary, because the buttons in Unity can do this, just a reminder 

namespace Menu{
    public class Partition : MonoBehaviour{
        protected int index;
        protected SelectionManager selection;

        void Start(){
            index = transform.GetSiblingIndex();

            selection = this.transform.parent.GetComponent<SelectionManager>();

            if(selection == null){
                Debug.LogWarning("SELECTION MANAGER NULL");
            }
        }
        
        public void Resume(){
            selection.ChangePartitionByIndex(index);
        }
        
    }
}
