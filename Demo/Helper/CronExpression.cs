namespace Demo.Helper
{
    public class CronExpression
    {
        public const string Every_10_Sec = "0/10 * * * * *";
        public const string Every_1_Min = "* * * * *";
        public const string Every_10_Min = "0 */10 * * * ?";
        public const string Every_15_Min = "0 */15 * * * ?";
        public const string Every_30_Min = "0 */30 * * * ?";
    }
}
