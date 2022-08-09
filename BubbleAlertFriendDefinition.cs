using Plus.Plugins;

namespace BubbleAlertFriend
{
    public class BubbleAlertFriendDefinition : IPluginDefinition
    {
        public string Name => "BubbleAlertFriend";
        public string Author => "Harb#9937";
        public Version Version => new(1, 0, 0);
        public Type PluginClass => typeof(BubbleAlertFriend);
    }
}