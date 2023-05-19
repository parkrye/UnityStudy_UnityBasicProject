using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataViewer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] Slider[] sliders;  // hp, sp, exp;

    public void OnStatusChanged(int[] status, int num)
    {
        if(num == 0)
        {
            level.text = "Level " + status[0];
            sliders[0].maxValue = status[1];
            sliders[0].value = status[2];
            sliders[1].maxValue = status[3];
            sliders[1].value = status[4];
            sliders[2].maxValue = status[5];
            sliders[2].value = status[6];
        }
    }
}
