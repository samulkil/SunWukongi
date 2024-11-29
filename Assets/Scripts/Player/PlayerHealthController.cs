using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public Animator animator;
    public int currentHealth;
    public int maxHealth;

    public float invencible = 2f;
    private float invencibleCount;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name + ". Please attach an Animator component.");
            return;
        }
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (invencibleCount > 0)
        {
            invencibleCount -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        if (invencibleCount <= 0)
        {
            currentHealth -= damage;
            animator.SetTrigger("Hurt");  // Animação de dano
            StartCoroutine(DamageAnimation());

            invencibleCount = invencible;

            if (currentHealth <= 0)
            {
                animator.SetTrigger("Die");  // Aciona a animação de morte
                StartCoroutine(DeathSequence());

                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }

    }

    IEnumerator DeathSequence()
    {
        // Aguarda a duração da animação de morte
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

    }

    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
    }
}
