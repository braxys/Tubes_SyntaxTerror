using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;
using System.Drawing;

namespace OverclockBot
{
    public class Overclock : Bot
    {
        // FUNGSI OBJEKTIF: Memaksimalkan Total Skor (Bullet Damage + Kills + Ramming + Survival)
        private string targetName = null;
        private double minDistance = 10000;
        private int moveDirection = 1;

        static void Main(string[] args) { new Overclock().Start(); }
        Overclock() : base(BotInfo.FromFile("Overclock.json")) { }

        public override void Run()
        {
            // Memisahkan pergerakan bagian tank agar tidak membuang giliran tempur
            AdjustRadarForGunTurn = true;
            AdjustGunForBodyTurn = true;

            // Kustomisasi warna tank
            BodyColor = Color.FromArgb(0x11, 0x11, 0x11);
            TurretColor = Color.FromArgb(0xFF, 0x00, 0x00);
            RadarColor = Color.FromArgb(0xFF, 0x45, 0x00);
            BulletColor = Color.FromArgb(0xFF, 0xD7, 0x00);

            while (IsRunning)
            {
                TurnRadarRight(360); // Terus memutar radar untuk mencari target
            }
        }

        public override void OnScannedBot(ScannedBotEvent e)
        {
            double distanceToEnemy = DistanceTo(e.X, e.Y);

            // Fokus pada target terdekat secara serakah agar damage terakumulasi cepat
            if (targetName == null || distanceToEnemy < minDistance || e.ScannedBotId.ToString() == targetName)
            {
                targetName = e.ScannedBotId.ToString();
                minDistance = distanceToEnemy;

                // 1. TARGETING & AIM
                double targetDir = DirectionTo(e.X, e.Y);
                double gunTurn = CalcDeltaAngle(GunDirection, targetDir); 
                TurnGunRight(gunTurn);

                // 2. FUNGSI KELAYAKAN & EKSEKUSI TEMBAKAN (GREEDY FIREPOWER)
                // Tembak secara serakah (daya besar di jarak dekat, daya kecil di jarak jauh)
                if (GunHeat == 0 && Energy > 1.0)
                {
                    double firePower = 3.0; // Daya tembak maksimum
                    if (distanceToEnemy > 300) firePower = 1.0;
                    else if (distanceToEnemy > 150) firePower = 2.0;

                    Fire(firePower);
                }

                // 3. FUNGSI SELEKSI PERGERAKAN
                double bodyTurn = CalcBearing(targetDir);

                // Prioritas 1: Ramming Target Sekarat (Bonus Poin 30%)
                if (distanceToEnemy < 120 && e.Energy < 15)
                {
                    TurnRight(bodyTurn); // Hadap musuh
                    Forward(60 * moveDirection); // Tabrak
                }
                // Prioritas 2: Pengejaran Brutal jika musuh menjauh
                else if (distanceToEnemy > 150)
                {
                    TurnRight(bodyTurn); 
                    CheckWallCollision();
                    Forward(40 * moveDirection);
                }
                // Prioritas 3: Menghindar ke samping jika jarak dekat
                else
                {
                    TurnRight(bodyTurn + 90); 
                    CheckWallCollision();
                    Forward(40 * moveDirection);
                }
            }
        }

        public override void OnBotDeath(BotDeathEvent e)
        {
            // Jika target mati, reset ingatan bot agar memburu target baru
            if (e.VictimId.ToString() == targetName)
            {
                targetName = null;
                minDistance = 10000;
            }
        }

        // Fungsi Menghindari Wall Damage
        private void CheckWallCollision()
        {
            double safeMargin = 50.0;
            if (X < safeMargin || X > 800 - safeMargin || Y < safeMargin || Y > 600 - safeMargin)
            {
                moveDirection *= -1; // Balikkan arah jika terlalu dekat dengan tembok
            }
        }

        public override void OnHitWall(HitWallEvent e)
        {
            moveDirection *= -1; // Mundur jika darurat menabrak
        }
    }
}