using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfAppBackend.Models
{
    public class RoundHole
    {
        [Key]
        public long roundHoleId { get; set; }

        // 外部キー: Round
        [Required]
        public long roundId { get; set; }
        [ForeignKey("roundId")]
        public Round Round { get; set; }

        // 外部キー: Hole
        [Required]
        public long holeId { get; set; }
        [ForeignKey("HoleId")]
        public Hole Hole { get; set; }

        // そのホールでの合計打数
        [Required]
        public int stroke { get; set; }

        // ナビゲーションプロパティ: Shots
        public List<Shot> Shots { get; set; } = new List<Shot>();

        // 追加提案: パット数を記録するカラム
        // パット数はゴルフスコアの重要な要素なので、追加すると分析に役立ちます
        public int? putts { get; set; }

        // 追加提案: ペナルティ打数
        public int? penaltyStrokes { get; set; }

        // 追加提案: フェアウェイキープ率を記録するカラム
        public bool? fairwayHit { get; set; }

        // 追加提案: パーオン率を記録するカラム
        public bool? greenInRegulation { get; set; }
    }
}
