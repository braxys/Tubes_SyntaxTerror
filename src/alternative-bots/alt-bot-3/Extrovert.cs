using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;

public class Extrovert : Bot
{
    // The main method starts our bot
    static void Main(string[] args)
    {
        new Extrovert().Start();
    }

    // Constructor, which loads the bot config file
    Extrovert() : base(BotInfo.FromFile("Extrovert.json")) { }

    // Called when a new round is started -> initialize and do some movement
    public override void Run()
    {

        BodyColor = Color.FromArgb(0xCC, 0xFF, 0x00);   // Electric Lime
        TurretColor = Color.FromArgb(0xFF, 0xA5, 0x00); // Vivid Orange
        RadarColor = Color.FromArgb(0x00, 0xFF, 0xFF);  // Cyan
        BulletColor = Color.FromArgb(0xFF, 0x00, 0xFF); // Magenta
        ScanColor = Color.FromArgb(0xFF, 0xFF, 0x00);   // Yellow
        TracksColor = Color.FromArgb(0xFF, 0x69, 0xB4); // Hot Pink
        GunColor = Color.FromArgb(0xFF, 0x00, 0x00);    // Red

        // Repeat while the bot is running
        while (IsRunning)
        {
            // Write your bot logic
        }
    }
}