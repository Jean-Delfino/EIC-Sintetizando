using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    Bridge between two AMN

*/

public class AMNConnector : MonoBehaviour{
    private float amnLetterSize;
    private float originalY;

    private RectTransform thisRect;

    private void Start(){
        amnLetterSize = this.transform.parent.GetComponent<RectTransform>().sizeDelta.y;
        thisRect = this.transform.GetComponent<RectTransform>();
        originalY = thisRect.sizeDelta.y;
    }

    public float Connection(Transform target){
        StartCoroutine(Look(target));
        return originalY - 5f;
    }

    public void StopConnection(){
        thisRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalY);
        StopAllCoroutines();
    }

    IEnumerator Look(Transform target){
        if(target != this.transform){
            RectTransform targetRect = target.GetComponent<RectTransform>();

            float distance = Vector2.Distance(targetRect.anchoredPosition, thisRect.anchoredPosition);
            float hipotenuse = Mathf.Sqrt((amnLetterSize * amnLetterSize) + (distance * distance));

            while(true){
                print("DISTANCE = " + distance);

                distance = ChangeDistance(distance, hipotenuse, originalY - 5f);
                thisRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, distance);

                LookAtFormula(targetRect);

                distance = Vector2.Distance(targetRect.anchoredPosition, thisRect.anchoredPosition);

                yield return null;
            }
        }
    }

    //https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html
    //https://forum.unity.com/threads/look-rotation-2d-equivalent.611044/
    private void LookAtFormula(RectTransform target){
       thisRect.right = target.anchoredPosition - thisRect.anchoredPosition;
    }

    private float ChangeDistance(float value, float max, float min){
        return Mathf.Clamp(value, min, max);
    }

    //https://docs.unity3d.com/ScriptReference/RectTransform.html
    //Maybe using Transform vector or localToWorldMatrix
}

/*
Vector3 myLocation = thisRect.anchoredPosition;
        Vector3 targetLocation = target.anchoredPosition;

        targetLocation.z = myLocation.z; // ensure there is no 3D rotation by aligning Z position
        
        // vector from this object towards the target location
        Vector3 vectorToTarget = targetLocation - myLocation;
        // rotate that vector by 90 degrees around the Z axis
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        
        // get the rotation that points the Z axis forward, and the Y axis 90 degrees away from the target
        // (resulting in the X axis facing the target)
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        
        // changed this from a lerp to a RotateTowards because you were supplying a "speed" not an interpolation value
        thisRect.rotation = Quaternion.RotateTowards(thisRect.rotation, targetRotation, turnSpeed * Time.deltaTime);
        thisRect.Translate(Vector3.right * turnSpeed * Time.deltaTime, Space.Self);


*/
