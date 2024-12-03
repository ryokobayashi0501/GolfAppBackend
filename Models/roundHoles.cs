using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GolfAppBackend.Models
{
    [Table("roundHoles")]
    public class RoundHole
    {
        [Key]
        public long roundHoleId { get; set; }

        // 外部キー: Round
        [Required]
        public long roundId { get; set; }

        [ForeignKey("roundId")]
        [JsonIgnore] // 循環参照を防ぐために追加
        public Round Round { get; set; }

        // 外部キー: Hole
        [Required]
        public long holeId { get; set; }

        [ForeignKey("holeId")]
        [JsonIgnore] // 循環参照を防ぐために追加
        public Hole Hole { get; set; }

        // そのホールでの合計打数
        [Required]
        public int stroke { get; set; }

        // パット数を記録するカラム
        public int? putts { get; set; }

        // ペナルティ打数
        public int? penaltyStrokes { get; set; }

        // 天候の条件
        public string weatherConditions { get; set; }

        // バンカーに入った回数
        public int? bunkerShotsCount { get; set; } // バンカーショットの回数を保持

        // バンカーからのリカバリー成功の有無
        public bool? bunkerRecovery { get; set; }

        // スクランブル挑戦の有無
        public bool? scrambleAttempted { get; set; }

        // スクランブル成功の有無
        public bool? scrambleSuccess { get; set; }

        // フェアウェイキープ率を記録するカラム
        public bool? fairwayHit { get; set; }

        // パーオン率を記録するカラム
        public bool? greenInRegulation { get; set; }

        // ナビゲーションプロパティ: Shots
        [JsonIgnore] // 循環参照を防ぐために追加
        public ICollection<Shot> Shots { get; set; } = new List<Shot>();
    }
}
