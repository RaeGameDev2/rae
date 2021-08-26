using System.Collections;
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

    private bool canDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        weapons = GetComponent<WeaponsHandler>();
        skills = GetComponent<PlayerSkills>();
        playerSpells = GetComponent<PlayerSpells>();
        direction = Direction.right;
        diagonalJump = false;
        isGrounded = true;
        currGravity = gravity;
        canDoubleJump = false;
        prevVelocityY = 0;
        canDamage = true;
    }

    private void Start()
    {
        anim.SetFloat("speed", groundSpeed / 7);
        jumpGravity = (jumpSpeed * jumpSpeed) / (2 * jumpHeight);
    }

    private void Update()
    {
        HandleInput();
        ChangeDirection();
        UpdateAnimation();

        if (weapons.currWeapon.type != Weapon.WeaponType.Scythe &&
            weapons.currWeapon.attackType != Weapon.AttackType.None)
            for (var i = -4; i < 5; i++)
            {
                var start = new Vector2(transform.position.x, transform.position.y + i);
                // LayerMask mask = LayerMask.GetMask("Enemy");
                var hit = Physics2D.Raycast(start, direction == Direction.left ? Vector2.left : Vector2.right, 6f);
                Debug.DrawLine(start, start + (direction == Direction.left ? Vector2.left : Vector2.right) * 6f,
                    Color.red, 2f);
                var enemy = hit.transform?.GetComponent<Enemy>();
                // Debug.Log(hit.transform?.tag);
                if (enemy == null) continue;
                OnAttackHit(enemy.GetComponent<Collider2D>());
                break;
            }
    }

    private void FixedUpdate()
    {
        //Reset gravity to falling gravity
        if (rb.velocity.y < 0)
            currGravity = gravity;

        //If not in dash, handles jump
        if (!playerSpells.quickTeleportActive)
        {
            if (animState != State.ATTACK)
            {
                // Debug.Log(hInput);
                CheckJump();
                if (isGrounded)
                {
                    rb.velocity = new Vector2(hInput * groundSpeed, rb.velocity.y);
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
            rb.AddForce(currGravity * Vector2.down, ForceMode2D.Force);
        }
        if (rb.velocity.y < 0 && rb.velocity.y > -fallSpeed)
            rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
        //Save previous y-velocity for adding fallSpeed
        prevVelocityY = rb.velocity.y;
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
        if (hInput > 0.01f && rb.velocity.x > 0)
        {
            if (direction == Direction.left)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                direction = Direction.right;
            }
        }
        else if (hInput < -0.01f && rb.velocity.x < 0)
        {
            if (direction == Direction.right)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                direction = Direction.left;
            }
        }
    }

    private void UpdateAnimation()
    {
        switch (weapons.currWeapon.attackType)
        {
            case Weapon.AttackType.None:
                {
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

        anim.SetInteger("weapon", (int)weapons.currWeapon.type);
        anim.SetInteger("state", (int)animState);
        anim.SetInteger("type", (int)weapons.currWeapon.attackType);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        if (rb.velocity.x != 0)
            diagonalJump = true;
        currGravity = jumpGravity;
        SoundManagerScript.playJumpSound = true;
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
        //  Debug.Log("se terminat animatia!");
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
        ATTACK
    }
}