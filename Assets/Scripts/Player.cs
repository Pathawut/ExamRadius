using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Die = Animator.StringToHash("Die");

    [SerializeField] private LayerMask m_Enemy;
    [SerializeField] private LayerMask m_Ground;

    public Transform Base;

    // Component
    private Animator m_Anim;
    private NavMeshAgent m_Agent;

    // Enemy
    private Transform m_Target;

    public int MaxHP = 100;
    public int Atk = 10;
    public float AtkRange = 2.0f;
    public float Speed = 3.0f;

    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Left Click on Enemy
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, m_Enemy))
            {
                m_Target = hit.collider.transform;
                m_Anim.Play(Walk);

                m_Agent.SetDestination(m_Target.position);
            }
        }

        // Right Click on Ground
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_Ground))
            {
                m_Target = null;
                m_Anim.Play(Walk);

                m_Agent.SetDestination(hit.point);
            }
        }

        if (m_Target != null)
        {
            var del = transform.position - m_Target.position;
            if (del.magnitude <= AtkRange)
            {
                m_Anim.Play(Attack);
            }
            else
            {
                m_Anim.Play(Walk);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            m_Anim.Play(Attack);
            m_Target = other.transform;
        }
    }

    public void Reset()
    {
        var pos = Base.position;
        transform.position = new Vector3(pos.x, 0.0f, pos.y);
    }
}