using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;

public class Rhino : Bot
{
    static void Main(string[] args) { new Rhino().Start(); }
    Rhino() : base(BotInfo.FromFile("Rhino.json")) { }

    public override void Run()
    {
        BodyColor   = Color.FromArgb(0x70, 0x70, 0x70); 
        TurretColor = Color.FromArgb(0x3A, 0x3A, 0x3A); 
        RadarColor  = Color.FromArgb(0xFF, 0x66, 0x00); 
        BulletColor = Color.FromArgb(0xFF, 0x22, 0x22); 
        ScanColor   = Color.FromArgb(0xFF, 0xAA, 0x00); 
        TracksColor = Color.FromArgb(0x10, 0x10, 0x10); 
        GunColor    = Color.FromArgb(0x99, 0x99, 0x99); 

        while (IsRunning)
        {
            TurnRadarLeft(360);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double targetDir = DirectionTo(e.X, e.Y);
        double distance = DistanceTo(e.X, e.Y);

        TurnRight(CalcBearing(targetDir));
        Forward(distance);

        TurnGunRight(CalcDeltaAngle(GunDirection, targetDir));
        if (distance < 100)
        {
            Fire(3); 
        }
    }
}