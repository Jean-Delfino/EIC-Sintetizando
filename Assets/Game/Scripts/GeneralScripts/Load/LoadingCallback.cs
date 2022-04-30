using System.Collections;
using System.Collections.Generic;
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
