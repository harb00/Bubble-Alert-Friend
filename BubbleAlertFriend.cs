using Plus.Plugins;
using Microsoft.Extensions.Logging;

namespace BubbleAlertFriend;

public class BubbleAlertFriend : IPlugin
{

    private readonly ILogger _logger;

    private readonly IPluginDefinition _pluginInfo = new BubbleAlertFriendDefinition();

    public BubbleAlertFriend(ILogger<BubbleAlertFriend> logger)
    {
        _logger = logger;
    }
    
    public void Start()
    {
        Logger(_pluginInfo.Name + " by " + _pluginInfo.Author + " has started.");
    }

    private void Logger(string message)
    {
        var CYAN = "\u001b[34m";
        var WHITE = "\u001b[37m";
        _logger.LogInformation(WHITE + "[" + CYAN + _pluginInfo.Name + WHITE + "] " + message);
    }

}
