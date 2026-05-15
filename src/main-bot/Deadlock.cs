using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;

public class Deadlock : Bot
{
    private string targetName = null;
    private double minDistance = 10000;

    static void Main(string[] args) { new Deadlock().Start(); }
    Deadlock() : base(BotInfo.FromFile("Deadlock.json")) { }

    public override void Run()
    {
        BodyColor   = Color.FromArgb(0x00, 0xC8, 0xFF); 
        TurretColor = Color.FromArgb(0x00, 0x7A, 0xCC); 
        RadarColor  = Color.FromArgb(0x80, 0xF0, 0xFF); 
        BulletColor = Color.FromArgb(0xFF, 0xF5, 0xF5); 
        ScanColor   = Color.FromArgb(0x00, 0xFF, 0xCC); 
        TracksColor = Color.FromArgb(0x1A, 0x1A, 0x1A); 
        GunColor    = Color.FromArgb(0xD9, 0xD9, 0xD9); 

        while (IsRunning)
        {
            TurnRadarLeft(360);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);

        if (targetName == null || distance < minDistance || e.ScannedBotId.ToString() == targetName)
        {
            targetName = e.ScannedBotId.ToString();
            minDistance = distance;

            double targetDir = DirectionTo(e.X, e.Y);
            
            // Putar meriam ke arah musuh dengan sudut paling efisien
            double gunTurn = CalcDeltaAngle(GunDirection, targetDir); 
            TurnGunRight(gunTurn);
            
            if (distance < 200) Fire(3);
            else Fire(1.5);

            // Putar tank ke arah musuh dan pepet terus
            double bodyTurn = CalcBearing(targetDir);
            TurnRight(bodyTurn);
            Forward(distance - 50); 
        }
    }

    public override void OnBotDeath(BotDeathEvent e)
    {
        if (e.VictimId.ToString() == targetName)
        {
            targetName = null;
            minDistance = 10000;
        }
    }
}