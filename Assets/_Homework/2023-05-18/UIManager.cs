using TMPro;
using UnityEngine;

namespace Homework
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMeshProUGUI;

        public void ShowFireCounter()
        {
            textMeshProUGUI.text = "" + GameManager.Instance.FireCount;
        }
    }

}