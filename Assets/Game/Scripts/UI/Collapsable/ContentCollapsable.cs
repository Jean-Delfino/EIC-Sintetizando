using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUserInterface.Collapsable{
    public class ContentCollapsable : MonoBehaviour{
        public void ContentOmission(){
            this.gameObject.SetActive(false);
        }
    }
}
