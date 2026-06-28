using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] InputActionReference moveAction;
    [SerializeField] float speed = 1;
    [SerializeField] int life = 5;
    [SerializeField] int currentLife = 5;

    [SerializeField] Transform spawnBullet;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Animator animator;
    [SerializeField] Image barLife;

    [SerializeField] GameObject bow;

    void Update()
    {
        Vector3 value = moveAction.action.ReadValue<Vector2>();
        Vector3 dir = new Vector3(value.x, 0, value.y);

        if (dir.magnitude > 0)
        {
            transform.position += dir.normalized * speed * Time.deltaTime;
            transform.forward = dir;

        }
        animator.SetFloat("Movement", dir.magnitude);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            animator.SetTrigger("Attack");
            Shoot();
        }

    }

    public void GetDamage(int damage)
    {
        currentLife -= damage;
        barLife.fillAmount = (float)currentLife / (float)life;
        Debug.Log(currentLife / life);

        if (currentLife <= 0)
        {
            Die();
            return;
        }
        animator.SetTrigger("Hit");
    }

    public void Die()
    {
        speed = 0;
        animator.SetBool("Death", true);
        Destroy(gameObject, 2);
    }
    public void Shoot()
    {
        Instantiate(bulletPrefab, spawnBullet.position, spawnBullet.rotation);
    }

    public void ShowBow(bool show)
    {
        bow.SetActive(show);
    }

}
