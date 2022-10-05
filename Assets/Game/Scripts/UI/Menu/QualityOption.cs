using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace Menu.Settings{
    public class QualityOption : MonoBehaviour{
        [SerializeField] SettingMenu sm = default;

        private TMP_Dropdown dropdown;

        void Start(){
            dropdown = this.transform.GetComponentInChildren<TMP_Dropdown>(true);
            dropdown.value = sm.GetQuality();
        }
    }
}
