using UnityEngine;

public class TankController : MonoBehaviour
{
    new Rigidbody rigidbody;
    public Transform turretTransform;

    [SerializeField] float power;
    const int turretRotationLimit = 300;
    int turretRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TurretMovement();
        BodyMovement();
    }

    void BodyMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rigidbody.velocity = v * power * transform.forward;
        if(v != 0f)
            transform.localEulerAngles += h / 10 * transform.up;
    }

    void TurretMovement()
    {
        if(Input.GetKey(KeyCode.Q)) 
        {
            if(turretRotation > -turretRotationLimit)
            {
                turretTransform.localEulerAngles -= transform.up / 5;
                turretRotation--;
            }
        }
        else if (Input.GetKey(KeyCode.E))
        {
            if (turretRotation < turretRotationLimit)
            {
                turretTransform.localEulerAngles += transform.up / 5;
                turretRotation++;
            }
        }
    }
}
