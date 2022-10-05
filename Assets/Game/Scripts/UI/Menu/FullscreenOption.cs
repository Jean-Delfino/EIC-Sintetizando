using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace Menu.Settings{
    public class FullscreenOption : MonoBehaviour{
        [SerializeField] SettingMenu sm = default;

        [SerializeField] Button onOption = default;
        [SerializeField] Button offOption = default;

        private void Start() {
            if(sm.GetFullScreen()){
                offOption.onClick.Invoke();
                return;
            }

            onOption.onClick.Invoke();
        }
    }
}
