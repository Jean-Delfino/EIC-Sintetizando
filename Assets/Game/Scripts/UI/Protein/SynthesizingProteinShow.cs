using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using TMPro;

namespace ProteinPart.InfoProtein{
    public class SynthesizingProteinShow : MonoBehaviour, IPointerClickHandler{
        private static SynthesizingProtein toShow = default;

        [Space]
        [Header("Box Description of the actual synthesizing protein")]
        [Space]

        [SerializeField] TextMeshProUGUI descriptionProtein = default;

        [Space]
        [Header("Box Description of Extra")]
        [Space]

        [SerializeField] GameObject boxTextExtraDescription = default;

        [SerializeField] TextMeshProUGUI nameExtraData = default;
        [SerializeField] TextMeshProUGUI textDescriptionExtraData = default;

        private int lastExtra = -1;

        private void Start(){
            descriptionProtein.text = toShow.GetDescriptionProtein();
        }

        public static void SetProtein(SynthesizingProtein sint){
            toShow = sint;
        }

        public void ShowDescriptionExtraData(int index){
            if(index < 0 || index > toShow.GetQtdOfExtras() - 1) return;

            if(index == lastExtra && boxTextExtraDescription.activeSelf){
                lastExtra = -1;
                boxTextExtraDescription.SetActive(false);
                return;
            }

            lastExtra = index;
            boxTextExtraDescription.SetActive(true);

            nameExtraData.text = toShow.GetNameExtra(index);
            textDescriptionExtraData.text = toShow.GetDescriptionTextExtra(index);
        }
        
        public void OnPointerClick(PointerEventData eventData){
            int index = TMP_TextUtilities.FindIntersectingLink(descriptionProtein, Input.mousePosition, Camera.main);

            if(index > -1){
                ShowDescriptionExtraData(index);
            }
        }
    }
}
