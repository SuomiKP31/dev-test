using AO;
namespace Assembly.scripts;

public class ResourceShop : Component
{
    //protected Interactable interactable;

    public override void Start()
    {
        //interactable = Entity.GetComponent<Interactable>();
        //interactable.CanUseCallback = HaveAnyResources;
        //interactable.OnInteract = ExchangeResourceForPlayer;
    }

    public bool HaveAnyResources(Player p)
    {
        var mp = p as MyPlayer;
        if (mp == null)
        {
            return false;
        }

        return mp.Resource > 0;
    }
    
    public void ExchangeResourceForPlayer(Player p)
    {
        var mp = p as MyPlayer;
        if (mp == null)
        {
            return;
        }
        float multiplier = mp.Multiplier;
        mp.Score.Set(mp.Score + (int) (multiplier*mp.Resource));
        mp.Money.Set(mp.Money + (int) (multiplier*mp.Resource));
        mp.Resource.Set(0);
        mp.CallClient_UpdateClientUI(); 
    }
    
    [ClientRpc]
    public void srpc()
    {
        
    }
    
}