using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootingGame
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] Slider engineSlider, energySlider;
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField][Range(0, 10000)] int engine; 
        [SerializeField][Range(0, 100)] int energy;
        [SerializeField] int score;

        void Awake()
        {
            engine = 10000;
            energy = 100;
            engineSlider.value = engine / 100;
            energySlider.value = energy;
        }

        public bool UseEngine(int use = 1)
        {
            if(engine >= use)
            {
                engine -= use;
                engineSlider.value = engine / 100;
                return true;
            }
            return false;
        }

        public bool UseEnergy(int use = 1)
        {
            if (energy >= use)
            {
                energy -= use;
                energySlider.value = energy;
                return true;
            }
            return false;
        }

        public void FillEngine(int fill = 1)
        {
            if (engine + fill <= 100)
            {
                engine += fill;
                engineSlider.value = engine / 100;
                return;
            }
            engine = 100;
            engineSlider.value = engine / 100;
        }

        public void FillEnergy(int fill = 1)
        {
            if (energy + fill <= 100)
            {
                energy += fill;
                energySlider.value = energy;
                return;
            }
            energy = 100;
            energySlider.value = energy;
        }

        public void AddScore(int add = 1)
        {
            score += add;
            scoreText.text = "" + score;
        }
    }
}