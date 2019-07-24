using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Base : Character_Base
{
    public delegate void Ability();
    public Ability[] abilities = new Ability[3];
    public List<Ability> buffs = new List<Ability>();

    ITarget currentTarget;
    public List<ITarget> activeTargets = new List<ITarget>();

    Vector3 movement;
    float moveMod;

    bool canAttack;
    bool isAttacking;

    Inventory inventory;
    public CS_Struct.Character_Data data;

    [SerializeField]
    public CS_Fear fear;

    int controls;
    bool canInvert;
    bool isChecking;

    public override void Init()
    {
        base.Init();

        InventorySetup();

        controls = 1;
        canInvert = true;
        isChecking = false;

        abilities[0] = EmpoweredStrike;
        abilities[1] = Cleave;
        abilities[2] = DefensiveCurl;

        movement = Vector3.zero;
        moveMod = 0.075f;

        IsAlive = true;
        canAttack = true;
        isAttacking = false;

        float baseTemp = 100.0f;
        health = new CS_Stat(baseTemp, skills.data.health, 0.0f, skills.data.healthRegen, HealthChange);
        baseTemp = 50.0f;
        mana = new CS_Stat(baseTemp, skills.data.mana, 0.0f, skills.data.manaRegen, ManaChange);

        fear = new CS_Fear(0.0f, 100.0f, FearChange);
        StartCoroutine(fear.PassiveFear());

        StartCoroutine(Tick());
    }

    public override void SkillSetUp()
    {
        skills = gameObject.GetComponent<Skills>();

        if (skills == null)
        {
            skills = gameObject.AddComponent<Skills>();
        }

        skills.SetCharacter(this);
        skills.LoadCharacter(data.skillData);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public void InventorySetup()
    {
        inventory = gameObject.GetComponent<Inventory>();

        if (inventory == null)
        {
            inventory = gameObject.AddComponent<Inventory>();
        }

        inventory.SetCharacter(this);
        inventory.SetCurrent(equipment);
    }

    public override void UpdateAllStats()
    {
        base.UpdateAllStats();

        health.ChangeMade();
        mana.ChangeMade();
        fear.ChangeMade();
        Player_Manager.Instance.XPChange();

        UI_Manager.Instance.inGame_ui.UpdateSkills(skills.data.oneHanded, skills.data.twoHanded, skills.data.dualWield,
            skills.data.magic, skills.data.archery, skills.data.heavyArmour, skills.data.lightArmour, skills.data.clothArmour,
            skills.data.health, skills.data.mana, skills.data.healthRegen, skills.data.manaRegen, skills.data.points);
    }

    public override void DamageTaken(float pDamage)
    {
        fear.ModFear(pDamage, true);
    }

    public void Movement(float pX, float pY)
    {
        Vector3 input = new Vector3(pX, 0, pY);

        movement = input * moveMod;
        movement *= controls;

        transform.LookAt((input * controls) + transform.position, Vector3.up);
    }

    public override IEnumerator Tick()
    {
        while (IsAlive)
        {
            transform.position += movement;
            currentTarget = SetActive();

            if (currentTarget != null)
            {
                if (currentTarget is IDamage)
                {
                    canAttack = true;
                }
            }
            else
            {
                canAttack = false;
            }

            if (fear.current >= 75)
            {
                if(!isChecking)
                    StartCoroutine(FearCheck());
            }

            yield return new WaitForFixedUpdate();
        }

        yield break;
    }

    IEnumerator FearCheck()
    {
        isChecking = true;

        while(isChecking)
        {
            isChecking = (fear.current >= 75) ? true : false;
            Debug.Log("Is Checking...");
            yield return new WaitForSeconds(5.0f);

            float chance = Random.Range(0, 100);

            if(chance > 90 && canInvert)
            {
                StartCoroutine(InvertControls());
            }
        }
    }

    IEnumerator InvertControls()
    {
        if (canInvert)
        {
            canInvert = false;
            controls = -1;
            Debug.Log("Controls Inverted");
            yield return new WaitForSeconds(3.0f);
            canInvert = true;
            controls = 1;
        }
        else
        {
            yield return null;
        }
    }

    public IEnumerator BuffManager()
    {
        while (buffs.Count > 0)
        {
            // REWORK <-- If a buff is removed part way though the loop, it will create an iteration issue.
            foreach(Ability buff in buffs)
            {
                if (buff != null)
                {
                    buff();
                }
            }

            yield return new WaitForSeconds(1.0f);
        }

        yield break;
    }

    void BuffCheck(Ability ability)
    {
        if(buffs.Count <= 0)
        {
            buffs.Add(ability);
            StartCoroutine(BuffManager());
        }
        else
        {
            buffs.Add(ability);
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ITarget>() != null)
        {
            ITarget target = other.GetComponent<ITarget>();

            if (target != null)
            {
                if(!activeTargets.Contains(target))
                    activeTargets.Add(target);
            }
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ITarget>() != null)
        {
            ITarget target = other.GetComponent<ITarget>();
            RemoveActiveTarget(target);
        }
    }

    ITarget SetActive()
    {
        if (activeTargets.Count > 0)
        {
            activeTargets.Sort((x, y) => y.TargetPriority.CompareTo(x.TargetPriority));

            if (currentTarget != activeTargets[0] && currentTarget != null)
            {
                currentTarget.ToggleHighlight(false);
            }

            activeTargets[0].ToggleHighlight(true);
            return activeTargets[0];
        }

        return null;
    }

    void RemoveActiveTarget(ITarget pTarget)
    {
        if (activeTargets.Contains(pTarget))
        {
            activeTargets.Remove(pTarget);
            pTarget.Deactivate();
        }
    }

    IEnumerator WaitAttacking()
    {
        yield return new WaitForSeconds(1.0f / attackSpeed);
        Debug.Log("Stopped Attacking");
        isAttacking = false;
    }

    public void HealthChange(float pCurrent, float pMax)
    {
        if (health.RegenCheck())
        {
            StartCoroutine(health.Regen());
        }

        Player_Manager.Instance.HealthChange(pCurrent, pMax);
    }

    public void ManaChange(float pCurrent, float pMax)
    {
        if (mana.RegenCheck())
        {
            StartCoroutine(mana.Regen());
        }

        Player_Manager.Instance.ManaChange(pCurrent, pMax);
    }

    public void FearChange(float pCurrent, float pMax)
    {
        Player_Manager.Instance.FearChange(pCurrent, pMax);
    }

    public void Drop(int pIndex)
    {
        inventory.DropItem(pIndex).Drop(transform.position + (Vector3.up / 2.0f));
    }

    public void AddXP(int pXP)
    {
        skills.IncreaseExperience(pXP);
        Player_Manager.Instance.XPChange();
    }
    public int GetLevel()
    {
        return skills.data.level;
    }

    public void Interact()
    {
        if (currentTarget is IDamage)
        {
            // <-- Make into seperate function for ease of use
            if (CS_Utility.GetDistance(currentTarget.GetGO()) < 2.0f && !isAttacking && canAttack)
            {
                Debug.Log("ATTACKING!");
                isAttacking = true;

                if (currentTarget.GetInterface<IDamage>().TakeDamage(damage * 2.0f))
                {
                    //fear.ModFear(1.0f, false);
                }
                else
                {
                    RemoveActiveTarget(currentTarget);
                    fear.ModFear(0.5f, false);
                }

                if (isAttacking)
                {
                    StartCoroutine(WaitAttacking());
                }
            }
            else
            {
                Debug.Log("Failed to attack");
                //Move towards the target until in range, then attack if no other input
            }
        }
        else if (currentTarget is IItem)
        {
            if (CS_Utility.GetDistance(currentTarget.GetGO()) < 4.0f)
            {
                inventory.AddItem(currentTarget.GetInterface<IItem>());
                RemoveActiveTarget(currentTarget);
            }
        }
        else if (currentTarget is IEnvironment)
        {
            if (CS_Utility.GetDistance(currentTarget.GetGO()) < 2.0f)
            {
                currentTarget.GetInterface<IEnvironment>().Interact();
                RemoveActiveTarget(currentTarget);
            }
        }
    }
    public void Abiilty1()
    {
        abilities[0]();
    }
    public void Abiltiy2()
    {
        abilities[1]();
    }
    public void Ability3()
    {
        abilities[2]();
    }

    // Abilities Definitions - Move to a different class for clarity of code.
    public void EmpoweredStrike()
    {
        if (currentTarget is IDamage)
        {
            if (CS_Utility.GetDistance(currentTarget.GetGO()) < 2.0f && !isAttacking && canAttack)
            {
                Debug.Log("Empowered Strike!");
                isAttacking = true;

                if (currentTarget.GetInterface<IDamage>().TakeDamage(damage * 2.0f))
                {
                    fear.ModFear(1.0f, false);
                }
                else
                {
                    activeTargets.Remove(currentTarget);
                    fear.ModFear(1.0f, false);
                }

                mana.ReduceStat(15.0f);

                if (isAttacking)
                {
                    StartCoroutine(WaitAttacking());
                }
            }
        }
    }

    public void Cleave()
    {
        // Cast a ray with the current target as the centre position. Go from main to off dealing 50% of damage. Reduce mana by 30.

        if (currentTarget is IDamage)
        {
            if (CS_Utility.GetDistance(currentTarget.GetGO()) < 2.0f && !isAttacking && canAttack)
            {
                Debug.Log("Cleave!");
                isAttacking = true;
                //Some fancy cone infront of the player, hit each target
                mana.ReduceStat(30.0f);

                if (isAttacking)
                {
                    StartCoroutine(WaitAttacking());
                }
            }
        }
    }

    public void DefensiveCurl()
    {
        if (!buffs.Contains(DefenseCurl))
        {
            buffs.Add(DefenseCurl);
        }
        else
        {
            buffs.Remove(DefenseCurl);
            UpdateCombat();
        }
    }

    void DefenseCurl()
    {
        UpdateCombat();

        if (mana.ReduceStat(10.0f))
        {
            Debug.Log("Squirtle use Defense Curl!");
            resist += 10.0f;
        }
        else
        {
            buffs.Remove(DefenseCurl);
        }
    }

    public void Save()
    {
        data.name = "Character " + data.saveIndex.ToString();
        data.skillData = skills.GetSkillData();
        data.itemData = inventory.GetItemData();

        Player_Manager.Instance.data = data;
        Data_Manager.Instance.Save(data);
    }

    public override void ResetClass()
    {
        base.ResetClass();
    }

    public override void Death()
    {
        IsAlive = false;
        IsActive = false;
        Player_Manager.Instance.Death();
    }
}
