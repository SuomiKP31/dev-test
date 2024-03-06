namespace Assembly.scripts;
using AO;
public class UIManager : System<UIManager>
{
    // Update using event when you need to. This should avoid fetching references each frame which causes a slight overhead
    public Action<MyPlayer> UpdateUIEvent;

    private string _scoreTxt;
    private string _resourceTxt;

    public override void Awake()
    {
        UpdateUIEvent = delegate(MyPlayer player) {  };
        UpdateUIEvent += UpdateUI;
    }
    
    public override void Start()
    {
        _scoreTxt = "0";
        _resourceTxt = "0";

        UpdateUI((MyPlayer)Network.LocalPlayer);
    }
    
    public void UpdateUI(MyPlayer player)
    {
        if (player == null)
        {
            return;
        }
        _scoreTxt = player.Score.ToString();
        _resourceTxt = player.Resource.ToString();
    }

    public override void Update()
    {
        
        // Draw the score
        {
            var topBarRect = UI.ScreenRect.CutTop(80);
            var currencyRect = topBarRect.CutLeft(225).Offset(550, -10);
            UI.Image(currencyRect, null, Vector4.White);
            UI.Text(currencyRect, $"Score: {_scoreTxt}", new UI.TextSettings() { font = UI.TextSettings.Font.BarlowBold, size = 40, color = Vector4.Black, verticalAlignment = UI.TextSettings.VerticalAlignment.Center, horizontalAlignment = UI.TextSettings.HorizontalAlignment.Center });
            // Draw resources
            var resourceRect = topBarRect.CutLeft(225).Offset(550, -10);
            UI.Image(resourceRect, null, Vector4.White);
            UI.Text(resourceRect, $"Material: {_resourceTxt}", new UI.TextSettings() { font = UI.TextSettings.Font.BarlowBold, size = 40, color = Vector4.Black, verticalAlignment = UI.TextSettings.VerticalAlignment.Center, horizontalAlignment = UI.TextSettings.HorizontalAlignment.Center });
            
        }
        
        {
            
        }
        // Draw the side buttons
        {
            var sideBarRect = UI.ScreenRect.LeftCenterRect().Grow(110, 100, 110, 0).Offset(5, 0);

            var buttonRect = sideBarRect.CutTop(100);
            if (UI.Button(buttonRect, "Upgrade", new UI.ButtonSettings() { sprite = Assets.GetAsset<Texture>("$AO/white.png") }, new UI.TextSettings() { font = UI.TextSettings.Font.BarlowBold, size = 24, color = Vector4.Black }).clicked)
            {
                Log.Info("I'm upgrading a stat!");
            }

            // Spacing
            sideBarRect.CutTop(10);

            var buttonRect2 = sideBarRect.CutTop(100);
            if (UI.Button(buttonRect2, "Upgrade2", new UI.ButtonSettings() { sprite = Assets.GetAsset<Texture>("$AO/white.png") }, new UI.TextSettings() { font = UI.TextSettings.Font.BarlowBold, size = 24, color = Vector4.Black }).clicked)
            {
                Log.Info("I'm upgrading another stat!");
            }
        }
    }
}