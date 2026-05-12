using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;

public class Ghost : Bot
{
    int moveDirection = 1; 

    static void Main(string[] args) { new Ghost().Start(); }
    Ghost() : base(BotInfo.FromFile("Ghost.json")) { }

    public override void Run()
    {
        BodyColor   = Color.FromArgb(0x55, 0x55, 0x66); 
        TurretColor = Color.FromArgb(0x22, 0x22, 0x2E); 
        RadarColor  = Color.FromArgb(0xAA, 0xAA, 0xFF); 
        BulletColor = Color.FromArgb(0xCC, 0xCC, 0xFF); 
        ScanColor   = Color.FromArgb(0x88, 0x88, 0xFF); 
        TracksColor = Color.FromArgb(0x11, 0x11, 0x11); 
        GunColor    = Color.FromArgb(0x66, 0x66, 0x88); 

        while (IsRunning)
        {
            TurnRadarRight(360);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double targetDir = DirectionTo(e.X, e.Y);
        
        TurnGunRight(CalcDeltaAngle(GunDirection, targetDir));
        Fire(1); 

        // Bergerak tegak lurus (90 derajat) terhadap garis tembak musuh
        double bodyTurn = CalcBearing(targetDir);
        TurnRight(bodyTurn + 90); 
        Forward(150 * moveDirection);
    }

    public override void OnHitWall(HitWallEvent e)
    {
        moveDirection *= -1;
        Forward(100 * moveDirection);
    }
}