using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GolfAppBackend.Models
{
    [Table("shots")]
    public class Shot
    {
        [Key]
        public long shotId { get; set; }

        // 外部キー: RoundHole
        [Required]
        public long roundHoleId { get; set; }

        [ForeignKey("roundHoleId")]
        [JsonIgnore] // 循環参照を防ぐために追加
        public RoundHole RoundHole { get; set; }

        // 何打目か
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Shot number must be a positive integer.")]
        public int shotNumber { get; set; }

        // ショットの飛距離（ヤード）
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Distance must be a non-negative integer.")]
        public int distance { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Remaining distance must be a non-negative integer.")]
        public int remainingDistance { get; set; }

        // 使用したクラブ
        [Required]
        public String clubUsed { get; set; }

        // ボールがどこに行ったか
        [Required]
        public String ballDirection { get; set; }

        // ショットの種類
        [Required]
        public String shotType { get; set; }

        // ボールの弾道の高さ
        [Required]
        public String ballHeight { get; set; }

        // ライの状態
        [Required]
        public String lie { get; set; }

        // ショットの結果
        public String? shotResult { get; set; }

        // ショットに関するメモ（自由記述）
        public string notes { get; set; }
    }
}
