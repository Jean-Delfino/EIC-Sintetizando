using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GameUserInterface.Text{
    public class TextShow : MonoBehaviour{
        // Start is called before the first frame update
        
        [SerializeField] TextMeshProUGUI title = default;
        [SerializeField] TextMeshProUGUI description = default;
        bool activeObject = false;
        public void Setup(string title, string description){
            this.title.text = title;
            this.description.text = description;
        }
        public void ShowText(string title,string description){
            if(activeObject == true) return;
            Setup(title, description);
        
            activeObject = true;
            ChangeVisibility(activeObject);
        }
        public void UnShowText(){
            if(activeObject == false) return;
            activeObject = false;
            ChangeVisibility(activeObject);
        }
        public void ChangeVisibility(bool state){
            this.gameObject.SetActive(state);
        }
    }
}
