using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] Transform turretTransform;

    [SerializeField] float power;

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
            turretTransform.localEulerAngles -= transform.up / 5;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            turretTransform.localEulerAngles += transform.up / 5;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Shell")
        {
            GameManager.GetGameManager().Life--;
        }
    }
}
