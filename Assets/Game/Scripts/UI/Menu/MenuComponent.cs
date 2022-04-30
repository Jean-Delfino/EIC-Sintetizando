using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu{
    public class MenuComponent : Partition{
        public void OnPointerEnter() {
            FindObjectOfType<SelectionManager>().PositioningBoltByIndex(index);   
        }
        public void DoOperation(){
            base.Resume();
        }

        public void Quit() {
            FindObjectOfType<SelectionManager>().Quit(); 
        }
    }
}
