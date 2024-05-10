using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerBehavior : MonoBehaviour
{
    private NavMeshAgent agent;

    [Header("Movement Configs")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float Acceleration;
    [SerializeField] private float turnSpeed;
    [Header("Attack Configs")]
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private Transform attackPos;
    [SerializeField] private FireBall spell;
    [Header("Particle Configs")]
    [SerializeField] private ParticleSystem clickParticle;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.acceleration = Acceleration;
        agent.angularSpeed = turnSpeed;
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnPlayerMove += HandleClick;
    }

    private void HandleClick()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                HandleAttack(hit);
            }
            else
            {
                HandleMovement(hit);
            }            
        }
    }

    private void HandleAttack(RaycastHit hit)
    {
        print("Attacking");
        GetComponent<Animator>().SetTrigger("attack");
        transform.LookAt(hit.point);
        Instantiate(spell, attackPos.position, attackPos.rotation);
    }

    private void HandleMovement(RaycastHit hit)
    {
        agent.SetDestination(hit.point);
        if (clickParticle != null)
        {
            ParticleSystem particle = Instantiate(clickParticle, hit.point, clickParticle.transform.rotation);
            Destroy(particle.gameObject, 0.5f);
        }
        GameManager.Instance.AudioManager.PlaySFX(SFX.click);
    }    

    public NavMeshAgent GetPlayerAgent()
    {
        return agent;
    }
}
