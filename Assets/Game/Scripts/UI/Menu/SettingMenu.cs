using UnityEngine;
using UnityEngine.Audio;

namespace Menu{
    public class SettingMenu : MonoBehaviour{
        [SerializeField] AudioMixer mainMixer = default;
        [SerializeField] GameObject pauseMenuRef = default;
        public void ChangeFullScreen(bool screenMode){
            Screen.fullScreen = screenMode;
        }

        public void ChangeQuality(int qualityOption){
            QualitySettings.SetQualityLevel(qualityOption);
        }

        public void ChangeVolume(float volumeLevel){
            mainMixer.SetFloat("VolumeMixer" , volumeLevel);
        }

        public void ChangeToPause(){
            pauseMenuRef.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
