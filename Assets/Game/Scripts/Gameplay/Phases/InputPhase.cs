using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhasePart{
    public class InputPhase : PhaseManagerMono{
        private int actualInput = -1;

        protected Transform inputArea = null;

        public void SetActualInput(int input){
            actualInput = input;
        }
        public IEnumerator ChangeInputField(){
            int input;

            while(actualInput == -1){
                yield return null;
            }
            
            while(true){
                input = 0;

                if(Input.GetKeyDown(KeyCode.LeftArrow)){
                    input = -1;
                }else if(Input.GetKeyDown(KeyCode.RightArrow)){
                    input = 1;
                }

                if(input != 0 && (actualInput + input < inputArea.childCount && actualInput + input > -1)){
                    //Deactive the actual, so you can activate the next one
                    //Get the input based on the index of siblings
                    inputArea.GetChild(actualInput).GetComponent<TextWithInput>().DeactivateInput(); //Deactivates the actual

                    actualInput += input;
                    inputArea.GetChild(actualInput).GetComponent<TextWithInput>().ActivateInput(); //Activates the "next"

                    yield return new WaitForSeconds(0.2f); //Input too fast, so wait for some time
                }
                yield return null;
            }
        }

        protected void SetInputArea(Transform inputArea){
            this.inputArea = inputArea;
            TextWithInput.SetOnwer(this); 
            StartCoroutine(ChangeInputField());//Easier way to start the components
        }
    }
}
