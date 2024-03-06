using AO;

namespace Assembly.scripts;

/// <summary>
/// A damageable crate component. Interactable. Respawn after a few seconds once destroyed. 
/// </summary>
public class Crate : Component
{
    [Serialized] protected int health;
    [Serialized] protected int reward;

    protected int curHealth;
    protected Sprite_Renderer rdr;

    protected SyncVar<bool> alive = new();

    protected float respawnTimer = 0;
    
    [Serialized]
    protected float respawnTime = 10;
    
    protected Interactable interactable;
    /// <summary>
    /// If crate is not dead, it can be interacted.
    /// </summary>
    /// <param name="p">MyPlayer class</param>
    /// <returns></returns>
    public bool CanBeDamaged(Player p)
    {
        if (!alive)
        {
            Log.Warn("Shouldn't happen!");
        }
        return (bool)alive; //&& Vector2.Distance(p.Entity.Position, Entity.Position) < 2f;
    }

    public void TakeDamage(Player p)
    {
        var mp = p as MyPlayer;
        if (mp == null)
        {
            Log.Error("Wrong object passed in");
            return;
        }
        curHealth -= mp.Atk;

        if (curHealth <= 0)
        {
            alive.Set(false);
            rdr.Enabled = false;
            GiveReward(mp);
        }
    }


    public override void Start()
    {
        alive.Set(true);
        curHealth = health;
        rdr = Entity.GetComponent<Sprite_Renderer>();
        interactable = Entity.GetComponent<Interactable>();
        
        interactable.OnInteract = TakeDamage;
        interactable.CanUseCallback = CanBeDamaged;
        
        //Network.Spawn(this.Entity); // Shouldn't need to call this when NetWorked is checked.
    }
    

    public override void Update()
    {
        // Respawn Logic
        if (!alive)
        {
            respawnTimer += Time.DeltaTime;
            if (respawnTimer > respawnTime)
            {
                // Respawn
                alive.Set(true);
                rdr.Enabled = true;
                respawnTimer = 0;
            }
        }
    }
    
    protected void GiveReward(MyPlayer mp)
    {
        mp.Resource.Set(mp.Resource + reward);
    }
}