using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabeDon.DataModels
{
    /// <summary>
    /// 特定の範囲をタップしたときの挙動に関する情報。
    /// </summary>
    public class AreaInfo
    {
        /// <summary>
        /// タップ後、この時間だけタップを受け付けない。
        /// </summary>
        public TimeFrame CoolTime { get; set; }
    }
}
