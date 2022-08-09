using Plus.Core;
using Plus.HabboHotel.Users;
using Plus.HabboHotel.GameClients;
using Plus.HabboHotel.Users.Authentication;
using Plus.Communication.Packets.Outgoing.Rooms.Notifications;

namespace BubbleAlertFriend;

public class OnUserLoginEvent : IStartable
{
    private readonly IAuthenticator _authenticate;
    private readonly IGameClientManager _gameClientManager;
    private Dictionary<string, string> _values;

    public OnUserLoginEvent(IAuthenticator authenticate, IGameClientManager gameClientManager)
    {
        _authenticate = authenticate;
        _gameClientManager = gameClientManager;
    }

    public Task Start()
    {
        _values = new();
        _authenticate.HabboLoggedIn += SubscribeToPlayer;
        return Task.CompletedTask;
    }

    private void SubscribeToPlayer(object? sender, HabboEventArgs e)
    {
        if (e.Habbo is Habbo user)
        {
            foreach (var friend in user.GetMessenger().Friends.Values)
            {
                if (friend.AppearOffline) continue;
                if (user.Id == friend.Id) continue;

                _values.Add("display", "BUBBLE");
                _values.Add("image", $"https://imager.habboon.pw/?figure={user.Look}&headonly=1");
                _values.Add("message", $"Your friend {user.Username} logged on.");

                _gameClientManager.GetClientByUserId(friend.Id).Send(new RoomNotificationComposer("furni_placement_error", _values));

                _values.Clear();
            }
        }
    }
}