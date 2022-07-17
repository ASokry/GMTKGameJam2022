using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private Transform _target;
    public Transform Target { get { return _target; } set { _target = value; } }

    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            var player = collision.transform.GetComponent<PlayerStats>();
            player.TakeDamage(damage);
        }
    }
}
