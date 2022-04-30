using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PhasePart.AMN{
    public class AMNInputField : MonoBehaviour{
        [SerializeField] AMNManager amnM;
        private TMP_InputField thisInput;
        private void Start() {
        thisInput = this.GetComponent<TMP_InputField>(); 
        thisInput.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
        }
        private void OnSubmit(){ //When there is enough characters
            print("That it");
            if(amnM.VerifyAMN(thisInput.text)){
                thisInput.text = "";
            }
            //Error ? Change the score ?
        }

        private void ValueChangeCheck(){
            thisInput.text = thisInput.text.ToUpper();

            if(thisInput.text.Length == AMNManager.GetSizeAMN()){
                OnSubmit();
            }
        }
    }
}