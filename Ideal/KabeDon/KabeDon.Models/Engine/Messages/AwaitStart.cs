namespace KabeDon.Engine.Messages
{
    /// <summary>
    /// 開始ボタンが押されるのを待つ。
    /// </summary>
    public class AwaitStart : Message
    {
        private AwaitStart() { }

        public static readonly AwaitStart Instance = new AwaitStart();
    }
}
