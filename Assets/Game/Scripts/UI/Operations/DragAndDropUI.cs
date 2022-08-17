using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragAndDropUI : MonoBehaviour
{
    private RectTransform thisRect;

    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;

    [SerializeField] Vector2 superiorLimit = default;
    [SerializeField] Vector2 inferiorLimit = default;

    private void Start() {
        thisRect = transform.GetComponent<RectTransform>();
    }

    public void OnDrag(){
        float x = horizontalSpeed * Input.GetAxis("Mouse X");
        float y = verticalSpeed * Input.GetAxis("Mouse Y");

        thisRect.position += new Vector3(x * Time.deltaTime, y * Time.deltaTime, 0);
        
        thisRect.anchoredPosition = new Vector2(
            Mathf.Clamp(thisRect.anchoredPosition.x, inferiorLimit.x, superiorLimit.x),
            Mathf.Clamp(thisRect.anchoredPosition.y, inferiorLimit.y, superiorLimit.y)
        );
    }
}
