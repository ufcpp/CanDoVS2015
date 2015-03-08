using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabeDon.DataModels
{
    public class State
    {
        /// <summary>
        /// ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 【編集用】
        /// 名前。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// この状態でいる時間。
        /// null なら時間での状態遷移なし。
        /// </summary>
        public TimeFrame TimeFrame { get; set; }
    }
}
