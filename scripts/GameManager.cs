using AO;
using Assembly.scripts;

public class GameManager : System<GameManager>
{
    public override void Awake()
    {
        Leaderboard.RegisterSortCallback((Player[] players) => {
            Array.Sort(players, (a, b) => {
                MyPlayer p1 = (MyPlayer)a;
                MyPlayer p2 = (MyPlayer)b;
                int p1s = p1.Score;
                int p2s = p2.Score;
                return p2s.CompareTo(p1s);
            });
        });
            
        Leaderboard.Register($"Scores", (players, strings) =>
        {
            for (int i = 0; i < players.Length; i++)
            {
                strings[i] = ((MyPlayer)players[i]).Score.ToString();
            }
        });
    }

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