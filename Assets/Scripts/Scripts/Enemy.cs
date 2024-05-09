using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float Acceleration;
    [SerializeField] private float turnSpeed;
    [SerializeField] private ParticleSystem dieParticle;

    private NavMeshAgent agent;
    private GameObject target;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.acceleration = Acceleration;
        agent.angularSpeed = turnSpeed;
    }

    private void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        agent.SetDestination(target.transform.position);
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.LookAt(target.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        speed = 0;
        animator.SetTrigger("death");
        ParticleSystem particle = Instantiate(dieParticle, transform.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
