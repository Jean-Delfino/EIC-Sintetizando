using UnityEngine;
using UnityEngine.Events;

/*Name is self explanatory*/

using GameGeneralScripts.Reflection;

namespace GameGeneralScripts.Player{
    public class PlayerInfo : GeneralGetter{

        private string actualProtein;
        private string namePlayer;
        private float maxScore;
        private float lastScore;
        private float actualScore;

        [SerializeField] UnityEvent onAwakeEvents;


        //private string textTeste = "Al";

        //private float maxTime;
        //private float actualBestTime;
        public static PlayerInfo Instance;


        private void Awake() { 
        // If there is an instance, do nothing 
            if (Instance == null) { 
                Instance = this;  
                DontDestroyOnLoad(Instance);
            }

            onAwakeEvents.Invoke();

            //print(this.gameObject.name);
            //print(Instance.gameObject.name);

            //print("1 : " + GetNamePlayer());
            //print("2 : " + GetActualProtein());
        }

        public void SetNamePlayer(string namePlayer){Instance.namePlayer = namePlayer;}
        public void SetProteinName(string actualProtein){Instance.actualProtein = actualProtein;}
        public void SetNamePlayer(){this.namePlayer = Instance.namePlayer;}
        public void SetProteinName(){this.actualProtein = Instance.actualProtein;}

        public string GetNamePlayer(){return Instance.namePlayer;}
        public string GetActualProtein(){return Instance.actualProtein;}
    }
}
