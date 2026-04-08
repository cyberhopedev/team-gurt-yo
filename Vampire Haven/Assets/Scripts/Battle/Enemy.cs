using UnityEngine;

/// <summary>
/// Parent/abstract class for all of the Enemies specified in order to function
/// </summary>
public abstract class Enemy : MonoBehaviour, IDamageable
{
    // Enemy stats and how much gold they drop
    [SerializeField]
    public int health;
    [SerializeField]
    public float speed;
    [SerializeField]
    public int defense;
    [SerializeField]
    public int gold;

    // Movement during the battle towards the target
    [SerializeField]
    protected Transform pointA, pointB;
    protected Vector3 movementTarget;

    // For animation of sprite
    protected Animator anim;
    protected SpriteRenderer rend;

    [SerializeField]
    protected Transform player;

    public void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        // Setup animator
        anim = GetComponentInChildren<Animator>();
        if(anim == null)
        {
            Debug.LogError(transform.name + " Animator is NULL or not implemented");
        }

        // Set up renderer
        rend = GetComponentInChildren<SpriteRenderer>();
        if(rend == null)
        {
            Debug.LogError(transform.name + " Sprite Renderer is NULL or not implemented");
        }
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void Move()
    {
        
    }

    protected virtual void Attack()
    {
        
    }

    /// <summary>
    /// Subtracts the amount of damage the current battler takes.
    /// </summary>
    /// <param name="damage">The amount of damage inflicted to the battler</param>
    public void TakeDamage(int damage)
    {
        
    }

    /// <summary>
    /// Reports if the battler is dead or alive, in order to be dead
    /// the battler's health must be 0.
    /// </summary>
    /// <returns>True if the battler is dead, false if otherwise</returns>
    public bool IsDead()
    {
        return false;
    }

}