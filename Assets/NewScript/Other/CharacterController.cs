using ProjectDawn.Navigation.Hybrid;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterController : MonoBehaviour
{
    public Image healthBar;

    public float health = 100f;
    public float maxHealth = 100f;

    public float attackPower = 10f;
    public float attackDistance = 5f;

    protected abstract string OpponentTag { get; }

    void Update()
    {
        MoveToClosestOpponent();
        AttackOpponent();
    }

    void MoveToClosestOpponent()
    {
        GameObject closestOpponent = FindClosestOpponent();
        if (closestOpponent != null)
        {
            SetDestination(closestOpponent.transform.position);
        }
    }

    GameObject FindClosestOpponent()
    {
        GameObject[] opponents;
        opponents = GameObject.FindGameObjectsWithTag(OpponentTag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject opponent in opponents)
        {
            Vector3 diff = opponent.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = opponent;
                distance = curDistance;
            }
        }
        return closest;
    }

    void SetDestination(Vector3 destination)
    {
        GetComponent<AgentAuthoring>().SetDestination(destination);
    }

    void AttackOpponent()
    {
        GameObject closestOpponent = FindClosestOpponent();
        if (closestOpponent != null)
        {
            float distanceToOpponent = Vector3.Distance(transform.position, closestOpponent.transform.position);

            if (distanceToOpponent <= attackDistance)
            {
                CharacterController opponentController = closestOpponent.GetComponent<CharacterController>();
                opponentController.TakeDamage(attackPower);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
