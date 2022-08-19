using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    Bridge between two AMN

*/

public class AMNConnector : MonoBehaviour
{
    private float originalY;

    private RectTransform thisRect;
    private RectTransform fatherRect;

    [SerializeField] Transform target;

    private void Start()
    {
        thisRect = this.transform.GetComponent<RectTransform>();
        fatherRect = this.transform.parent.GetComponent<RectTransform>();
        originalY = thisRect.sizeDelta.y;
        StartCoroutine(Look(target));
    }

    public float Connection(Transform target)
    {
        StartCoroutine(Look(target));
        return originalY - 5f;
    }

    public void StopConnection()
    {
        thisRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalY);
        StopAllCoroutines();
    }

    IEnumerator Look(Transform target)
    {
        if (target != this.transform)
        {
            RectTransform targetRect = target.GetComponent<RectTransform>();

            while (true)
            {
                LookAtFormula(targetRect);
                yield return null;
            }
        }
    }

    //https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html
    //https://forum.unity.com/threads/look-rotation-2d-equivalent.611044/
    private void LookAtFormula(RectTransform target)
    {
        Vector3 newPosition = target.position;
        Vector3 objectPosition = fatherRect.position;

        //print("TARGET" + newPosition);
        //print("POS" + objectPosition);
        //Vector2 direction = target.anchoredPosition - fatherRect.anchoredPosition;
        Vector2 direction = newPosition - objectPosition;

        thisRect.rotation = Quaternion.FromToRotation(Vector3.down, direction);

        float distance = Vector2.Distance(target.anchoredPosition, fatherRect.anchoredPosition);
    
         thisRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, distance);
     }

    // private float ChooseDistance(float value, float max, float min)
    // {
    //     return Mathf.Clamp(value, min, max);
    // }

    //https://docs.unity3d.com/ScriptReference/RectTransform.html
    //Maybe using Transform vector or localToWorldMatrix
}