# Syntax Terror — Robocode Tank Royale Bot

## Algoritma Greedy yang Diimplementasikan

### Bot Utama - Overclock *(Main Bot)*

**Fungsi Objektif:** Memaksimalkan total skor (Bullet Damage + Kills + Ramming + Survival)

Overclock menerapkan greedy berlapis dengan tiga prioritas keputusan pada setiap giliran:

| Prioritas | Kondisi | Aksi Greedy |
|-----------|---------|-------------|
| 1 | Musuh dekat (`< 120`) & energi musuh rendah (`< 15`) | **Ramming** — tabrak langsung untuk bonus poin 30% |
| 2 | Musuh jauh (`> 150`) | **Kejar** — maju agresif ke arah musuh |
| 3 | Jarak menengah | **Strafe** — bergerak menyamping 90° untuk menghindari peluru |

**Greedy Firepower:** Daya tembak dipilih secara serakah berdasarkan jarak:
- Jarak `≤ 150` → `Fire(3.0)` (maksimum)
- Jarak `150–300` → `Fire(2.0)` (menengah)
- Jarak `> 300` → `Fire(1.0)` (minimum, hemat energi)

**Target Selection:** Selalu memilih target terdekat secara greedy (min-distance selection) agar kerusakan terakumulasi cepat pada satu lawan.

---

### Alt-Bot 1 — Deadlock

**Strategi:** *Greedy Minimum Distance + Full Fire*

Deadlock mengunci satu target secara permanen dan terus mendekatinya tanpa henti. Setiap giliran, bot memilih aksi yang paling segera menghasilkan damage:

- Selalu membidik dan mengejar target dengan jarak terdekat.
- Menembak dengan `Fire(3.0)` saat jarak `< 200`, dan `Fire(1.5)` saat lebih jauh.
- Tidak melakukan evasion — semua sumber daya dialokasikan untuk output damage maksimal.

---

### Alt-Bot 2 — Ghost

**Strategi:** *Greedy Minimum Energy + Maximum Evasion*

Ghost menerapkan filosofi bertahan hidup: menembak seminimal mungkin dan selalu memilih jalur yang paling jauh dari ancaman.

- Hanya menembak dengan `Fire(1)` — konsumsi energi minimum per tembakan.
- Tembakan hanya dilepaskan saat meriam sudah sejajar akurat (`|gunTurn| < 5°`) dan dingin, menghindari pemborosan energi.
- **Greedy Evasion:** Jika musuh `< 400`, lari serong 110°; jika lebih jauh, bergerak tegak lurus 90°.
- Langsung membalik arah saat mendeteksi kedekatan dinding (sebelum tertabrak).

---

### Alt-Bot 3 — Rhino

**Strategi:** *Greedy Maximum Ramming*

Rhino mengabaikan jarak dan taktik halus. Setiap giliran, bot langsung melakukan keputusan paling agresif yang tersedia:

- Secara serakah maju ke arah musuh dengan jarak `distance + 50` piksel ekstra untuk **menjamin terjadinya Ramming**.
- Hanya menembak pada jarak dekat (`< 150`) dengan `Fire(3.0)` untuk memaksimalkan damage point.
- Radar terus berputar penuh untuk selalu melacak musuh terdekat.

---

## Requirements & Instalasi

### Prasyarat

| Kebutuhan | Versi |
|-----------|-------|
| [.NET SDK](https://dotnet.microsoft.com/download) | 10.0 |
| [Robocode Tank Royale](https://robocode-dev.github.io/tank-royale/) | Terbaru |

### Instalasi .NET SDK

**Windows / macOS / Linux:**
```bash
# Cek apakah .NET sudah terinstal
dotnet --version

# Jika belum, unduh dari:
# https://dotnet.microsoft.com/download
```

### Instalasi Robocode Tank Royale

1. Unduh Robocode Tank Royale GUI dari [halaman rilis resmi](https://github.com/robocode-dev/tank-royale/releases).
2. Jalankan file `.jar` menggunakan Java:
   ```bash
   java -jar robocode-tankroyale-gui-x.x.x.jar
   ```
3. Tambahkan direktori bot ke **Bot Root Directories** di pengaturan Robocode.

---

## Cara Menjalankan Bot

```cmd
cd src/main-bot
Overclock.cmd
```

## Kendala saat Development

- **Sinkronisasi radar-gun-body:** Tanpa `AdjustRadarForGunTurn = true` dan `AdjustGunForBodyTurn = true`, perputaran body menyebabkan meriam dan radar ikut bergerak sehingga aiming tidak akurat.
- **Wall damage:** Bot agresif seperti Deadlock dan Rhino sering menabrak dinding. Solusi: menambahkan pengecekan `CheckWallCollision()` berbasis margin koordinat.

---

## Authors

| Nama | Peran |
|------|-------|
| **Bagas Hari Muthi** | 124140128 |
| **Arief Fandi Satria** | 124140212 |
| **Arya Dimar Fath** | 124140020 |

> Tim: **SyntaxTerror**
> Bahasa: C# (.NET)
> Platform: Robocode Tank Royale
