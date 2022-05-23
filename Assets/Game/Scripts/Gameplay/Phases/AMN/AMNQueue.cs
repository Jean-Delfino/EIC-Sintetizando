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

        public async Task NewAMNInLine(bool newRb, string amnName){
            actualColor = Util.CreateNewDifferentColor(actualColor);           

            await transporterList.RibossomeExit(this.transform, amnPrefab.gameObject);

            if(newRb){
                await transporterList.RibossomeEnter(actualColor, amnName);
            }

            //PushAMN(amnName); Not needed anymore, see RibossomeExit
        }

        public async Task SetAllRibossomeEnter(int ribossomeMaxNumber){
            int i;

            transporterList.SetPool(ribossomeMaxNumber);

            for(i = 0; i < ribossomeMaxNumber; i++){
                actualColor = Util.CreateNewDifferentColor(actualColor);
                await transporterList.RibossomeEnter(actualColor, (i+1).ToString());
            }

            await Task.Yield();
        }

        public void PushAMN(string amnName){
            Letter hold;

            hold = Instantiate<Letter>(amnPrefab, this.transform);
            hold.gameObject.GetComponent<Image>().color = actualColor;
            hold.Setup(amnName);
        }
    }
}
