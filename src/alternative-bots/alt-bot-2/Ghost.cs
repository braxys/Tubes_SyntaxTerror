using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System;
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

        AdjustRadarForGunTurn = true;
        AdjustGunForBodyTurn = true;

        while (IsRunning)
        {
            TurnRadarRight(360);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double enemyDir = DirectionTo(e.X, e.Y);
        double distance = DistanceTo(e.X, e.Y);

        // Fungsi Seleksi: Membidik musuh dan menembak dengan energi minimum (Power 1)
        double gunTurn = CalcGunBearing(enemyDir);
        TurnGunRight(gunTurn);
        
        // Fungsi Kelayakan: Hanya menembak jika laras meriam sejajar dengan musuh dan dingin
        if (Math.Abs(gunTurn) < 5 && GunHeat == 0) 
        {
            Fire(1); 
        }

        // Fungsi Seleksi: Memilih rute yang memaksimalkan jarak dari ancaman
        double bodyTurn = CalcBearing(enemyDir);
        double evasiveAngle;

        if (distance < 400)
        {
            // Jika musuh terlalu dekat, lari serong (110 derajat) untuk menjauh
            evasiveAngle = bodyTurn + 110;
        }
        else
        {
            // Jika musuh cukup jauh, bergerak tegak lurus (90 derajat) untuk menghindari peluru
            evasiveAngle = bodyTurn + 90;
        }

        TurnRight(evasiveAngle); 
        
        CheckWallCollision();

        // Bergerak maju/mundur dengan kecepatan acak (patah-patah) agar sulit dibidik
        Forward(60 * moveDirection);
    }

    public override void OnHitWall(HitWallEvent e)
    {
        // Fungsi Kelayakan: Jika menabrak dinding balik arah
        moveDirection *= -1;
        Forward(100 * moveDirection);
    }

    // Fungsi tambahan untuk Fungsi Kelayakan
    private void CheckWallCollision()
    {
        double margin = 50; // Jarak aman dari dinding
        if (X < margin || X > 800 - margin || Y < margin || Y > 600 - margin)
        {
            // Secara serakah langsung membalik arah jika mendeteksi dinding untuk menghindari Wall Damage
            moveDirection *= -1;
        }
    }
}