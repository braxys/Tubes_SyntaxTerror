using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;

public class Deadlock : Bot
{
    // The main method starts our bot
    static void Main(string[] args)
    {
        new Deadlock().Start();
    }

    // Constructor, which loads the bot config file
    Deadlock() : base(BotInfo.FromFile("Deadlock.json")) { }

    // Called when a new round is started -> initialize and do some movement
    public override void Run()
    {

        BodyColor   = Color.FromArgb(0x00, 0xC8, 0xFF); // Neon Cyan
        TurretColor = Color.FromArgb(0x00, 0x7A, 0xCC); // Deep Blue
        RadarColor  = Color.FromArgb(0x80, 0xF0, 0xFF); // Ice Cyan
        BulletColor = Color.FromArgb(0xFF, 0xF5, 0xF5); // White
        ScanColor   = Color.FromArgb(0x00, 0xFF, 0xCC); // Aqua Green
        TracksColor = Color.FromArgb(0x1A, 0x1A, 0x1A); // Matte Black
        GunColor    = Color.FromArgb(0xD9, 0xD9, 0xD9); // Silver

        // Repeat while the bot is running
        while (IsRunning)
        {
            // Write your bot logic
        }
    }
}