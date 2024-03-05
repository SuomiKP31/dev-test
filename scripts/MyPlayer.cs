using AO;

public class MyPlayer : Player
{
    // Sync vars may be used inside components to replicate data from the server to clients.
    public SyncVar<int> Score = new();

    public override void Start()
    {
        
    }

    public override void Update()
    {
        // Input functions on the player are callable from the client and server.
        // But we want the server to be authoritative, so we only want to process input on the server.
        if (Network.IsServer && this.IsInputDown(Input.UnifiedInput.KEYCODE_SPACE))
        {
            // Setting a value on a sync var (from the server) will automatically replicate to all clients.
            Score.Set(Score + 1);
        }
    }
}