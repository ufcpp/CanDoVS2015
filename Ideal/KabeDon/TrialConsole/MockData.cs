using KabeDon.DataModels;
using P = KabeDon.DataModels.Probability;

namespace TrialConsole
{
    class MockData
    {
        public static Level Data => new Level
        {
            TimeLimitSeconds = 2 * 60,
            StartSound = "30start.mp3",
            FinishSound = "16finiiish.mp3",
            InitialState = "normal",
            States =
            {
                new State
                {
                    Id = "normal",
                    Description = @"普通。驚き遷移多め。
壁ドン領域: 2点。遷移なし率高め。
離れた場所: 0点。驚き遷移率高め。
ボディタッチ: 0点。怒り遷移率高め。
離れた場所タッチで驚き(照れ遷移多め)にして、そこから照れにするのが定石。
ほっといて怒ることはない。
",
                    Image = "normal.png",
                    Sound = "drum02.mp3",
                    TimeFrame = TimeFrame.Exponential(3),
                    Transition = new P.Table<string>
                    {
                        Items =
                        {
                            { 20, "normal" },
                            { 1, "embarrassed" },
                            { 5, "surprise" },
                        }
                    },
                    Area =
                    {
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(370, 290, 340, 510),
                            Score = 0,
                            Description = "頭",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 3, null },
                                    { 1, "embarrassed" },
                                    { 1, "surprise" },
                                    { 5, "angry" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(370, 290, 340, 510),
                            Score = 0,
                            Description = "顔",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 3, null },
                                    { 1, "embarrassed" },
                                    { 1, "surprise" },
                                    { 5, "angry" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(280, 420, 520, 340),
                            Score = 2,
                            Description = "壁ドン(良)",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "taiko03.mp3",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 20, null },
                                    { 1, "embarrassed" },
                                    { 1, "surprise" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(350, 970, 450, 270),
                            Score = 0,
                            Description = "胸",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 5, null },
                                    { 10, "angry" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(260, 760, 620, 1160),
                            Score = 0,
                            Description = "胴",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 2, null },
                                    { 1, "embarrassed" },
                                    { 1, "surprise" },
                                    { 4, "angry" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(130, 210, 820, 920),
                            Score = 0,
                            Description = "壁ドン(広)",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "drum02.mp3",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 10, null },
                                    { 1, "embarrassed" },
                                    { 5, "surprise" },
                                },
                            },
                        },
                    },
                },

                new State
                {
                    Id = "surprise",
                    Description = @"驚き表情。照れ遷移多め。
壁ドン領域: 3点。遷移なし率高め。
離れた場所: 1点。照れ遷移率高め。
頭: 0点。照れ遷移率かなり高め。怒らない。
ボディタッチ: 0点。普通遷移率高め。胸だけは怒る。
放置で怒り遷移多め。期待させといて何もしないとかちょっと。
",
                    Image = "surprise.png",
                    Sound = "drum02.mp3",
                    TimeFrame = TimeFrame.Exponential(3),
                    Transition = new P.Table<string>
                    {
                        Items =
                        {
                            { 1, "normal" },
                            { 2, "angry" },
                        },
                    },
                    Area =
                    {
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(370, 290, 340, 510),
                            Score = 0,
                            Description = "頭",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 3, null },
                                    { 10, "embarrassed" },
                                    { 1, "normal" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(370, 290, 340, 510),
                            Score = 0,
                            Description = "顔",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 3, null },
                                    { 3, "embarrassed" },
                                    { 1, "normal" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(280, 420, 520, 340),
                            Score = 3,
                            Description = "壁ドン(良)",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "taiko03.mp3",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 20, null },
                                    { 1, "embarrassed" },
                                    { 1, "normal" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(350, 970, 450, 270),
                            Score = 0,
                            Description = "胸",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 5, null },
                                    { 4, "normal" },
                                    { 2, "angry" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(260, 760, 620, 1160),
                            Score = 0,
                            Description = "胴",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 3, null },
                                    { 1, "embarrassed" },
                                    { 4, "normal" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(130, 210, 820, 920),
                            Score = 0,
                            Description = "壁ドン(広)",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "drum02.mp3",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 20, null },
                                    { 1, "embarrassed" },
                                    { 2, "normal" },
                                },
                            },
                        },
                    },
                },

                new State
                {
                    Id = "embarrassed",
                    Description = @"照れ状態。チョロインなので何やっても得点。
壁ドン領域: 4点。経過時間リセット率高め(継続して照れ状態維持できる)。
顔、頭: 7点。得点は高いんだけど、経過時間リセットされない。
離れた場所: 1点。もはやこれでは何も起きない。
ボディタッチ: 3点。驚き遷移率高め。
",
                    Image = "embarrassed.png",
                    Sound = "drum02.mp3",
                    TimeFrame = TimeFrame.Exponential(5),
                    Transition = new P.Table<string>
                    {
                        Items =
                        {
                            { 2, "normal" },
                            { 1, "surprise" },
                        },
                    },
                    Area =
                    {
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(370, 290, 340, 510),
                            Score = 7,
                            Description = "頭",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 1, null },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(370, 290, 340, 510),
                            Score = 7,
                            Description = "顔",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 1, null },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(280, 420, 520, 340),
                            Score = 4,
                            Description = "壁ドン(良)",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "taiko03.mp3",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 1, null },
                                    { 1, "embarrassed" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(350, 970, 450, 270),
                            Score = 3,
                            Description = "胸",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 2, null },
                                    { 4, "surprise" },
                                    { 1, "normal" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(260, 760, 620, 1160),
                            Score = 3,
                            Description = "胴",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 2, null },
                                    { 4, "surprise" },
                                    { 1, "normal" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(130, 210, 820, 920),
                            Score = 1,
                            Description = "壁ドン(広)",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "drum02.mp3",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 1, null },
                                },
                            },
                        },
                    },
                },

                new State
                {
                    Id = "angry",
                    Description = @"怒り状態。とにかく減点だらけ。
壁ドン領域: 1点。
離れた場所: 0点。
顔、頭: -1点。たまに通常状態に復帰可能。怒り継続率高め。
胴: -1点。
胸: -1点。レアボイスの「往生際が悪いわよ」。100% 怒り継続。
",
                    Image = "angry.png",
                    Sound = "drum02.mp3",
                    TimeFrame = TimeFrame.Exponential(30),
                    Transition = new P.Table<string>
                    {
                        Items =
                        {
                            { 1, "normal" },
                        },
                    },
                    Area =
                    {
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(370, 290, 340, 510),
                            Score = -1,
                            Description = "頭",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 3, null },
                                    { 1, "normal" },
                                    { 5, "angry" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(370, 290, 340, 510),
                            Score = -1,
                            Description = "顔",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 3, null },
                                    { 1, "normal" },
                                    { 5, "angry" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(280, 420, 520, 340),
                            Score = 1,
                            Description = "壁ドン(良)",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "taiko03.mp3",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 1, null },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(350, 970, 450, 270),
                            Score = -1,
                            Description = "胸",
                            CoolTime = TimeFrame.Constant(2),
                            Sound = "04oujougiwa.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 1, "angry" },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(260, 760, 620, 1160),
                            Score = -1,
                            Description = "胴",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "37ah.wav",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 1, null },
                                },
                            },
                        },
                        new AreaInfo
                        {
                            Rect = Rect.LeftTop(130, 210, 820, 920),
                            Score = 0,
                            Description = "壁ドン(広)",
                            CoolTime = TimeFrame.Constant(1),
                            Sound = "drum02.mp3",
                            Transition = new P.Table<string>
                            {
                                Items =
                                {
                                    { 1, null },
                                },
                            },
                        },
                    },
                },
            },
        };

    }
}
