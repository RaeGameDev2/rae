using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class PlayerSpells : MonoBehaviour
{
    public bool phaseWalkActive;
    public bool lifeDrainActive;
    public bool parryActive;
    public bool shieldActive;
    public bool debuffActive;
    private bool quickTeleportActive;
    public bool orbDropped;
    public float shieldDamage;
    private bool pause;
    private PlayerSkills playerSkills;
    private Resources playerResources;
    private Weapons_Handler weaponsHandler;
    private Vector3 transportPosition;
    [SerializeField] private GameObject damageRae;
    [SerializeField] private GameObject shockwavePrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject phaseWalkPrefab;
    [SerializeField] private GameObject teleportPrefab;
    private GameObject instanceShield;
    [SerializeField] private float timeLifeDrain = 5;
    [SerializeField] private float timePhaseWalk = 5;
    [SerializeField] private float timeParry = 1;

    private void Awake()
    {
        playerSkills = GetComponent<PlayerSkills>();
        weaponsHandler = GetComponent<Weapons_Handler>();
        playerResources = GetComponent<Resources>();
        phaseWalkActive = false;
        lifeDrainActive = false;
        parryActive = false;
        shieldActive = false;
        debuffActive = false;
        quickTeleportActive = false;
        pause = false;
    }

    private void Update()
    {
        CheckShield();

        if (!Input.GetKeyDown(KeyCode.Alpha1)) return;
        if (quickTeleportActive) return;
        if (shieldActive) return;
        if (lifeDrainActive) return;
        if (parryActive) return;
        if (phaseWalkActive) return;
        switch (weaponsHandler.currWeapon.type)
        {
            case Weapon.WeaponType.SCYTHE:
                if (playerSkills.IsLifeDrainUnlocked())
                {
                    lifeDrainActive = true;
                    StartCoroutine("StopLifeDrain");
                    StartCoroutine(SpellAnimation());
                    playerResources.UseMana();
                }
                else if (playerSkills.IsParryUnlocked())
                {
                    parryActive = true;
                    StartCoroutine("StopLifeDrain");
                    StartCoroutine(SpellAnimation());
                    playerResources.UseMana();
                }
                break;
            case Weapon.WeaponType.ORB:
                if (playerSkills.IsQuickTpUnlocked())
                {
                    if (orbDropped)
                    {
                        orbDropped = false;
                        StartCoroutine(TeleportAnimation());
                    }
                    else
                    {
                        orbDropped = true;
                        transportPosition = transform.position;
                        playerResources.UseMana();
                        StartCoroutine(SpellAnimation());
                    }
                }
                else if (playerSkills.IsShieldUnlocked())
                {
                    shieldActive = true;
                    shieldDamage = playerSkills.GetLevelShield();
                    instanceShield = Instantiate(shieldPrefab, transform.position + new Vector3(0f, 1f, -0.1f), Quaternion.identity, transform);
                    playerResources.UseMana();
                }
                break;
            case Weapon.WeaponType.STAFF:
                if (playerSkills.IsPhaseWalkUnlocked())
                {
                    phaseWalkActive = true;
                    StartCoroutine("StopPhaseWalk");
                    playerResources.UseMana();
                }
                else if (playerSkills.IsDebuffUnlocked())
                {
                    if (debuffActive) return;
                    debuffActive = true;
                    StartCoroutine(SpellAnimation());
                    playerResources.UseMana();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Pause()
    {
        pause = !pause;
    }
    public void StopDebuff()
    {
        debuffActive = false;
    }

    private void CheckShield()
    {
        if (!shieldActive) return;
        if (shieldDamage != 0) return;
        Destroy(instanceShield);
        shieldActive = false;
    } 

    public void Shockwave()
    {
        parryActive = false;
        var instance = Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
        instance.GetComponent<Shockwave>().SetLevelParry(playerSkills.GetLevelParry());
        instance.transform.parent = transform;
    }

    private IEnumerator StopLifeDrain()
    {
        yield return new WaitForSeconds(timeLifeDrain);
        lifeDrainActive = false;
    }
    private IEnumerator StopParry()
    {
        yield return new WaitForSeconds(timeParry);
        lifeDrainActive = false;
    }
    private IEnumerator StopPhaseWalk()
    {
        var instance = Instantiate(phaseWalkPrefab,
            transform.position + new Vector3(0f, 1f, -0.66f), Quaternion.identity, transform);
        yield return new WaitForSeconds(timePhaseWalk - 1f);
        phaseWalkActive = false;
        StartCoroutine(StopPhaseWalkAnimation(instance));
    }
    private IEnumerator StopPhaseWalkAnimation(GameObject instance)
    {
        var material = instance.GetComponent<Renderer>().material;
        var spriteRenderer = instance.GetComponentInChildren<SpriteRenderer>();
        var colorMaterial = material.color;
        var colorRenderer = spriteRenderer.color;
        var albedo = 1f;
        while (albedo > 0)
        {
            yield return new WaitForFixedUpdate();
            albedo -= Time.fixedDeltaTime;
            material.color = new Color(colorMaterial.r, colorMaterial.g, colorMaterial.b, albedo);
            spriteRenderer.color = new Color(colorRenderer.r, colorRenderer.g, colorRenderer.b, albedo);
            instance.transform.position -= Vector3.back * Time.fixedDeltaTime / 2f;
        }
        Destroy(instance);
    }

    private IEnumerator TeleportAnimation()
    {
        quickTeleportActive = true;
        var instance = Instantiate(teleportPrefab,
            transform.position + new Vector3(0f, 1f, -6f), Quaternion.identity, transform);
        var collider  = instance.GetComponentInChildren<CircleCollider2D>().gameObject;
        collider.SetActive(false);
        var material = instance.GetComponent<Renderer>().material;
        material.SetFloat("_FresnelPower", 0);
        var initialLocalScale = transform.localScale;
        
        var time = 1f;
        var fresnelPower = 0f;
        while (time > 0)
        {
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
            fresnelPower  = Mathf.Min(10f, fresnelPower + 7f * Time.fixedDeltaTime);
            material.SetFloat("_FresnelPower", fresnelPower);
        }
        
        collider.SetActive(true);
        time = 0.5f;
        while (time > 0)
        {
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
            transform.localScale -= initialLocalScale * Time.fixedDeltaTime * 2f;
        }

        transform.position = transportPosition;
        time = 0.5f;
        while (time > 0)
        {
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
            transform.localScale += initialLocalScale * Time.fixedDeltaTime * 2f;
        }

        transform.localScale = initialLocalScale;
        collider.gameObject.SetActive(false);
        time = 1f;
        while (time > 0)
        {
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
            fresnelPower = Mathf.Max(0f, fresnelPower - 7f * Time.fixedDeltaTime);
            material.SetFloat("_FresnelPower", fresnelPower);
        }

        quickTeleportActive = false;
        Destroy(instance);
    }
    private IEnumerator SpellAnimation()
    {
        var instance = Instantiate(damageRae, transform.position + new Vector3(0, 1f), Quaternion.identity, transform);
        var spriteRenderer = instance.GetComponent<SpriteRenderer>();
        var color = Color.blue;
        color.a = 0.5f;
        spriteRenderer.color = color;
        instance.transform.localScale *= 1.2f;

        var time = 0.5f;
        while (time > 0)
        {
            color.a -= Time.deltaTime / 2f;
            spriteRenderer.color = color;
            instance.transform.localScale *= 1.01f;

            time -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(instance);
    }
}
//
// Summary:
//     tasta 0 pentru MainSpell; tasta 1 pentru FireBolt
//     Atacul se face pe o singura directie (ultima directie indicata de jucator (dintre dreapta, drapta sus, sus, stanga sus, stanga) defaultul este dreapta)
//     Doar primul inamic este afectat pe o distanta de X unitati (3 si 3 momentan)
//     Fiecare spell are un cooldown si un manaCost asociat
//     Mana se regenereaza singura in acest script, ulterior poate fi modificat
//     trebuie activat Gizmos in Game Tab in Unity ca sa se vada liniile desenate
// public class PlayerSpells : MonoBehaviour
// {
//     public float coolDownFireBolt = 10f; // in seconds
//     public float coolDownMainSpell = 5f; // in seconds
//     public float costFireBolt = 50f;
//     public float costMainSpell = 30f;
//     public float damageMainSpell = 30f;
//     public float damageFireBolt = 50f;
//     public float distanceMainSpell = 3f;
//     public float distanceFireBolt = 3f;
//     public float mana = 100f;
//     public float manaRegenerateSpeed = 10f; // 10 unitati din 100 pe secunda
//     private float timeNextFireBolt;
//     private float timeNextMainSpell;
//     private Vector2 attackDirection;
//
//     private void Start()
//     {
//         attackDirection = Vector2.right;
//     }
//
//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Alpha0))
//         {
//             if (Time.time >= timeNextMainSpell && mana >= costMainSpell)
//             {
//                 timeNextMainSpell = Time.time + coolDownMainSpell;
//                 MainSpell();
//                 Debug.Log($"MainSpell: mana left: {mana}, time: {Time.time}, next: {timeNextMainSpell}");
//             }
//             else
//             {
//                 Debug.Log($"Cooldown: next: {timeNextMainSpell}, time: {Time.time} OR mana to low: mana: {mana}");
//             }
//         }
//
//         if (Input.GetKeyDown(KeyCode.Alpha1))
//         {
//             if (Time.time >= timeNextFireBolt && mana >= costFireBolt)
//             {
//                 timeNextFireBolt = Time.time + coolDownFireBolt;
//                 FireBolt();
//                 Debug.Log($"FireBolt: mana left: {mana}, time: {Time.time}, next: {timeNextFireBolt}");
//             }
//             else
//             {
//                 Debug.Log($"Cooldown: next: {timeNextFireBolt}, time: {Time.time} OR mana to low: mana: {mana}");
//             }
//         }
//
//         // auto regenerate mana: Subject to change
//         mana += manaRegenerateSpeed * Time.deltaTime;
//         if (mana > 100f)
//             mana = 100f;
//
//         GetDirection();
//     }
//
//     private void MainSpell()
//     {
//         mana -= costMainSpell;
//         DrawLines(Color.cyan, distanceMainSpell);
//         HitEnemy(damageMainSpell, distanceMainSpell);
//     }
//
//     private void FireBolt()
//     {
//         mana -= costFireBolt;
//         DrawLines(Color.red, distanceFireBolt);  // TODO: mecanicile si animatiile
//         HitEnemy(damageFireBolt, distanceFireBolt);
//     }
//
//     private void HitEnemy(float damage, float distance)
//     {
//         var start = new Vector2(transform.position.x, transform.position.y);
//
//         // needs the player to be in Layer IgnoreRayCast and Enemy to be in any other layer. 
//         var hit = Physics2D.Raycast(start, attackDirection, distance);
//
//         var enemy = hit.transform?.GetComponent<Enemy>();
//         if (enemy != null)
//         {
//             Debug.Log("Hit enemy");
//             enemy.hitPoints -= damage;
//             if (enemy.hitPoints <= 0)
//             {
//                 // TODO: disable enemy not to be hit after death before it is destroyed
//                 enemy.hitPoints = 0;
//                 Destroy(enemy.gameObject, 1f);
//             }
//
//             var components = enemy.gameObject.GetComponentsInChildren<Transform>();
//
//             var hpBar = components.FirstOrDefault(component => component.tag == "HP");
//             if (hpBar != null)
//                 hpBar.localScale = new Vector3(enemy.hitPoints / 100, hpBar.localScale.y, hpBar.localScale.z);
//         }
//         else
//         {
//             Debug.Log("Miss");
//         }
//     }
//
//     private void DrawLines(Color color, float distance)
//     {
//         for (var angle = -30; angle <= 30; angle += 15)
//             Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, angle) * attackDirection * distance, color, 2f);
//     }
//
//     private void GetDirection()
//     {
//         var x = Input.GetAxisRaw("Horizontal");
//         var y = Input.GetAxisRaw("Vertical");
//         if (y < 0)
//             y = 0;
//
//         if (x != 0 || y != 0)
//             attackDirection = new Vector2(x, y).normalized;
//     }
// }