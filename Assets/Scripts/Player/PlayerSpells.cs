using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    [SerializeField] private GameObject damageRae;
    public bool debuffActive;
    private GameObject instanceShield;
    public bool lifeDrainActive;
    public bool orbDropped;
    public bool parryActive;
    private bool pause;
    public bool phaseWalkActive;
    private PlayerResources playerResources;
    private PlayerSkills playerSkills;
    public bool quickTeleportActive;
    public bool shieldActive;
    public float shieldDamage;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject shockwavePrefab;
    [SerializeField] private GameObject teleportPrefab;
    [SerializeField] private float timeLifeDrain = 5f;
    [SerializeField] private float timeParry = 1f;
    [SerializeField] private float timePhaseWalk = 5f;
    private Vector3 transportPosition;
    private WeaponsHandler weaponsHandler;

    private SpriteRenderer scytheRendererAttack;
    private SpriteRenderer scytheRendererAttack2;
    private SpriteRenderer scytheRendererIdle;
    [SerializeField] private Sprite scythe;
    [SerializeField] private Sprite scytheBloodDrain;

    private void Awake()
    {
        playerSkills = GetComponent<PlayerSkills>();
        weaponsHandler = GetComponent<WeaponsHandler>();
        playerResources = GetComponent<PlayerResources>();

        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        scytheRendererIdle = spriteRenderers.FirstOrDefault(obj => obj.transform.name == "Scythe_001");
        scytheRendererAttack2 = spriteRenderers.FirstOrDefault(obj => obj.transform.name == "Scythe");
        scytheRendererAttack = spriteRenderers.FirstOrDefault(obj => obj.transform.name == "Scythe_000");

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
        if (Input.GetKeyDown(KeyCode.Q)) 
            PhaseWalk();
        CheckShield();

        if (!Input.GetKeyDown(KeyCode.Alpha1)) return;
        if (playerResources.currentMana <= 0) return;
        if (quickTeleportActive) return;
        if (shieldActive) return;
        if (lifeDrainActive) return;
        if (parryActive) return;
        if (phaseWalkActive) return;
        switch (weaponsHandler.currWeapon.type)
        {
            case Weapon.WeaponType.Scythe:
                if (playerSkills.IsLifeDrainUnlocked())
                {
                    LifeDrain();
                }
                else if (playerSkills.IsParryUnlocked())
                {
                    Parry();
                }

                break;
            case Weapon.WeaponType.Orb:
                if (playerSkills.IsQuickTpUnlocked())
                {
                    QuickTP();
                }
                else if (playerSkills.IsShieldUnlocked())
                {
                    Shield();
                }

                break;
            case Weapon.WeaponType.Staff:
                if (playerSkills.IsPhaseWalkUnlocked())
                {
                    PhaseWalk();
                }
                else if (playerSkills.IsDebuffUnlocked())
                {
                    Debuff();
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void LifeDrain()
    {
        lifeDrainActive = true;
        scytheRendererAttack.sprite = scytheBloodDrain;
        scytheRendererAttack2.sprite = scytheBloodDrain;
        scytheRendererIdle.sprite = scytheBloodDrain;
        StartCoroutine(StopLifeDrain());
        StartCoroutine(SpellAnimation());
        playerResources.UseMana();
    }
    private void Parry()
    {
        parryActive = true;
        StartCoroutine(StopParry());
        StartCoroutine(SpellAnimation());
        playerResources.UseMana();
    }

    private void QuickTP()
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

    private void Shield()
    {
        shieldActive = true;
        shieldDamage = playerSkills.GetLevelShield();
        instanceShield = Instantiate(shieldPrefab, transform.position + new Vector3(0f, 1f, 2f),
            Quaternion.identity, transform);
        playerResources.UseMana();
    }

    private void PhaseWalk()
    {
        phaseWalkActive = true;
        StartCoroutine(StopPhaseWalk());
        playerResources.UseMana();
    }

    private void Debuff()
    {
        if (debuffActive) return;
        debuffActive = true;
        StartCoroutine(SpellAnimation());
        playerResources.UseMana();
    }
    private void CheckShield()
    {
        if (!shieldActive) return;
        if (shieldDamage != 0) return;
        shieldActive = false;
        Destroy(instanceShield);
    }

    private IEnumerator StopLifeDrain()
    {
        yield return new WaitForSeconds(timeLifeDrain);
        lifeDrainActive = false;

        scytheRendererIdle.sprite = scythe;
        scytheRendererAttack.sprite = scythe;
        scytheRendererAttack2.sprite = scythe;
    }

    private IEnumerator StopParry()
    {
        yield return new WaitForSeconds(timeParry);
        parryActive = false;
    }
    
    private IEnumerator StopPhaseWalk()
    {
        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        var colors = spriteRenderers.Select(spriteRenderer => spriteRenderer.color).ToArray();
        var albedo = 1f;
        const float initialTime = 1f;
        var time = initialTime;
        phaseWalkActive = true;

        while (time > 0)
        {
            albedo -= Time.fixedDeltaTime / (1.5f * initialTime);
            for (var i = 0; i < spriteRenderers.Length; i++)
            {
                colors[i].a = albedo;
                spriteRenderers[i].color = colors[i];
            }

            time -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(timePhaseWalk - 2 * initialTime);

        time = 1f;
        while (time > 0)
        {
            albedo += Time.fixedDeltaTime /(2 * initialTime);
            for (var i = 0; i < spriteRenderers.Length; i++)
            {
                colors[i].a = albedo;
                spriteRenderers[i].color = colors[i];
            }

            time -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        for (var i = 0; i < spriteRenderers.Length; i++)
        {
            colors[i].a = 1f;
            spriteRenderers[i].color = colors[i];
        }

        phaseWalkActive = false;
    }

    private IEnumerator TeleportAnimation()
    {
        quickTeleportActive = true;
        var instance = Instantiate(teleportPrefab,
            transform.position + new Vector3(0f, 1f, 2f), Quaternion.identity, transform);
        var collider = instance.GetComponentInChildren<CircleCollider2D>().gameObject;
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
            fresnelPower = Mathf.Min(10f, fresnelPower + 7f * Time.fixedDeltaTime);
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

    public void Pause()
    {
        pause = !pause;
    }

    public void StopDebuff()
    {
        debuffActive = false;
    }

    public void Shockwave()
    {
        parryActive = false;
        var instance = Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
        instance.GetComponent<Shockwave>().SetLevelParry(playerSkills.GetLevelParry());
        instance.transform.parent = transform;
    }
}