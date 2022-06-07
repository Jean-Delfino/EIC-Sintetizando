using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using TMPro;

/*
    Gets a list of UnityEvents and for each it give the input of the TMProInput
    The field or the function of the UnityEvent are unknow

    Used for SetNamePlayer
*/

namespace GameUserInterface.Input{
    public class InputSend : MonoBehaviour{
        [SerializeField] TMP_InputField thisInput;
        [SerializeField] UnityEvent m_MyEvent;

        [SerializeField] UnityEvent submitDependence;
        [SerializeField] UnityEvent changeValueDependence;
        [SerializeField] UnityEvent noTextAnwser;

        void Start(){

            if(changeValueDependence != null){
                thisInput.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
            }

        }

        public void SendSubmitToObject(){
            if(m_MyEvent == null) return;
            
            if(thisInput.text.Length == 0){
                noTextAnwser.Invoke();
                return;
            }  

            //print("texto = " + this.thisInput.text + " " + thisInput.text.Length);

            Util.UnityEventInvokeAllListenersTheSame
                (m_MyEvent, new object[] {this.thisInput.text}, new Type [] {typeof(string)});

            submitDependence.Invoke();
        }

        private void ValueChangeCheck(){
            if(thisInput.text.Length == 0){
                noTextAnwser.Invoke();
                return;
            }

            changeValueDependence.Invoke();
        }
    }
}
