using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // Parâmetros de saúde
    public int health = 100;
    private bool isDead = false;

    // Referência ao Rigidbody2D para controle de movimento
    public Rigidbody2D rigidBody;

    // Parâmetros de ataque
    public int attackDamage = 10; // Dano causado ao jogador
    public float attackRange = 1.5f; // Alcance do ataque
    public LayerMask playerLayer; // Camada para identificar o jogador
    public float attackCooldown = 1f; // Tempo entre ataques
    private float lastAttackTime = 0f; // Momento do último ataque

    // Referência ao Animator para animações
    private Animator animator;

    // Referência ao jogador
    private Transform player;

    void Start()
    {
        // Inicializa referências
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator não encontrado no GameObject!");
        }

        player = GameObject.FindWithTag("Player").transform; // Certifique-se de que o Player tem a tag "Player"
        if (player == null)
        {
            Debug.LogError("Player não encontrado! Certifique-se de que o jogador possui a tag 'Player'.");
        }
    }

    void Update()
    {
        if (isDead) return;

        // Verifica se está no alcance do jogador e se o cooldown acabou
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
            if (playerCollider != null && playerCollider.CompareTag("Player"))
            {
                Attack(playerCollider.gameObject);
                lastAttackTime = Time.time; // Atualiza o tempo do último ataque
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Se já estiver morto, ignora

        health -= damage;
        animator.SetTrigger("Hurt"); // Dispara a animação de dano

        if (health <= 0)
        {
            Die(); // Chama a lógica de morte
        }
    }

    private void Die()
    {
        if (isDead) return; // Evita múltiplas execuções
        isDead = true;

        // Configura animação e comportamento de morte
        animator.SetBool("isDead", true);
        rigidBody.velocity = Vector2.zero; // Para o movimento
        rigidBody.isKinematic = true; // Evita que a física afete o inimigo
        GetComponent<Collider2D>().enabled = false; // Desativa o colisor

        StartCoroutine(DeathSequence());
    }

    private void Attack(GameObject playerObject)
    {
        if (isDead) return;

        // Dispara a animação de ataque
        animator.SetTrigger("Attack");
            
        // Aplica dano ao jogador
        PlayerHealthController.instance.TakeDamage(attackDamage);

    }

    IEnumerator DeathSequence()
    {
        // Aguarda o fim da animação de morte
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);

        // Desativa ou destrói o inimigo
        Destroy(gameObject);
    }

    // Desenha o alcance do ataque no editor da Unity para ajustes visuais
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
