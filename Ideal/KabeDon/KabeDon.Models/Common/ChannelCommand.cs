using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Windows.Input;

namespace KabeDon.Common
{
    /// <summary>
    /// <see cref="Channel{TMessage}"/> と <see cref="ICommand"/> のつなぎこみ。
    /// </summary>
    public static class ChannelCommand
    {
        /// <summary>
        /// 戻り値のコマンドが実行されるまで待つよう、<see cref="Channel{TMessage}.SetHandler{T}(AsyncChannelHandler{TMessage})"/> を設定する。
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static ICommand SetCommand<TMessage, T>(this Channel<TMessage> channel)
            where T : TMessage
        {
            var c = new ObservableCommand();
            channel.SetHandler<T>(m => c.FirstAsync().ToTask());
            return c;
        }

        /// <summary>
        /// 戻り値のコマンドが実行されるまで待つよう、<see cref="Channel{TMessage}.SetHandler{T}(AsyncChannelHandler{TMessage})"/> を設定する。
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static ICommand SetCommand<TMessage, T, TResponse>(this Channel<TMessage> channel)
            where T : TMessage, IResponsiveMessage<TResponse>
        {
            var c = new ObservableCommand<TResponse>();
            channel.SetHandler<T>(async m => m.Response = await c.FirstAsync().ToTask());
            return c;
        }

        /// <summary>
        /// <see cref="Channel{TMessage}"/> の <typeparamref name="T"/> に対するハンドリングを <see cref="IObservable{T}"/> 化。
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static IObservable<T> AsObservable<TMessage, T>(this Channel<TMessage> channel)
            where T : TMessage
        {
            var s = new Subject<T>();
            channel.SetHandler<T>(m => s.OnNext(m));
            return s;
        }
    }
}
