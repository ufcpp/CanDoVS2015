using System.Threading.Tasks;

namespace KabeDon.Common
{
    /// <summary>
    /// <see cref="Channel{TMessage}"/> のメッセージの非同期ハンドラー。
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    /// <param name="message"></param>
    /// <returns></returns>
    public delegate Task AsyncChannelHandler<TMessage>(TMessage message);

    /// <summary>
    /// <see cref="Channel{TMessage}"/> のメッセージのハンドラー。
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    /// <param name="message"></param>
    /// <returns></returns>
    public delegate void ChannelHandler<TMessage>(TMessage message);
}
