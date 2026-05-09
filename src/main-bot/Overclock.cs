using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;

public class Overclock : Bot
{
    // The main method starts our bot
    static void Main(string[] args)
    {
        new Overclock().Start();
    }

    // Constructor, which loads the bot config file
    Overclock() : base(BotInfo.FromFile("Overclock.json")) { }

    // Called when a new round is started -> initialize and do some movement
    public override void Run()
    {

        BodyColor   = Color.FromArgb(0x9D, 0x00, 0xFF); // Neon Purple
        TurretColor = Color.FromArgb(0x5A, 0x00, 0x99); // Dark Violet
        RadarColor  = Color.FromArgb(0xFF, 0x00, 0xFF); // Magenta
        BulletColor = Color.FromArgb(0x00, 0xFF, 0xFF); // Plasma Cyan
        ScanColor   = Color.FromArgb(0xFF, 0x55, 0xFF); // Pink Neon
        TracksColor = Color.FromArgb(0x18, 0x18, 0x18); // Carbon Black
        GunColor    = Color.FromArgb(0xC0, 0xC0, 0xFF); // Energy Silver

        // Repeat while the bot is running
        while (IsRunning)
        {
            // Write your bot logic
        }
    }
}