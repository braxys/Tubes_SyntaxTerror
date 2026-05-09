using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;

public class Rhino : Bot
{
    // The main method starts our bot
    static void Main(string[] args)
    {
        new Rhino().Start();
    }

    // Constructor, which loads the bot config file
    RHino() : base(BotInfo.FromFile("Rhino.json")) { }

    // Called when a new round is started -> initialize and do some movement
    public override void Run()
    {

        BodyColor   = Color.FromArgb(0x70, 0x70, 0x70); // Steel Gray
        TurretColor = Color.FromArgb(0x3A, 0x3A, 0x3A); // Gunmetal
        RadarColor  = Color.FromArgb(0xFF, 0x66, 0x00); // Aggressive Orange
        BulletColor = Color.FromArgb(0xFF, 0x22, 0x22); // Crimson Red
        ScanColor   = Color.FromArgb(0xFF, 0xAA, 0x00); // Amber
        TracksColor = Color.FromArgb(0x10, 0x10, 0x10); // Black
        GunColor    = Color.FromArgb(0x99, 0x99, 0x99); // Metallic Silver

        // Repeat while the bot is running
        while (IsRunning)
        {
            // Write your bot logic
        }
    }
}