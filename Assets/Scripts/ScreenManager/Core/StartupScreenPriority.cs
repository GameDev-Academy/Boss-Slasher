/*
[0] bonus wheel (existing user with available bonus wheel)
[1] Shop purchase recovering from previous incomplete/failed transaction (if any)
[2] deeplink reward popup (if used via Push notification or direct use)
[3] if sweep user - Play sweep video and after user’s action is complete i.e. either complete registration or skip registration.
[4] all in-apps pop up based on the priority set up from Errol
[5] deal pop up (for the available deal for the user)
*/
public enum StartupScreenPriority
{
    ShopRecovery = 900,
    DeepLinkScreen = 800,
    SweepstakePromo = 700,
    ClansSeasonEnd = 696,
    ClansMissions = 695,
    VideoPromo = 690,
    InAppScreen = 600,
    DealScreen = 500,
    LoginScreen = 400,
}

public enum SlotScreensPriority
{
    ActionProcessor = 15000,
    Magnet = 10100,
    LevelUp = 10000,
    JourneyMissionCompleted = 9900,
    JourneySeasonOver = 9800,
    JourneyMissionsGameWelcome = 9700,
    JourneyMissionsWelcome = 9600,
    JourneyMissionsHowToPlay = 9500,
    JourneyMissionsGameOutOfSpins = 9400,
}
