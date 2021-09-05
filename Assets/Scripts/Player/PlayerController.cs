using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float airSpeed;

    private Animator anim;
    private State animState;
    [HideInInspector] public bool canDoubleJump;

    [SerializeField] private float currGravity;
    [HideInInspector] public bool diagonalJump;

    private Direction direction;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpGravity;

    [SerializeField] private float groundSpeed;
    private float hInput;
    public bool isGrounded;

    [SerializeField] private float jumpHeight;

    private bool jumpPressed;
    [SerializeField] private float jumpSpeed;
    [HideInInspector] public bool onIce;
    private bool pause;
    private PlayerSpells playerSpells;
    private float prevVelocityY;


    private Rigidbody2D rb;
    private PlayerSkills skills;
    private WeaponsHandler weapons;
    List<RaycastHit2D> results;
    ContactFilter2D filter2D;

    private bool canDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        weapons = GetComponent<WeaponsHandler>();
        skills = GetComponent<PlayerSkills>();
        playerSpells = GetComponent<PlayerSpells>();
        filter2D = filter2D.NoFilter();
        results = new List<RaycastHit2D>();
        direction = Direction.right;
        diagonalJump = false;
        isGrounded = true;
        currGravity = gravity;
        canDoubleJump = false;
        prevVelocityY = 0;
        canDamage = true;
        animState = State.IDLE;
    }

    private void Start()
    {
        anim.SetFloat("speed", groundSpeed / 7);
        jumpGravity = (jumpSpeed * jumpSpeed) / (2 * jumpHeight);
    }

    private void Update()
    {
        if (pause)
            return;
        if (animState == State.DEATH)
            return;
        HandleInput();
        ChangeDirection();
        UpdateAnimation();

        if (weapons.currWeapon.type != Weapon.WeaponType.Scythe &&
            weapons.currWeapon.attackType != Weapon.AttackType.None)
            for (var i = -4; i < 5; i++)
            {
                var start = new Vector2(transform.position.x, transform.position.y + i);
                var hit = Physics2D.Raycast(start, direction == Direction.left ? Vector2.left : Vector2.right, 6f);
                Debug.DrawLine(start, start + (direction == Direction.left ? Vector2.left : Vector2.right) * 6f,
                    Color.red, 2f);
                var enemy = hit.transform?.GetComponent<Enemy>();
                if (enemy == null) continue;
                OnAttackHit(enemy.GetComponent<Collider2D>());
                break;
            }
    }

    private void FixedUpdate()
    {
        if (pause)
            return;
        if (animState == State.DEATH)
            return;
        //Reset gravity to falling gravity
        if (rb.velocity.y <= 0)
            currGravity = gravity;

        //If not in dash, handles jump
        if (!playerSpells.quickTeleportActive)
        {
            if (animState != State.ATTACK)
            {
                CheckJump();
                if (isGrounded)
                {
                    if (rb.velocity.y >= 0)
                        rb.velocity = new Vector2(hInput * groundSpeed, rb.velocity.y);
                    else
                    {
                        rb.velocity = new Vector2(hInput * groundSpeed, 0);
                        currGravity = 0;
                    }
                }
                else
                {
                    var speed = hInput * airSpeed + rb.velocity.x;
                    if (diagonalJump)
                        speed = Mathf.Clamp(speed, -groundSpeed, groundSpeed);
                    else
                        speed = Mathf.Clamp(speed, -airSpeed, airSpeed);
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            //Add simulated gravity
            // rb.AddForce(currGravity * Vector2.down, ForceMode2D.Force);

            var velocityY = rb.velocity.y - currGravity * Time.fixedDeltaTime;
            rb.velocity = new Vector2(rb.velocity.x, velocityY);

        }
        if (!isGrounded && rb.velocity.y < 0 && rb.velocity.y > -fallSpeed)
            rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
        if (isGrounded && rb.velocity.y < jumpSpeed / 2)
        {
            currGravity = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        // rb.velocity = Vector2.zero;
    }

    //Polls input every frame and updates flags accordingly
    private void HandleInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && animState != State.ATTACK)
            jumpPressed = true;
    }

    //Flip player when changing direction
    private void ChangeDirection()
    {
        if (playerSpells.quickTeleportActive)
            return;
        if (animState == State.ATTACK)
            return;
        if (hInput > 0.01f && direction == Direction.left)
        {
            transform.localScale =
                new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            direction = Direction.right;
        }
        else if (hInput < -0.01f && direction == Direction.right)
        {
            transform.localScale =
                new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            direction = Direction.left;
        }
    }

    private void UpdateAnimation()
    {
        switch (weapons.currWeapon.attackType)
        {
            case Weapon.AttackType.None:
                {
                    if (animState != State.LAND)
                        if (Mathf.Abs(rb.velocity.x) < groundSpeed)
                            animState = State.IDLE;
                        else
                            animState = State.RUN;
                    break;
                }
            case Weapon.AttackType.Basic:
                {
                    var attackSpeed = weapons.currWeapon.attackSpeed +
                                      weapons.currWeapon.bonusAttackSpeed * skills.GetLevelAttackSpeed();
                    anim.SetFloat("attackSpeed", attackSpeed / 100);
                    animState = State.ATTACK;
                    break;
                }
            case Weapon.AttackType.Heavy:
                {
                    var attackSpeed = weapons.currWeapon.attackSpeed +
                                      weapons.currWeapon.bonusAttackSpeed * skills.GetLevelAttackSpeed();
                    anim.SetFloat("attackSpeed", attackSpeed / 200);
                    animState = State.ATTACK;
                    break;
                }
        }
        if (animState != State.LAND && animState != State.ATTACK)
            if (rb.velocity.y < -0.1)
                animState = State.FALL;
            else if (rb.velocity.y > 0.1)
                animState = State.JUMP;
        if (animState == State.FALL)
        {
            Physics2D.Raycast(transform.position, Vector2.down, filter2D, results, 5f);
            foreach (RaycastHit2D hit in results)
            {
                if (hit.collider.tag == "Ground")
                {
                    animState = State.LAND;
                }
            }
        }
        anim.SetInteger("state", (int)animState);
        anim.SetInteger("weapon", (int)weapons.currWeapon.type);
        anim.SetInteger("type", (int)weapons.currWeapon.attackType);

        // Debug.Log($"{animState} {weapons.currWeapon.type}    anim state {anim.GetInteger("state")} anim weapon {anim.GetInteger("weapon")}");
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        if (rb.velocity.x != 0)
            diagonalJump = true;
        currGravity = jumpGravity;
        SoundManagerScript.instance.PlaySound(SoundManagerScript.SoundType.JUMP);
    }

    private void CheckJump()
    {
        if (jumpPressed)
        {
            jumpPressed = false;

            if (isGrounded)
            {
                Jump();
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }
    }

    public void Pause()
    {
        pause = !pause;
    }

    public void OnAttackEnd()
    {
        weapons.currWeapon.attackType = Weapon.AttackType.None;
        animState = State.IDLE;
        canDamage = true;
    }

    public void OnLandEnd()
    {
        animState = State.IDLE;
    }

    public void OnAttackHit(Collider2D col)
    {
        if (!canDamage) return;
        var enemy = col.GetComponent<Enemy>();
        var rng = Random.Range(0, 101);
        var critRate = weapons.currWeapon.critRate + weapons.currWeapon.bonusCritRate * skills.GetLevelCritRate();
        var critDmg = weapons.currWeapon.critDmg + weapons.currWeapon.bonusCritDmg * skills.GetLevelCritBonus();
        var attackSpeed = weapons.currWeapon.attackSpeed +
                          weapons.currWeapon.bonusAttackSpeed * skills.GetLevelAttackSpeed();

        var attackDmg = weapons.currWeapon.attackType switch
        {
            Weapon.AttackType.Basic => weapons.currWeapon.mainDamage +
                                       weapons.currWeapon.bonusAttackDmg * skills.GetLevelAttack(),
            Weapon.AttackType.Heavy => weapons.currWeapon.secondaryDamage +
                                       weapons.currWeapon.bonusAttackDmg * skills.GetLevelAttack(),
            _ => 0
        };

        var crit = rng <= critRate;
        if (crit)
            attackDmg += critDmg;

        enemy.OnDamageTaken(attackDmg, crit);

        if (playerSpells.lifeDrainActive)
            enemy.LifeDrain(skills.GetLevelLifeDrain());

        if (playerSpells.debuffActive)
        {
            enemy.Debuff(skills.GetLevelDebuff());
            playerSpells.StopDebuff();
        }
        canDamage = false;
    }

    private enum Direction
    {
        left,
        right
    }

    private enum State
    {
        IDLE,
        RUN,
        ATTACK,
        JUMP,
        FALL,
        LAND,
        DEATH
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public IEnumerator activateDeath()
    {
        animState = State.DEATH;
        anim.SetInteger("state", (int)animState);
        rb.velocity = Vector2.zero;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        GameManager.instance.Die();
    }

    public void deactivateDeath()
    {
        animState = State.IDLE;
        anim.SetInteger("state", (int)animState);
        GetComponent<CapsuleCollider2D>().enabled = true;
    }
}