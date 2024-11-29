using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2f; // Distância para iniciar o ataque
    public float timeBetweenAttacks = 1f; // Tempo entre ataques
    public int attackDamage = 10; // Dano causado por ataque

    private Transform player; // Referência ao Player
    private Animator animator;
    private float attackCooldown;

    private bool isPlayerInRange; // Verifica se o Player está no alcance

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Certifique-se de que o Player tem a tag "Player"
        animator = GetComponent<Animator>();
        attackCooldown = 0;
    }

    void Update()
    {
        // Reduz o tempo de espera entre ataques
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        // Detecta o Player no alcance de ataque
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            isPlayerInRange = true;
            AttackPlayer();
        }
        else
        {
            isPlayerInRange = false;
            // Aqui você pode adicionar lógica para o inimigo perseguir o Player
        }
    }

    void AttackPlayer()
    {
        if (isPlayerInRange && attackCooldown <= 0)
        {
            animator.SetTrigger("Attack"); // Ativa a animação de ataque
            PlayerHealthController.instance.TakeDamage(attackDamage); // Aplica dano ao Player
            attackCooldown = timeBetweenAttacks; // Reseta o tempo entre ataques
        }
    }

    // Para adicionar feedback visual, pode-se implementar Gizmos para mostrar o alcance do ataque
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
