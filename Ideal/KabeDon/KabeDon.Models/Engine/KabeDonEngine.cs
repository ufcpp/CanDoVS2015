using KabeDon.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace KabeDon.Engine
{
    using M = Messages;
    using D = DataModels;

    /// <summary>
    /// ゲームの実行エンジン。
    /// </summary>
    public class KabeDonEngine : BindableBase
    {
        private readonly D.Level _level;

        public KabeDonEngine(D.Level lavel)
        {
            _level = lavel;
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
        public Channel<M.Message> Channel { get; } = new Channel<M.Message>();

        /// <summary>
        /// 実行。
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync(CancellationToken ct)
        {
            var cts = new CancellationTokenSource();
            ct.Register(() => cts.Cancel());

            var state = _level.GetInitialState();
            Image = state.Image;

            await Channel.SendAsync(M.AwaitStart.Instance);

            IsRunning = true;
            await Channel.SendAsync(new M.PlaySound(_level.StartSound));

            try
            {
                await Task.WhenAny(
                    CountDown(ct).ContinueWith(_ => cts.Cancel()),
                    ExecuteGame(cts.Token)
                    );

                await Channel.SendAsync(new M.PlaySound(_level.FinishSound));
            }
            finally
            {
                IsRunning = false;
            }
        }

        /// <summary>
        /// 残り時間カウント。
        /// </summary>
        /// <returns></returns>
        private async Task CountDown(CancellationToken ct)
        {
            var timeLimit = DateTime.Now.AddSeconds(_level.TimeLimitSeconds);

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
        /// <param name="endOfGame"></param>
        /// <returns></returns>
        private async Task ExecuteGame(CancellationToken endOfGame)
        {
            var state = _level.GetInitialState();
            var random = new Random();
            var timeout = Task.Delay(state.TimeFrame.Next(random));

            while (!endOfGame.IsCancellationRequested)
            {
                var tap = Channel.SendAsync<D.Point>(new M.AwaitTap());

                await Task.WhenAny(tap, timeout);

                if (timeout.IsCompleted)
                {
                    System.Diagnostics.Debug.WriteLine($"timeout");
                    Transit(state.Transition, random, ref state, ref timeout);
                    continue;
                }

                var tapped = await tap;
                var area = state.GetArea(tapped);

                if (area == null)
                {
                    await Channel.SendAsync(new M.PlaySound(state.Sound));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"tapped: {tapped}, area: {area.Rect}, {area.Description}");

                    await Channel.SendAsync(new M.PlaySound(area.Sound));
                    Score += area.Score;

                    Transit(area.Transition, random, ref state, ref timeout);

                    //todo: クールタイムの間、何か表示だしたい
                    await Task.Delay(area.CoolTime.Next(random));
                }
            }
        }

        private void Transit(D.Probability.Table<string> transition, Random random, ref D.State state, ref Task timeout)
        {
            var next = transition.GetOccurrence(random);

            if (next == null) return;

            state = _level.GetState(next);
            System.Diagnostics.Debug.WriteLine($"state changed to {state}");
            Image = state.Image;
            timeout = Task.Delay(state.TimeFrame.Next(random));
        }
    }
}
