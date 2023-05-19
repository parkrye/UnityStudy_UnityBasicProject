using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFixer : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    enum FixCase { X, Y, Z, XY, XZ, YZ, XYZ }
    [SerializeField] FixCase fixCase; 

    void LateUpdate()
    {
        switch (fixCase)
        {
            case FixCase.X:
                transform.position = new Vector3(targetTransform.position.x, transform.position.y, transform.position.z);
                break;
            case FixCase.Y:
                transform.position = new Vector3(transform.position.x, targetTransform.position.y, transform.position.z);
                break;
            case FixCase.Z:
                transform.position = new Vector3(transform.position.x, transform.position.y, targetTransform.position.z);
                break;
            case FixCase.XY:
                transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y, transform.position.z);
                break;
            case FixCase.XZ:
                transform.position = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);
                break;
            case FixCase.YZ:
                transform.position = new Vector3(transform.position.x, targetTransform.position.y, targetTransform.position.z);
                break;
            case FixCase.XYZ:
                transform.position = targetTransform.position;
                break;
        }
    }
}
