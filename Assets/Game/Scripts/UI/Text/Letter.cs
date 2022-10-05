using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

/*Pretty much just a text component*/

namespace GameUserInterface.Text{
    public class Letter : MonoBehaviour{
        [SerializeField] TextMeshProUGUI letterText = null;
        public void Setup(string value){
            if(letterText == null){
                this.transform.GetComponentInChildren<TextMeshProUGUI>().text = value;
                return;
            }

            letterText.text = value;
        }

        public string GetValue(){
            if(letterText == null){
                return this.transform.GetComponentInChildren<TextMeshProUGUI>().text;
            }

            return letterText.text;
        }
    }
}
