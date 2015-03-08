namespace KabeDon.Engine.Messages
{
    public class PlaySound : Message
    {
        public string Name { get; }
        public PlaySound(string name) { Name = name; }
    }
}
