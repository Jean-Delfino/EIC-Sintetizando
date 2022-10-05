using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    Used in the marking of the game
*/

namespace GameUserInterface.Text{
    public class InfoEditableComponent : InfoComponent{
        [SerializeField] List<TextMeshProUGUI> texts = default; //All the texts we need to start 

        private List<string> saveTexts = new List<string>();

        public void Setup(List<string> textsMessage){
            saveTexts = textsMessage;
        }

        public void Setup(List<TextMeshProUGUI> markingText){
            texts = markingText;
        }

        public void SetText(int start){ //Going to be used
            int i;

            if(start + texts.Count > texts.Count){
                return;
            }

            for(i = 0; i < texts.Count; i++){
                this.texts[i + start].text = saveTexts[i];
            }
        }

        public void SetExternalTextVisible(){
            int i;

            SetText(0);

            for(i = 0; i < texts.Count; i++){
                GameObject hold = texts[i].gameObject;
                hold.SetActive(!hold.activeSelf);
            }
        }
    }
}