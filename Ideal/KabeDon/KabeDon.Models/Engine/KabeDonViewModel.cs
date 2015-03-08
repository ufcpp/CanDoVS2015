using System;
using System.Windows.Input;
using System.Reactive.Linq;

namespace KabeDon.Engine
{
    using Common;
    using Packages;
    using M = Messages;
    using D = DataModels;

    /// <summary>
    /// <see cref="KabeDonEngine"/> に対して、リソースのパス解消したり、コマンド提供したり。
    /// </summary>
    public class KabeDonViewModel : BindableBase
    {
        private readonly PackageManager _manager;

        public KabeDonViewModel(PackageManager manager)
        {
            _manager = manager;
            Engine = new KabeDonEngine(manager.Level);
            Engine.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(Engine.Image))
                    OnPropertyChanged(nameof(Image));
            };

            var channel = Engine.Channel;
            StartCommand = channel.SetCommand<M.Message, M.AwaitStart>();
            TapCommand = channel.SetCommand<M.Message, M.AwaitTap, D.Point>();
            SoundRequested = channel.AsObservable<M.Message, M.PlaySound>().Select(x => manager.GetSound(x.Name));
        }

        /// <summary>
        /// ゲームの実行エンジン。
        /// </summary>
        public KabeDonEngine Engine { get; }

        /// <summary>
        /// リソース パス解消済みの画像ファイル名。
        /// </summary>
        public string Image => _manager.GetImage(Engine.Image);

        /// <summary>
        /// 開始待ち。
        /// </summary>
        public ICommand StartCommand { get; }

        /// <summary>
        /// タップ待ち。
        /// </summary>
        public ICommand TapCommand { get; }

        /// <summary>
        /// 音声再生要求イベント。
        /// </summary>
        public IObservable<string> SoundRequested { get; }
    }
}
