using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TMPro;
using System;

/*
    Gets a list of UnityEvents and for each it give the input of the TMProInput
    The field or the function of the UnityEvent are unknow

    Used for SetNamePlayer
*/

namespace GameUserInterface.Input{
    public class InputSend : MonoBehaviour{
        [SerializeField] TMP_InputField thisInput;
        [SerializeField] UnityEvent m_MyEvent;
        [SerializeField] UnityEvent dependence;
        //void Start(){}

        public void SendInputToObject(){
            if(m_MyEvent == null) return;
            if(thisInput.text.Length == 0) return;  

            //print("texto = " + this.thisInput.text + " " + thisInput.text.Length);

            Util.UnityEventInvokeAllListenersTheSame
                (m_MyEvent, new object[] {this.thisInput.text}, new Type [] {typeof(string)});

            dependence.Invoke();
        }
    }
}
