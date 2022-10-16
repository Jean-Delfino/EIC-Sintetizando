using UnityEngine;
using TMPro;

namespace PhasePart.AMN{
    public class AMNInputField : MonoBehaviour{
        [SerializeField] AMNManager amnM;
        private TMP_InputField thisInput;

        private bool wait = false;

        private void Start() {
            thisInput = this.GetComponent<TMP_InputField>(); 
            thisInput.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
        }
        
        private async void OnSubmit(){ //When there is enough characters
            //print("That it");
            print(wait + " O K " + thisInput.text);
            if(!wait && amnM.VerifyAMN(thisInput.text)){
                string auxTextAMN = thisInput.text;
                thisInput.text = "";

                wait = true;
                await amnM.PushNewAMN(auxTextAMN);
                wait = false;

                ValueChangeCheck();
            }
        }

        private void ValueChangeCheck(){
            if(thisInput.text.Length == 0){
                return;
            }

            thisInput.text = FormatText();

            if(thisInput.text.Length == AMNManager.GetSizeAMN()){
                OnSubmit();
            }
        }

        private string FormatText(){
            if(thisInput.text.Length == 1){
                return thisInput.text.ToUpper();
            }

            return thisInput.text[0].ToString().ToUpper() + thisInput.text.Substring(1).ToLower(); 
        }
    }
}