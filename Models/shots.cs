using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfAppBackend.Models
{
    public class Shot
    {
        [Key]
        public long shotId { get; set; }

        // 外部キー: RoundHole
        [Required]
        public long roundHoleId { get; set; }
        [ForeignKey("roundHoleId")]
        public RoundHole RoundHole { get; set; }

        // 何打目か
        [Required]
        public int shotNumber { get; set; }

        // ショットの飛距離（ヤード）
        [Required]
        public double distance { get; set; }

        // 使用したクラブ
        [Required]
        public string clubUsed { get; set; }

        // ボールがどこに行ったか
        [Required]
        public string ballDirection { get; set; }

        // ショットの種類
        [Required]
        public string shotType { get; set; }

        // ボールの弾道の高さ
        [Required]
        public string ballHeight { get; set; }

        // 追加提案: ライの状態（フェアウェイ、ラフ、バンカーなど）
        [Required]
        public string lie { get; set; }

        // 追加提案: 天候条件（晴れ、雨、風など）
        public string weatherConditions { get; set; }

        // 追加提案: ショットの結果（成功、失敗など）
        public string shotResult { get; set; }

        // 追加提案: ショットに関するメモ（自由記述）
        public string notes { get; set; }
    }
}
