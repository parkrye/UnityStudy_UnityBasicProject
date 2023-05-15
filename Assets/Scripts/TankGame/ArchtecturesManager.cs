using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace TankGameScripts
{
    public class ArchtecturesManager : MonoBehaviour
    {
        private void Start()
        {
            foreach (MeshRenderer child in GetComponentsInChildren<MeshRenderer>())
            {
                if (child.tag == "RandomLevelArt" && child.GetComponent<MeshRenderer>() != null)
                {
                    switch (Random.Range(0, 5))
                    {
                        case 0:
                            child.gameObject.SetActive(false);
                            break;
                        case 1:
                            child.transform.localEulerAngles += new Vector3(0, Random.Range(1, 360), 0);
                            break;
                        case 2:
                            child.transform.localScale *= Random.Range(0.5f, 2f);
                            break;
                    }
                }
            }

        }
    }

}

