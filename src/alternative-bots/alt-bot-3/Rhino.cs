using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System;
using System.Drawing;

public class Rhino : Bot
{
    static void Main(string[] args) { new Rhino().Start(); }
    Rhino() : base(BotInfo.FromFile("Rhino.json")) { }

    public override void Run()
    {
        // Kustomisasi warna agar bot Anda unik saat kompetisi [cite: 1096]
        BodyColor   = Color.FromArgb(0x4A, 0x00, 0x00); // Merah Darah Gelap
        TurretColor = Color.FromArgb(0x7A, 0x00, 0x00); 
        RadarColor  = Color.FromArgb(0xFF, 0x33, 0x00); 
        BulletColor = Color.FromArgb(0xFF, 0x00, 0x00); 
        ScanColor   = Color.FromArgb(0xFF, 0x22, 0x00); 
        TracksColor = Color.FromArgb(0x11, 0x11, 0x11); 
        GunColor    = Color.FromArgb(0x66, 0x11, 0x11); 

        // WAJIB: Memisahkan putaran radar dan meriam dari badan tank [cite: 839, 840]
        AdjustRadarForGunTurn = true;
        AdjustGunForBodyTurn = true;

        while (IsRunning)
        {
            // Secara serakah memutar radar secepat mungkin untuk mencari mangsa
            TurnRadarRight(360);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        // 1. Kumpulkan Jarak dan Arah Absolut
        double targetDir = DirectionTo(e.X, e.Y);
        double distance = DistanceTo(e.X, e.Y);

        // 2. Kunci Radar (Radar Lock) agar musuh tidak lepas dari pandangan
        double radarTurn = CalcDeltaAngle(RadarDirection, targetDir);
        TurnRadarRight(radarTurn);

        // 3. Bidik Meriam Terlebih Dahulu (Menghindari Action Lag) [cite: 698, 699]
        double gunTurn = CalcDeltaAngle(GunDirection, targetDir);
        TurnGunRight(gunTurn);

        // --- FUNGSI KELAYAKAN (FEASIBILITY) & SELEKSI (FIRING) ---
        // Jika musuh sudah berada di jarak dekat (< 150) dan meriam dingin, 
        // tembakkan peluru maksimal (Power 3) secara serakah.
        if (distance < 150 && GunHeat == 0)
        {
            Fire(3.0);
        }

        // 4. Manuver Ramming (Tabrakan Brutal)
        // Putar badan persis menghadap musuh menggunakan API Bawaan
        TurnRight(CalcBearing(targetDir));

        // --- FUNGSI SELEKSI GREEDY (RAMMING) ---
        // Secara serakah, paksa bot maju dengan jarak musuh + 50 piksel ekstra.
        // Hal ini menjamin terjadinya benturan (Ramming) untuk memanen 
        // 2x Ram Damage dan 30% Ram Bonus[cite: 940, 941].
        Forward(distance + 50);
    }
}