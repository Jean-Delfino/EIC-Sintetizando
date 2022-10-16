using UnityEngine;
using UnityEngine.Audio;

namespace Game.Scripts.UI.Menu{
    public class SettingMenu : MonoBehaviour{
        [SerializeField] AudioMixer mainMixer = default;
        [SerializeField] GameObject pauseMenuRef = default;   

        private float actualVolume;

        private void Awake() {
            mainMixer.GetFloat("VolumeMixer", out actualVolume);
        }     
        
        public void ChangeFullScreen(bool screenMode){
            Screen.fullScreen = screenMode;
        }

        public void ChangeQuality(int qualityOption){
            QualitySettings.SetQualityLevel(qualityOption);
        }

        public int GetQuality(){
            return QualitySettings.GetQualityLevel(); 
        }

        public void ChangeVolume(float volumeLevel){
            mainMixer.SetFloat("VolumeMixer" , volumeLevel);
            actualVolume = volumeLevel;
        }

        public float GetVolume(){
            return actualVolume;
        }

        public bool GetFullScreen(){
            return Screen.fullScreen;
        }

        //This function will be called in the end of the drag of a slider
        //Or in a button
        public void ReleaseBasicAudioToConfirmation(){
            //Sound when this function is called
        }

        public void ChangeToPause(){
            pauseMenuRef.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
