using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Menu
{
    public class CreditHolder : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI groupName;

        public void Setup(string groupName)
        {
            this.groupName.text = groupName;
        }
    }
}