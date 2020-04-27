using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField] Rigidbody2D FrontWheel;
    [SerializeField] Rigidbody2D BackWheel;

    [SerializeField] WheelJoint2D FrontWheelJoint;
    [SerializeField] WheelJoint2D BackWheelJoint;

    RaycastHit2D rayHit_front;
    RaycastHit2D rayHit_back;

    private float wheelRadius;

    private Vector2 BikeDirection;

    private float _vertical = 0;

    private void Awake()
    {
        wheelRadius = FrontWheel.GetComponent<CircleCollider2D>().radius;

        body = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 frontWheelStartPoint = FrontWheel.transform.position - (transform.up * (wheelRadius + 0.01f));
        Vector2 backWheelStartPoint = BackWheel.transform.position - (transform.up * (wheelRadius + 0.01f));

        rayHit_front = Physics2D.Raycast(frontWheelStartPoint, -transform.up, wheelRadius + 0.01f);
        rayHit_back = Physics2D.Raycast(backWheelStartPoint, -transform.up, wheelRadius + 0.01f);

        BikeDirection = FrontWheel.position - BackWheel.position;
        BikeDirection.Normalize();
    }
    private int movingforce = 1250;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _vertical += 0.1f;
            if (_vertical > 1)
            {
                _vertical = 1;
            }

            ApplyTorqueBike(movingforce);
        }
        else
        {
            _vertical = 0;
        }
    }

    public void ApplyTorqueBike(int force)
    {
        if (body.velocity.x <= 18f && body.velocity.x >= -15)
        {
            body.AddForce(_vertical * BikeDirection * force);
        }
    }
}