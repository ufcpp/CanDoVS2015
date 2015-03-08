using KabeDon.Common;
using KabeDon.DataModels;

namespace KabeDon.Engine.Messages
{
    public class AwaitTap : Message, IResponsiveMessage<Point>
    {
        /// <summary>
        /// タップされた点。
        /// </summary>
        public Point Response { get; set; }
    }
}
