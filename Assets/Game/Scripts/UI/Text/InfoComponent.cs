using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUserInterface.Text{
    public class InfoComponent : MonoBehaviour{
        [SerializeField] GameObject childRef = null; //Where the information is
        private bool visibility = false;

        void Start(){
            if(childRef == null){
                childRef = this.gameObject;
            }
        }

        public void SingleObjectVisibility(){
            visibility = !visibility;
            childRef.SetActive(visibility);
        }
    }
}
