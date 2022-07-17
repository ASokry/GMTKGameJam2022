using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Enemy))]
public class Chase : MonoBehaviour
{
    public bool canChase;
    [SerializeField] public bool smoothChase;
    [SerializeField, Range(0f, 10f)] private float speed = 2f;
    [SerializeField,Range(0f, 20f)] private float maxAcceleration = 5f;

    private Rigidbody2D rb;
    private Enemy enemy;

    private Vector2 velocity;



    float distanceCheckCoolDown = 2;
    float timeSinceCoolDown = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();

        canChase = enemy.Target ? true : false;
    }

    private void Update()
    {
        rb.velocity = velocity;
        timeSinceCoolDown += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(canChase)
        {


            var direction = enemy.Target.position - transform.position;
            direction.Normalize();
            velocity = rb.velocity;

            if(smoothChase)
            {
                var desiredVelocity = new Vector2(direction.x, direction.y) * speed;
                var maxSpeedChange = maxAcceleration * Time.deltaTime;

                velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
                velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);
            }
            else
            {

                // STILL WIP

                var distance = Vector2.Distance(enemy.Target.position, transform.position);
                var acceleration = new Vector2(direction.x, direction.y) * speed;
                velocity += acceleration * Time.deltaTime;
                
                if(timeSinceCoolDown - distanceCheckCoolDown <= 0 && direction.sqrMagnitude > 2 * 2)
                {
                    velocity = Vector2.zero;
                    timeSinceCoolDown = Time.time;
                }

                Vector3 displacement = velocity * Time.deltaTime;
            }
                
        }
    }

}
