using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Menu{
    public class VolumeSlider : MonoBehaviour{
        [SerializeField] SettingMenu sm = default;

        private Slider slider;

        void Start(){
            slider = this.transform.GetComponent<Slider>();
            slider.value = sm.GetVolume();
        }
    }
}
