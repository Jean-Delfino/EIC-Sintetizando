using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using PhasePart;
public class InputFieldChild : MonoBehaviour, IPointerClickHandler{
    public void OnPointerClick(PointerEventData eventData){
        this.transform.parent.GetComponent<TextWithInput>().SendInput();
    }
}
