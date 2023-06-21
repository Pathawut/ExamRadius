using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Die = Animator.StringToHash("Die");

    // Component
    private Animator m_Anim;
    private NavMeshAgent m_Agent;

    // Enemy
    private Transform m_Target;

    private bool isChasing = false;

    public float MaxRange = 10.0f;
    public Transform Base;

    public int MaxHP = 100;
    public int Atk = 10;
    public float AtkRange = 2.0f;
    public float Speed = 2.7f;

    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("OnTriggerEnter Player");
            m_Anim.Play(Attack);

            m_Target = other.transform;
        }
    }


    private void Update()
    {
        if (m_Target != null)
        {
            var del = transform.position - m_Target.position;
            if (del.magnitude > AtkRange)
            {
                isChasing = true;
                m_Anim.Play(Walk);
            }
            else
            {
                isChasing = false;
                m_Anim.Play(Attack);
            }
        }

        if (isChasing)
        {
            if (m_Target != null)
            {
                var del = transform.position - m_Target.position;
                if (del.magnitude < MaxRange)
                {
                    m_Agent.SetDestination(m_Target.position);
                }
                else
                {
                    isChasing = false;
                }
            }
            
        }
        else
        {
            var del = transform.position - Base.position;
            if (del.magnitude < MaxRange)
            {
                m_Agent.SetDestination(Base.position);
            }
            else
            {
                isChasing = false;
                m_Target = null;
            }
        }
    }

    public void Reset()
    {
        var pos = Base.position;
        transform.position = new Vector3(pos.x, 0.0f, pos.y);
    }
}