using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform player;

    [SerializeField] float speed;
    [SerializeField] float radius;

    [SerializeField] int life = 1;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Home").transform;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (life <= 0) return;

        Vector3 dir = player.position - transform.position;

        if (dir.magnitude > radius)
        {
            dir = target.position - transform.position;
        }
        dir.y = 0;
        transform.position += dir.normalized * speed * Time.deltaTime;
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

    public void Die()
    {
        speed = 0;
        animator.SetBool("Die",true);
        Destroy(gameObject,2);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
