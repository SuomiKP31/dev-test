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

        // Draw the score
        {
            var topBarRect = UI.ScreenRect.CutTop(80);
            var currencyRect = topBarRect.CutLeft(225).Offset(550, -10);
            UI.Image(currencyRect, null, Vector4.White);
            UI.Text(currencyRect, localPlayer.Score.ToString(), new UI.TextSettings() { font = UI.TextSettings.Font.BarlowBold, size = 40, color = Vector4.Black, verticalAlignment = UI.TextSettings.VerticalAlignment.Center, horizontalAlignment = UI.TextSettings.HorizontalAlignment.Center });
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