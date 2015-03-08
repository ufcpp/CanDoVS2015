namespace KabeDon.Common
{
    /// <summary>
    /// 応答を持つメッセージ。
    /// <see cref="Channel{TMessage}.SendAsync{TResult}(TMessage, System.Threading.CancellationToken)"/> で、結果を返してほしいメッセージを送るとき、そのメッセージに実装する。
    /// 結果は <see cref="Response"/> に入れてもらう。
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IResponsiveMessage<TResponse>
    {
        TResponse Response { get; set; }
    }
}
