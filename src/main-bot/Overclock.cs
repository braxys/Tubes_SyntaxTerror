using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System;
using System.Drawing;

public class Overclock : Bot
{
    string targetId = null;
    double minEnergy = 1000;
    int moveDirection = 1;

    static void Main(string[] args) { new Overclock().Start(); }
    Overclock() : base(BotInfo.FromFile("Overclock.json")) { }

    public override void Run()
    {
        BodyColor   = Color.FromArgb(0x9D, 0x00, 0xFF); 
        TurretColor = Color.FromArgb(0x5A, 0x00, 0x99); 
        RadarColor  = Color.FromArgb(0xFF, 0x00, 0xFF); 
        BulletColor = Color.FromArgb(0x00, 0xFF, 0xFF); 
        ScanColor   = Color.FromArgb(0xFF, 0x55, 0xFF); 
        TracksColor = Color.FromArgb(0x18, 0x18, 0x18); 
        GunColor    = Color.FromArgb(0xC0, 0xC0, 0xFF); 

        while (IsRunning)
        {
            // Trik Greedy: Reset batas energi setiap putaran radar.
            // Ini memaksa Overclock SELALU mencari bot yang paling sekarat di seluruh arena.
            minEnergy = 1000; 
            TurnRadarRight(360);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        // ==========================================
        // FUNGSI SELEKSI: Greedy by Minimum Energy
        // ==========================================
        if (targetId == null || e.Energy < minEnergy || e.ScannedBotId.ToString() == targetId)
        {
            targetId = e.ScannedBotId.ToString();
            minEnergy = e.Energy;

            double targetDir = DirectionTo(e.X, e.Y);
            
            // ==========================================
            // FUNGSI KELAYAKAN: Tembakan Akurat & Hemat
            // ==========================================
            double gunTurn = CalcDeltaAngle(GunDirection, targetDir);
            TurnGunRight(gunTurn);

            // Cek kelayakan: Hanya tembak jika sudah lurus (< 10 derajat)
            if (Math.Abs(gunTurn) <= 10)
            {
                double bulletPower = 1.5; // Power standar
                if (minEnergy < 5) bulletPower = 0.1; // Cuma butuh senggolan kecil buat nyampah kill

                if (Energy > 5) Fire(bulletPower);
            }

            // ==========================================
            // PERGERAKAN: Evasion Standar (Strafing)
            // ==========================================
            // Karena fokus mencari musuh sekarat, gerakan cukup mengorbit tegak lurus (90 derajat)
            // agar tidak gampang tertembak bot tipe Tracker.
            double bearing = CalcBearing(targetDir);
            TurnRight(bearing + 90);
            
            // Pergerakan mikro agar radar tidak nge-blank
            Forward(50 * moveDirection);
        }
    }

    public override void OnHitWall(HitWallEvent e)
    {
        moveDirection *= -1;
        Forward(50 * moveDirection);
    }

    public override void OnHitBot(HitBotEvent e)
    {
        moveDirection *= -1;
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {
        // Kalau kena tembak, refleks ganti arah orbit
        moveDirection *= -1;
    }
}