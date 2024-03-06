using AO;

namespace Assembly.scripts;

/// <summary>
/// A damageable crate component. Interactable. Respawn after a few seconds once destroyed. 
/// </summary>
public class Crate : Component
{
    [Serialized] protected int health;
    [Serialized] protected int reward;

    
    protected Sprite_Renderer rdr;
    
    protected int curHealth;
    protected bool alive;

    protected float respawnTimer = 0;
    
    [Serialized]
    protected float respawnTime = 10;
    
    protected Interactable interactable;

    protected Vector2 originalPos;
    /// <summary>
    /// If crate is not dead, it can be interacted.
    /// </summary>
    /// <param name="p">MyPlayer class</param>
    /// <returns></returns>
    public bool CanBeDamaged(Player p)
    {
        return alive;
    }

    /// <summary>
    /// Note: This is probably not the right way to do it. Damage and crate status is calculated locally
    /// I've encountered a weird problem, where if I return a SyncVar<bool> in CanBeDamaged(), the interaction prompt does not pop up
    /// Player SyncVars work just fine though, could be my oversight...
    /// </summary>
    /// <param name="p"></param>
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
            // Log.Error("Crate Dead");
            alive = false;
            rdr.Tint = new Vector4(0,0,0,0);
            GiveReward(mp);
        }
    }


    public override void Start()
    {
        alive = true;
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
                alive = true;
                curHealth = health;
                rdr.Tint = new Vector4(1,1,1,1);
                respawnTimer = 0;
            }
        }
    }
    
    protected void GiveReward(MyPlayer mp)
    {
        mp.Resource.Set(mp.Resource + reward);
        mp.CallClient_UpdateClientUI();
    }
}