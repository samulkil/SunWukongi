using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public int attackDamage = 10; // Dano causado ao jogador
    public float attackRange = 1.5f; // Alcance do ataque
    public LayerMask playerLayer; // Camada para identificar o jogador
    public float attackCooldown = 1f; // Tempo entre ataques

    private float lastAttackTime = 0f; // Momento do último ataque

    // Update is called once per frame
    void Update()
    {
        // Verifica se está no alcance do jogador e se o cooldown acabou
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Collider2D player = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
            if (player != null && player.CompareTag("Player"))
            {
                Attack(player.gameObject);
                lastAttackTime = Time.time; // Atualiza o tempo do último ataque
            }
        }
    }

    private void Attack(GameObject player)
    {
        // Aplica dano ao jogador
        PlayerHealthController.instance.TakeDamage(attackDamage);

        // Toca o som do ataque
    }

    // Desenha o alcance do ataque no editor da Unity para ajustes visuais
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
