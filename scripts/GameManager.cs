using AO;
using Assembly.scripts;

public class GameManager : System<GameManager>
{
    public override void Start()
    {
        Player.OnPlayerJoin = PlayerJoin;
        Player.OnPlayerLeave = PlayerLeave;
    }

    public override void Update()
    {
        /*var localPlayer = (MyPlayer) Network.LocalPlayer;

        // The local player is null on the server
        if (localPlayer == null)
        {
            return;
        }*/
    }

    protected void PlayerJoin(Player player)
    {
        Log.Info("Player Join");
        UIManager.Instance.UpdateUIEvent.Invoke((MyPlayer)Network.LocalPlayer);
    }

    protected void PlayerLeave(Player player)
    {
        Log.Info("Player Left");
        
        UIManager.Instance.UpdateUIEvent.Invoke((MyPlayer)Network.LocalPlayer);
    }
}