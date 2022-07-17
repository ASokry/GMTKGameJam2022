using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Enemy))]
public class Chase : MonoBehaviour
{
    public bool canChase;
    [SerializeField] private float speed = 2f;

    [Header("Smooth Chase")]
    [SerializeField] public bool smoothChase;
    [SerializeField] private float maxAcceleration = 5f;

    [Header("Rocket Chase")]
    [SerializeField, Range(5f, 50f)] private float resetVelocityRange = 10f;

    [Header("Range")]
    [SerializeField] bool hasRange;
    [SerializeField, Range(1f, 50f)] private float maxRange = 5f;

    private Rigidbody2D rb;
    private Enemy enemy;
    private Vector2 velocity;
    private Vector2 heading;
    private Vector2 direction;
    private Vector2 displacement;
    float distanceCheckCoolDown = 2;
    float timeSinceCoolDown = 0;

    Vector2 gizmo_dir;
    Vector2 gizmo_velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();

        
        gizmo_dir = transform.position;
        gizmo_velocity = transform.position;
    }

    private void Start()
    {
        canChase = enemy.Target ? true : false;
    }

    private void Update()
    {
        rb.velocity = velocity;
        gizmo_velocity = velocity;
    }

    private void FixedUpdate()
    {
        if(canChase)
        {
            heading = enemy.Target.position - transform.position;
            direction = heading.normalized;
            velocity = rb.velocity;

            if(hasRange)
            {

                if(heading.sqrMagnitude < maxRange * maxRange)
                {
                    Move();
                }   
                else
                {
                    velocity = Vector2.zero;
                }
            }
            else
            {
                Move();
            }

            gizmo_dir = direction;
                
        }
    }

    private void Move()
    {
        if (smoothChase)
        {
            SmoothChase();

        }
        else
        {
            RocketChase();
        }
    }
    

    private void SmoothChase()
    {
        var desiredVelocity = new Vector2(direction.x, direction.y) * speed;
        var maxSpeedChange = maxAcceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);
    }

    // works well, but can break sometimes
    private void RocketChase()
    {

        var acceleration = new Vector2(direction.x, direction.y) * speed;
        velocity += acceleration * Time.deltaTime;

        if (Time.time - timeSinceCoolDown >= distanceCheckCoolDown && heading.sqrMagnitude > resetVelocityRange * resetVelocityRange)
        {
            //print("check");
            velocity = Vector2.zero;
            timeSinceCoolDown = Time.time;
        }

        //displacement = velocity * Time.deltaTime;

        

    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + gizmo_dir);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + gizmo_velocity);
        }
    }

}
