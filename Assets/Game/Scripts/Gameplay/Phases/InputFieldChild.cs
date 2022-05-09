using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using PhasePart;

/*
    It seems a little strange or useless, but it's a lot useful
    It pretty much sends to the Controller (Onwer) the Index of this child
*/
public class InputFieldChild : MonoBehaviour, IPointerClickHandler{
    public void OnPointerClick(PointerEventData eventData){
        this.transform.parent.GetComponent<TextWithInput>().SendInput();
    }
}
