namespace GolfAppBackend.Models.DTOs
{
    public class RoundHoleDto
    {
        public int stroke { get; set; }
        public int? putts { get; set; }
        public int? penaltyStrokes { get; set; }
        public bool? fairwayHit { get; set; }
        public bool? greenInRegulation { get; set; }

        // 天候の条件
        public string weatherConditions { get; set; }

        // バンカーに入った回数
        public int? bunkerShotsCount { get; set; } // バンカーショットの回数

        // バンカーからのリカバリー成功の有無
        public bool? bunkerRecovery { get; set; }

        // スクランブル挑戦の有無
        public bool? scrambleAttempted { get; set; }

        // スクランブル成功の有無
        public bool? scrambleSuccess { get; set; }
    }
}
