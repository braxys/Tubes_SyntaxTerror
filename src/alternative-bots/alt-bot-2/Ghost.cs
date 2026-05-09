using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;

public class Ghost : Bot
{
    // The main method starts our bot
    static void Main(string[] args)
    {
        new Ghost().Start();
    }

    // Constructor, which loads the bot config file
    Ghost() : base(BotInfo.FromFile("Ghost.json")) { }

    // Called when a new round is started -> initialize and do some movement
    public override void Run()
    {

        BodyColor   = Color.FromArgb(0x55, 0x55, 0x66); // Dark Gray
        TurretColor = Color.FromArgb(0x22, 0x22, 0x2E); // Shadow Black
        RadarColor  = Color.FromArgb(0xAA, 0xAA, 0xFF); // Pale Purple
        BulletColor = Color.FromArgb(0xCC, 0xCC, 0xFF); // Ghost White
        ScanColor   = Color.FromArgb(0x88, 0x88, 0xFF); // Soft Indigo
        TracksColor = Color.FromArgb(0x11, 0x11, 0x11); // Pitch Black
        GunColor    = Color.FromArgb(0x66, 0x66, 0x88); // Smoky Blue

        // Repeat while the bot is running
        while (IsRunning)
        {
            // Write your bot logic
        }
    }
}