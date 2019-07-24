using UnityEngine;
using System.Collections;
using UnityEngine.AI;

using CS_Enum;
using CS_Struct;

public class AI_Base : Character_Base, IEnemy, IDrop
{
    RARITY rarity;
    public RARITY Rarity
    { get { return rarity; } set { rarity = value; } }

    IItem itemDrop;
    public IItem ItemDrop
    { get { return itemDrop; }
        set
        {
            itemDrop = value;
            drop = itemDrop.GetITarget().GetGO();
            EquipItem(itemDrop);
        }
    }

    public GameObject drop;

    NavMeshAgent agent;
    Collider myMeshCollider;
    SphereCollider activationCollider;

    bool isAttacking;

    float chaseRadius;
    float attackRadius;

    public Vector3 spawner;

    public override void Init()
    {
        base.Init();

        agent = GetComponent<NavMeshAgent>();
        myMeshCollider = transform.Find("Mesh").GetComponent<Collider>();
        activationCollider = GetComponent<SphereCollider>();

        ToggleHighlight(false);
        IsActive = false;
        IsAlive = true;

        chaseRadius = activationCollider.radius;
        attackRadius = 0.55f;
        BasePriority = 10.0f;

        float baseTemp = 50.0f;
        health = new CS_Stat(baseTemp, skills.data.health, 0.0f, 0.0f);
        baseTemp = 50.0f;
        mana = new CS_Stat(baseTemp, skills.data.mana, 0.0f, 0.0f);

        transform.position = spawner;
    }

    public override IEnumerator Tick()
    {
        // Check if AI is active
        while (IsActive && IsAlive)
        {
            // Set the distance between the AI Unit and the Player
            float dis = CS_Utility.GetDistance(gameObject);
            UpdateTargetPriority(dis);

            if (dis <= chaseRadius) // If within Range set the players position as the target position
            {
                agent.enabled = true;
                agent.destination = Player_Manager.Instance.GetPlayerGO().transform.position;
                agent.isStopped = false;

                if (dis <= attackRadius) // If within Attack range, set the velocity of the Agent to 0 and attack
                {
                    agent.velocity = Vector3.zero;

                    if (dis < 2.0f && !isAttacking)
                    {
                        isAttacking = true;
                        float mod = (Player_Manager.Instance.GetPlayer().fear.current >= 50) ? 2.0f : 1.0f;
                        Player_Manager.Instance.GetPlayer().GetInterface<IDamage>().TakeDamage(damage / mod);
                        StartCoroutine(WaitAttacking());
                    }
                }
            }
            else
            {
                agent.enabled = false;
            }

            yield return null;
        }

        agent.isStopped = true;
        yield break;
    }

    IEnumerator WaitAttacking()
    {
        yield return new WaitForSeconds(3.0f);
        isAttacking = false;
    }

    public override void UpdateAllStats()
    {
        base.UpdateAllStats();
        UpdateCombat();
    }

    public override bool TakeDamage(float pDamage)
    {
        pDamage = pDamage - (pDamage / 100.0f * resist);

        float mod = (Player_Manager.Instance.GetPlayer().fear.current >= 25) ? 2.0f : 1.5f;

        if (health.ReduceStat(pDamage * mod))
        {
            DamageTaken(2.0f);
            return true;
        }
        else
        {
            Death();
            return false;
        }
    }

    public void Drop()
    {
        ItemDrop.Drop(transform.position + (Vector3.up / 2.0f));
    }

    public override void ResetClass()
    {
        base.ResetClass();
        health.current = health.max;
        IsAlive = true;
        myMeshCollider.enabled = true;
        activationCollider.enabled = true;
        gameObject.SetActive(true);
    }

    public override void Death()
    {
        ToggleHighlight(false);
        IsAlive = false;
        IsActive = false;
        myMeshCollider.enabled = false;
        activationCollider.enabled = false;
        gameObject.SetActive(false);

        Player_Manager.Instance.AddXP(50); // <-- Note To Self: make some cleaver algorithem for this shit.
        Drop();
    }
}