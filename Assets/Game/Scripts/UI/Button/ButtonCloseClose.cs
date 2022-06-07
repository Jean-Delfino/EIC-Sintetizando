using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameUserInterface.Button{
    /*
        The main idea of this script is to open X things and close Y things

        But in can be used in N diferrent ways
    */
    public class ButtonCloseClose : MonoBehaviour{
        //Idea from Thiago Bandeira Testing

        [SerializeField] UnityEvent click;
        [SerializeField] UnityEvent afterClick;

        bool clicked = true; //See the beginning of the OnClickButton, it turn to be false actually

        public void OnClickButton(){
            clicked = !clicked;
            
            if(clicked){
                afterClick.Invoke();
                return;
            }

            click.Invoke();
        }

    }
}
