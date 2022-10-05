using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUserInterface.Text{
    public class InfoComponent : MonoBehaviour{
        [SerializeField] Transform childRef = null; //Where the information is
        private bool visibility = false;

        void Start(){
            if(childRef == null){
                childRef = this.transform;
            }
        }

        public void SetAllVisible(){
            visibility = !visibility;

            foreach(Transform child in childRef){
                child.gameObject.SetActive(visibility);
            }
        }

        public void SingleObjectVisibility(){
            visibility = !visibility;
            
            childRef.gameObject.SetActive(visibility);
        }
    }
}
