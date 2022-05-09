using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

/*
    Used in the gameplay by the object InfoDisplay
*/

namespace GameUserInterface.Text{
    public class InfoDisplay : MonoBehaviour{
        private List<string> messages;

        [SerializeField] TextMeshProUGUI totalMessages; //Information in the display (X/TOTAL)
        [SerializeField] TextMeshProUGUI actualMessage; //Actual message (ACTUAL/TOTAL)
        [SerializeField] TextMeshProUGUI messageContent; //Content, here we show the message

        private int actualMessageIndex;

        private void Start(){
            messages = new List<string>();
            actualMessageIndex = 0;
        }
        
        public void SetMessages(List<string> messages){
            this.messages = messages;
        }

        public void RestartPhase(List<string> messages){
            actualMessageIndex = 0;
            SetMessages(messages);

            totalMessages.text = (messages.Count).ToString();
            ShowText();
        }

        private void ShowText(){
            actualMessage.text = (actualMessageIndex + 1).ToString();

            messageContent.text = messages[actualMessageIndex];
        }
        public void IncreaceMessage(int increace){
            if(messages == null || actualMessageIndex >= messages.Count - 1){
                return;
            }

            actualMessageIndex += increace;
            ShowText();
        }
        public void DecreaceMessage(int increace){
            if(actualMessageIndex <= 0){
                return;
            }

            actualMessageIndex -= increace;
            ShowText();
        }
    }
}
