using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Menu
{
    public class CreditInformation : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameCredit;
        [SerializeField] private TextMeshProUGUI contentCredit;
        [SerializeField] private Image imageCredit;

        public void Setup(string nameCredit, string contentCredit, Sprite imageCredit)
        {
            this.nameCredit.text = nameCredit;
            this.contentCredit.text = contentCredit;
            this.imageCredit.sprite = imageCredit;
        }
    }
}