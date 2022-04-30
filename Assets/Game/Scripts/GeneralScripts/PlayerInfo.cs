using UnityEngine;

/*Name is self explanatory*/

using GameGeneralScripts.Reflection;

namespace GameGeneralScripts.Player{
    public class PlayerInfo : GeneralGetter{
        private string actualProtein;
        private string namePlayer;
        private float maxScore;
        private float lastScore;
        private float actualScore;

        //private float maxTime;
        //private float actualBestTime;

        //Singleton pattern
        public static PlayerInfo Instance { get; private set; }
        public PlayerInfo(){}
        private void Awake() { 
        // If there is an instance, and it's not me, delete myself.
        
            if (Instance != null && Instance != this) { 
                Destroy(this); 
            }else{ 
                Instance = this;
            }
        }

        public void SetNamePlayer(string namePlayer){this.namePlayer = namePlayer;}
        public void SetProteinName(string nameProtein){this.actualProtein = nameProtein;}
        public string GetNamePlayer(){return this.namePlayer;}
        public string GetActualProtein(){return this.actualProtein;}
    }
}
