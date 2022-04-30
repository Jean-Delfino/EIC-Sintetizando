using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUserInterface.Text{
    public class InfoComponent : MonoBehaviour{
        [SerializeField] Transform childRef = null;
        private bool visibility = false;

        void Start(){
            if(childRef == null){
                childRef = this.transform;
            }
        }

        public void OnMouseInfoVisibility(){ //this is also mouse exit
            visibility = !visibility;
            foreach(Transform child in childRef){
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
    }
}
