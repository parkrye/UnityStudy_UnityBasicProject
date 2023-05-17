using UnityEngine;
using UnityEngine.SceneManagement;

namespace Homework
{
    public class Title : MonoBehaviour
    {
        public void OnPlayButton()
        {
            SceneManager.LoadScene("Tank");
        }
    }

}