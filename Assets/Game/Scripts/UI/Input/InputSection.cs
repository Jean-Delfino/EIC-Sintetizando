using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class InputSection : MonoBehaviour{
    [SerializeField] UnityEvent submit;

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            submit.Invoke();
        }
    }
    
}
