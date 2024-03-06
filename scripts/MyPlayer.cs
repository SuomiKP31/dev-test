using AO;
using Assembly.scripts;

public partial class MyPlayer : Player
{
    // Sync vars may be used inside components to replicate data from the server to clients.
    public SyncVar<int> Score = new();
    public SyncVar<int> Resource = new();
    public SyncVar<int> Money = new();

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
        Leaderboard.RegisterSortCallback((Player[] players) => {
            Array.Sort(players, (a, b) => {
                MyPlayer p1 = (MyPlayer)a;
                MyPlayer p2 = (MyPlayer)b;
                int p1s = p1.Score;
                int p2s = p2.Score;
                return p2s.CompareTo(p1s);
            });
        });

        if (this.IsLocal)
        {
            Leaderboard.Register($"Scores", (players, strings) =>
            {
                for (int i = 0; i < players.Length; i++)
                {
                    strings[i] = ((MyPlayer)players[i]).Score.ToString();
                }
            });
        }
        
        

        /*var rg = Entity.AddComponent<Rigidbody>();
        rg.Velocity = new Vector2(0, 1);*/
        CallClient_UpdateClientUI();
    }

    public override void Update()
    {
        // Input functions on the player are callable from the client and server.
        // But we want the server to be authoritative, so we only want to process input on the server.
        if (Network.IsServer && this.IsInputDown(Input.UnifiedInput.KEYCODE_SPACE))
        {
            // Setting a value on a sync var (from the server) will automatically replicate to all clients.
            Score.Set(Score + 1);
            CallClient_UpdateClientUI(); // The server stub is not included in the IDE...
        }
    }
    

    #region RPCs

    // Client RPCs from UI Managers will be called from all clients.
    [ClientRpc]
    public void UpdateClientUI()
    {
        UIManager.Instance.UpdateUIEvent.Invoke(this);

    }

    [ClientRpc]
    public void SetPopup(string txt, float t)
    {
        UIManager.Instance.PopupEvent.Invoke(txt, t, this);
    }

    [ServerRpc]
    public void UpgradeAtk()
    {
        if (Money > 5)
        {
            Atk.Set(Atk + 1);
            Money.Set(Money - 5);
        }
        else
        {
            CallClient_SetPopup("Need 5 bucks!", 3);
        }
        
        CallClient_UpdateClientUI();
    }

    [ServerRpc]
    public void UpgradeMtp()
    {
        if (Money > 5)
        {
            Multiplier.Set(Multiplier + 0.5f);
            Money.Set(Money - 5);
        }
        else
        {
            CallClient_SetPopup("Need 5 bucks!", 3);
        }
        CallClient_UpdateClientUI();
    }

    #endregion
    
}