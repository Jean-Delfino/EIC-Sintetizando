using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace PhasePart.RNA{
    public class RNA : TextWithInput{ 
        [SerializeField] Image lightConfirm = default; 
        private bool valueInput = false;

        void Start(){
            //Adds a listener to the main input field and invokes a method when the value changes.
            GetMainInputField().onValueChanged.AddListener(delegate {ValueChangeCheck(); });
        }

        private void GenerateText(){
            RNASpawner RNAonwer = (RNASpawner)owner;

            int value = UnityEngine.Random.Range(0 , RNAonwer.GetValidationCount());
            valueInput = false;
            Setup(RNAonwer.GetKeyByIndex(value)); //Inheritance from Letter
        }

        public void RandomStart(){
            GenerateText();
        }

        public void RNASetup(string text){
            valueInput = false;
            Setup(text);
        }

        // Invoked when the value of the text field changes.
        public void ValueChangeCheck(){
            RNASpawner RNAonwer = (RNASpawner)owner;
            print(this.transform.gameObject.name + "ENTERED VALUE CHANGE CHECK");

            if(GetValueInputText() == "") {
                SetColor(RNAonwer.GetColorDef());
                RNAonwer.ChangeQuantityToNextPhase(Convert.ToInt32(valueInput) * -1);
                valueInput = false;
                return;
            }
            
            string val = GetValueInputText().ToUpper(); //Easier to work
            RNAonwer.SetCorrespondentValidation(originalPosition, val); //In the anwser of original position puts val

            //Validates the input with the RNA
            if(RNAonwer.GetValueValidation(GetValue()) == val){
                RNAonwer.ChangeQuantityToNextPhase(Convert.ToInt32(!valueInput)); //0 or 1, 0 if already true
                valueInput = true; //Now its true, so if it's wrong it gonna put -1 and if right it's gonna put 0

                SetValue(val, RNAonwer.GetColorRight());
                return;
            }
            //0 or -1
            //-1 if to true to false, or to empty
            RNAonwer.ChangeQuantityToNextPhase(Convert.ToInt32(valueInput) * -1); //If the player change the string

            valueInput = false; //Now false, so if it's wrong it gonna put 0 and if right it's gonna put 1

            SetValue(val, RNAonwer.GetColorWrong());
        }

        public void SetValue(string valuePas, Color valueColor){
            SetValueInputText(valuePas); //Sets the input text to UPPER CASE

            lightConfirm.color = valueColor;
        }

        public bool GetValueInputValidation(){
            return this.valueInput;
        }

        public string GetValueText(){
            return this.GetValue();
        }

        public new int GetOriginalPosition(){
            return this.originalPosition;
        }
        
        public void SetColor(Color valueColor){
            lightConfirm.color = valueColor;
        }


    }
}


    /*
    public void SetValue(string value, Color? valueColor){
        mainInputField.text = value;

        lightConfirm.color = valueColor ?? defColor;
    }*/
