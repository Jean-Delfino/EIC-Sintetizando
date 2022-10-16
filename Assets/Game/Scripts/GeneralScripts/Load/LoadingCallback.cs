using UnityEngine;

namespace GameSceneManagement{
    public class LoadingCallback : MonoBehaviour{
        private bool isFirstUpdate = true;

        private void Update(){
            if(isFirstUpdate){
                isFirstUpdate = false;
                Loader.LoaderCallback();
            }
        }
    }
}
