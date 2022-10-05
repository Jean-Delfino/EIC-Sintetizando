using UnityEngine;

namespace Menu{
    public class PauseMenu : MonoBehaviour{
        private static bool isPaused = false;

        [SerializeField] GameObject optionMenuRef = default;

        public void PauseGame(){
            if(!isPaused){
                this.gameObject.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
            }
        }

        public void ResumeGame(){
            if(isPaused){
                this.gameObject.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
            }
        }

        public void OptionMenu(){
            this.gameObject.SetActive(false);
            optionMenuRef.SetActive(true);
        }

        public void QuitGame(){
            Application.Quit();
        }

        public bool GetIsPaused(){
            return isPaused;
        }
    }
}
