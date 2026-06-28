using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform player;

    [SerializeField] float speed;
    [SerializeField] float detectionRadius;
    [SerializeField] float attackRadius;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] int life = 1;

    private Animator animator;
    private float actualSpeed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Home").transform;
        player = GameObject.FindWithTag("Player").transform;
        actualSpeed = speed;
    }

    void Update()
    {
        if (life <= 0) return;

        Vector3 dir = player.position - transform.position;

        if(dir.magnitude < attackRadius)
        {
            animator.SetTrigger("Attack");
            actualSpeed = 0;
        }
        else if (dir.magnitude > detectionRadius)
        {
            dir = target.position - transform.position;
        }
        dir.y = 0;
        transform.position += dir.normalized * actualSpeed * Time.deltaTime;
        transform.forward = dir;

    }

    public void GetDamage(int damage)
    {
        life -= damage;

        if(life <= 0)
        {
            Die();
            return;
        }

        animator.SetTrigger("Hit");
    }

    public void Attack()
    {
        if(Physics.Raycast(transform.position + Vector3.up * 1.5f, transform.forward, out RaycastHit hit, (attackRadius + 0.5f), playerLayer)) {
            Player p = hit.collider.GetComponent<Player>();
            if (p != null)
            {
                p.GetDamage(1);
            }
        }

        Debug.DrawLine(transform.position + Vector3.up * 1.5f,
                       transform.position + Vector3.up * 1.5f + transform.forward * (attackRadius + 0.5f),
                       Color.red, 1);
    }

    public void StopAttack()
    {
        actualSpeed = speed;
    }

    public void Die()
    {
        speed = 0;
        animator.SetBool("Die",true);
        Destroy(gameObject,2);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
