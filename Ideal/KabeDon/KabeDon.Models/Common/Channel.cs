using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace KabeDon.Common
{
    /// <summary>
    /// 非同期メッセージ送受信チャネル。
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public class Channel<TMessage>
    {
        ConcurrentDictionary<Type, AsyncChannelHandler<TMessage>> _listeners = new ConcurrentDictionary<Type, AsyncChannelHandler<TMessage>>();

        /// <summary>
        /// メッセージを送って応答を待つ。
        /// <see cref="IResponsiveMessage{TResponse}"/> を実装してないとダメ。
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="message"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<TResult> SendAsync<TResult>(TMessage message, CancellationToken ct = default(CancellationToken))
        {
            AsyncChannelHandler<TMessage> listener;
            if (!_listeners.TryGetValue(message.GetType(), out listener)) return default(TResult);

            if (ct != default(CancellationToken))
            {
                await Task.WhenAny(
                    listener(message),
                    AwaitCancel(ct)
                    );
                if (ct.IsCancellationRequested) return default(TResult);
            }
            else
            {
                await listener(message);
            }

            return ((IResponsiveMessage<TResult>)message).Response;
        }

        /// <summary>
        /// メッセージを送って応答を待つ。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task SendAsync(TMessage message, CancellationToken ct = default(CancellationToken))
        {
            AsyncChannelHandler<TMessage> listener;
            if (!_listeners.TryGetValue(message.GetType(), out listener)) return;

            if (ct != default(CancellationToken))
            {
                await Task.WhenAny(
                    listener(message),
                    AwaitCancel(ct)
                    );
                if (ct.IsCancellationRequested) return;
            }
            else
            {
                await listener(message);
            }
        }

        private Task AwaitCancel(CancellationToken ct)
        {
            var tcs = new TaskCompletionSource<bool>();
            ct.Register(() => tcs.TrySetResult(false));
            return tcs.Task;
        }

        /// <summary>
        /// <typeparamref name="T"/> に対するハンドラーを設定する。
        /// 複数回呼んだ場合上書き。
        /// </summary>
        /// <typeparam name="T">メッセージの型。</typeparam>
        /// <param name="handler">ハンドラー。</param>
        public void SetHandler<T>(AsyncChannelHandler<T> handler)
            where T : TMessage
        {
            if (handler == null)
            {
                AsyncChannelHandler<TMessage> dummy;
                _listeners.TryRemove(typeof(T), out dummy);
            }
            else
            {
                _listeners.TryAdd(typeof(T), m => handler((T)m));
            }
        }

        /// <summary>
        /// 同期版。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        public void SetHandler<T>(ChannelHandler<T> handler)
            where T : TMessage
            => SetHandler<T>(m => { handler(m); return Task.FromResult(false); });
    }
}
