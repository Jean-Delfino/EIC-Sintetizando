using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Menu{
    public class TextShow : MonoBehaviour{
        [SerializeField] private TextMeshProUGUI title = default;
        [SerializeField] private TextMeshProUGUI description = default;
        private bool activeObject = false;
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
