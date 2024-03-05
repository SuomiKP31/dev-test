using AO;

public class GameManager : System<GameManager>
{
    public override void Start()
    {
    }

    public override void Update()
    {
        var localPlayer = (MyPlayer) Network.LocalPlayer;

        // The local player is null on the server
        if (localPlayer == null)
        {
            return;
        }
    }
}