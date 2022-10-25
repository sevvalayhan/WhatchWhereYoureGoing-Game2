using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletBehaviour : MonoBehaviour
{
    public Transform target;

    private float speed = 15.0f;
    private bool homing;
    private float rocketStrength = 15.0f;
    private float aliveTimer = 5.0f;

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (homing && target != null)
        {
            var moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);

        }
        if (speed==0)
        {
            Destroy(gameObject);

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                var away = -collision.contacts[0].normal;
                targetRigidbody.AddForce(away*rocketStrength,ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
