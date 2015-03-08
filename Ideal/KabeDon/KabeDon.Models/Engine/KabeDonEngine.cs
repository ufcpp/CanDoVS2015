using KabeDon.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KabeDon.Engine
{
    using System.Reactive.Linq;
    using M = Messages;
    using D = DataModels;

    public class KabeDonEngine : BindableBase
    {
        public KabeDonEngine()
        {
            StartCommand = _channel.SetCommand<M.Message, M.AwaitStart>();
            TapCommand = _channel.SetCommand<M.Message, M.AwaitTap, D.Point>();
            SoundRequested = _channel.AsObservable<M.Message, M.PlaySound>().Select(x => x.Name);
        }

        /// <summary>
        /// 現在のスコア。
        /// </summary>
        public int Score
        {
            get { return _score; }
            set { SetValue(ref _score, value); }
        }
        private int _score;

        /// <summary>
        /// 背景画像。
        /// </summary>
        public string Image
        {
            get { return _image; }
            set { SetValue(ref _image, value); }
        }
        private string _image;

        /// <summary>
        /// 残り時間。
        /// </summary>
        public TimeSpan RemainderTime
        {
            get { return _remainderTime; }
            set { SetValue(ref _remainderTime, value); }
        }
        private TimeSpan _remainderTime;

        /// <summary>
        /// 実行中。
        /// </summary>
        public bool IsRunning
        {
            get { return _running; }
            set { SetValue(ref _running, value); }
        }
        private bool _running;

        // Channel 生で返すか、コマンド介すべきか悩む。
        private readonly Channel<M.Message> _channel = new Channel<M.Message>();

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

        /// <summary>
        /// 実行。
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(Packages.PackageManager manager, CancellationToken ct)
        {
            var cts = new CancellationTokenSource();
            ct.Register(() => cts.Cancel());

            var state = manager.Level.GetInitialState();
            var image = manager.GetImage(state.Image);
            Image = image;

            await _channel.SendAsync(M.AwaitStart.Instance);

            IsRunning = true;
            await _channel.SendAsync(new M.PlaySound(manager.Level.StartSound));

            try
            {
                await Task.WhenAny(
                    CountDown(manager, ct).ContinueWith(_ => cts.Cancel()),
                    ExecuteGame(manager, cts.Token)
                    );

                await _channel.SendAsync(new M.PlaySound(manager.Level.FinishSound));
            }
            finally
            {
                IsRunning = false;
            }
        }

        /// <summary>
        /// 残り時間カウント。
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        private async Task CountDown(Packages.PackageManager manager, CancellationToken ct)
        {
            var timeLimit = DateTime.Now.AddSeconds(manager.Level.TimeLimitSeconds);

            TimeSpan remainder;
            while (!ct.IsCancellationRequested && (remainder = timeLimit - DateTime.Now) > TimeSpan.Zero)
            {
                RemainderTime = remainder;
                await Task.Delay(TimeSpan.FromSeconds(0.5));
            }
        }

        /// <summary>
        /// ゲーム本体。
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="endOfGame"></param>
        /// <returns></returns>
        private async Task ExecuteGame(Packages.PackageManager manager, CancellationToken endOfGame)
        {
            var state = manager.Level.GetInitialState();
            var random = new System.Random();

            while (true)
            {
                var tapped = await _channel.SendAsync<D.Point>(new M.AwaitTap());
                var area = state.GetArea(tapped);

                if (area == null)
                {
                    await _channel.SendAsync(new M.PlaySound(state.Sound));
                }
                else
                {
                    await _channel.SendAsync(new M.PlaySound(area.Sound));
                    Score += area.Score;

                    //todo: クールタイムの間何か表示だしたい
                    await Task.Delay(area.CoolTime.Next(random));
                }
            }
        }
    }
}
