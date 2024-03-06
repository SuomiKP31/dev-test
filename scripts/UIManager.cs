namespace Assembly.scripts;
using AO;
public class UIManager : System<UIManager>
{
    // Update using event when you need to. This should avoid fetching references each frame which causes a slight overhead
    public Action<MyPlayer> UpdateUIEvent;
    public Action<string, float, Player> PopupEvent;

    private string _scoreTxt;
    private string _resourceTxt;
    private string _atkTxt;
    private string _multiplierTxt;
    private string _moneyTxt;

    private string _popupTxt;
    private float _popupRemainingTime;
    public override void Awake()
    {
        PopupEvent = SetPopup;
        UpdateUIEvent = UpdateUI;
    }
    
    public override void Start()
    {
        _scoreTxt = "0";
        _resourceTxt = "0";
        _moneyTxt = "0";

        UpdateUI((MyPlayer)Network.LocalPlayer);
    }
    

    public void UpdateUI(MyPlayer player)
    {
        if (player == null || Network.LocalPlayer == null)
        {
            return;
        }

        if (!player.IsLocal)
        {
            return;
        }
        _scoreTxt = player.Score.ToString();
        _resourceTxt = player.Resource.ToString();
        _atkTxt = player.Atk.ToString();
        _multiplierTxt = player.Multiplier.ToString();
        _moneyTxt = player.Money.ToString();
    }

    
    public void SetPopup(string txt, float time, Player player)
    {
        if (!player.IsLocal)
        {
            return;
        }
        _popupRemainingTime = time;
        _popupTxt = txt;
    }

    public override void Update()
    {
        // Update timers
        {
            if (_popupRemainingTime > 0)
            {
                //Log.Warn(_popupTxt);
                _popupRemainingTime -= Time.DeltaTime;
                // Draw the popup
                {
                    var centerRect = UI.ScreenRect.CenterRect().Grow(235).CutBottom(50);
                    UI.Text(centerRect, $"{_popupTxt}", new UI.TextSettings() { font = UI.TextSettings.Font.BarlowBold, size = 24, color = Vector4.Black, 
                        verticalAlignment = UI.TextSettings.VerticalAlignment.Center, horizontalAlignment = UI.TextSettings.HorizontalAlignment.Center,
                        wordWrap = true, outline = true, outlineColor = Vector4.White
                    });
                }
            }
            else
            {
                _popupRemainingTime = 0;
                _popupTxt = "";
            }
        }
        
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
            // Draw money
            var moneyRect = topBarRect.CutLeft(225).Offset(550, -10);
            UI.Image(moneyRect, null, Vector4.White);
            UI.Text(moneyRect, $"Money: {_moneyTxt}", new UI.TextSettings() { font = UI.TextSettings.Font.BarlowBold, size = 40, color = Vector4.Black, verticalAlignment = UI.TextSettings.VerticalAlignment.Center, horizontalAlignment = UI.TextSettings.HorizontalAlignment.Center });
        }
        
        {
            
        }
        // Draw the side buttons
        {
            var sideBarRect = UI.ScreenRect.LeftCenterRect().Grow(110, 100, 110, 0).Offset(5, 0);

            var buttonRect = sideBarRect.CutTop(100);
            if (UI.Button(buttonRect, $"+Atk: {_atkTxt}", new UI.ButtonSettings() { sprite = Assets.GetAsset<Texture>("$AO/white.png") }, new UI.TextSettings() { font = UI.TextSettings.Font.BarlowBold, size = 24, color = Vector4.Black }).clicked)
            {
                var player = (MyPlayer)Network.LocalPlayer;
                Log.Info("I'm upgrading a stat!");
                player.CallServer_UpgradeAtk();
            }

            // Spacing
            sideBarRect.CutTop(10);

            var buttonRect2 = sideBarRect.CutTop(100);
            if (UI.Button(buttonRect2, $"+Mtp: {_multiplierTxt}", new UI.ButtonSettings() { sprite = Assets.GetAsset<Texture>("$AO/white.png") }, new UI.TextSettings() { font = UI.TextSettings.Font.BarlowBold, size = 24, color = Vector4.Black }).clicked)
            {
                var player = (MyPlayer)Network.LocalPlayer;
                Log.Info("I'm upgrading another stat!");
                player.CallServer_UpgradeMtp();
            }
            
        }
        
        
    }
}