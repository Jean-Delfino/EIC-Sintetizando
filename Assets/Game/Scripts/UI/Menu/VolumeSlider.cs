using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
namespace Menu.Settings{
    public class VolumeSlider : MonoBehaviour{
        [SerializeField] SettingMenu sm = default;

        private Slider slider;

        void Start(){
            slider = this.transform.GetComponent<Slider>();
            slider.value = sm.GetVolume();
        }
    }
}
