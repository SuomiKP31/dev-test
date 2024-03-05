using AO;
using Assembly.scripts;

public partial class MyPlayer : Player
{
    // Sync vars may be used inside components to replicate data from the server to clients.
    public SyncVar<int> Score = new();
    public SyncVar<int> Resource = new();

    // Player Upgrade
    public SyncVar<int> Atk = new();
    public SyncVar<float> Multiplier = new();
    public override void Start()
    {
        if (Network.IsServer)
        {
            Atk.Set(1);
            Multiplier.Set(1f);
        }
    }

    public override void Update()
    {
        // Input functions on the player are callable from the client and server.
        // But we want the server to be authoritative, so we only want to process input on the server.
        if (Network.IsServer && this.IsInputDown(Input.UnifiedInput.KEYCODE_SPACE))
        {
            // Setting a value on a sync var (from the server) will automatically replicate to all clients.
            Score.Set(Score + 1);
            CallClient_UpdateClientUI(); // The server stub is not included in the IDE which is really annoying...
        }
    }

    [ClientRpc]
    public void UpdateClientUI()
    {
        UIManager.Instance.UpdateUIEvent.Invoke(this);
    }
}