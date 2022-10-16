using System.Collections.Generic;
using UnityEngine;

namespace ProteinPart.InfoProtein{
    [System.Serializable]
    public class SynthesizingProtein{
        [TextArea(15,20)]
        [SerializeField] string descriptionProtein;
        [SerializeField] List<ExtraDataProtein> meta = default;

        [System.Serializable]

        private class ExtraDataProtein{
            [SerializeField] string nameExtra = default;
            [SerializeField] string descriptionExtra = default;

            public string GetNameExtra(){
                return nameExtra;
            }

            public string GetDescriptionExtra(){
                return descriptionExtra;
            }
        }

        public string GetDescriptionProtein(){
            return this.descriptionProtein;
        }

        public int GetQtdOfExtras(){
            return meta.Count;
        }  

        public string GetNameExtra(int index){
            return meta[index].GetNameExtra();
        }

        public string GetDescriptionTextExtra(int index){
            return meta[index].GetDescriptionExtra();
        }

    }
}
