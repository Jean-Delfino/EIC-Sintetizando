using Menu.Settings;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Menu{
    public class QualityOption : MonoBehaviour{
        [SerializeField] SettingMenu sm = default;

        private TMP_Dropdown dropdown;

        void Start(){
            dropdown = this.transform.GetComponentInChildren<TMP_Dropdown>(true);
            dropdown.value = sm.GetQuality();
        }
    }
}
