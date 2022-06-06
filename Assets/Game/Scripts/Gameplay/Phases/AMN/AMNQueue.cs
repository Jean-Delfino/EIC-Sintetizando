using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

using GameUserInterface.Text;

namespace PhasePart.AMN{
    public class AMNQueue : MonoBehaviour{
        private Color actualColor = new Color(0f, 0f, 0f);
        [SerializeField] Letter amnPrefab = default;
        [SerializeField] RibossomeAnimator transporterList = default;

        public async Task NewAMNInLine(bool lastOne, bool newRb, string amnNumber, string amnName){
            //print(" NUMBER = " + amnNumber);
            //print("     novo RB = " + newRb);
            //print("     ultimo = " + lastOne);

            actualColor = Util.CreateNewDifferentColor(actualColor);           

            await transporterList.RibossomeExit(lastOne, this.transform, amnPrefab.gameObject, amnName);

            if(newRb){
                await transporterList.RibossomeEnter(actualColor, amnNumber);
            }

            //PushAMN(amnName); Not needed anymore, see RibossomeExit
        }

        public async Task SetAllRibossomeEnter(int ribossomeMaxNumber){
            int i;

            transporterList.SetPool(ribossomeMaxNumber);

            for(i = 0; i < ribossomeMaxNumber - 1; i++){ //Set the max, but in the begginning only two will be spawned
                actualColor = Util.CreateNewDifferentColor(actualColor);
                await transporterList.RibossomeEnter(actualColor, (i+1).ToString());
            }

            await Task.Yield();
        }

        public void PushAMN(string amnName){ //Old
            Letter hold;

            hold = Instantiate<Letter>(amnPrefab, this.transform);
            hold.gameObject.GetComponent<Image>().color = actualColor;
            hold.Setup(amnName);
        }
    }
}
