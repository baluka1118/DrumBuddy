using System;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using DrumBuddy.IO.Services;
using Notification = Avalonia.Controls.Notifications.Notification;

namespace DrumBuddy.Services;

public static class MidiOutputConnectionHelper
{
    public const string LastMidiOutputDeviceKey = "LastUsedMidiOutputDevice";

    public static async Task<bool> EnsureConnectedAsync(
        MidiOutputService midiOutputService,
        ConfigurationService configurationService,
        NotificationService notificationService,
        Func<MidiDeviceShortInfo[], Task<MidiDeviceShortInfo?>> chooseDeviceAsync)
    {
        if (midiOutputService.IsConnected)
            return true;

        var desiredName = configurationService.Get<string>(LastMidiOutputDeviceKey) ?? string.Empty;
        var connectionResult = midiOutputService.TryConnect(desiredName);
        switch (connectionResult.DevicesConnected.Length)
        {
            case 0:
                notificationService.ShowNotification(new Notification(
                    "No MIDI output devices found",
                    "Enable IAC Driver in Audio MIDI Setup, or connect a MIDI interface.",
                    NotificationType.Error));
                return false;
            case > 1:
            {
                var chosenDevice = await chooseDeviceAsync(connectionResult.DevicesConnected);
                if (chosenDevice is null)
                {
                    notificationService.ShowNotification(new Notification(
                        "No MIDI output device chosen",
                        "Select an output device to play the sheet.",
                        NotificationType.Warning));
                    return false;
                }

                midiOutputService.SetUserChosenDevice(chosenDevice);
                await configurationService.SetAsync(LastMidiOutputDeviceKey, chosenDevice.Name);
                return true;
            }
            default:
                await configurationService.SetAsync(LastMidiOutputDeviceKey,
                    connectionResult.DevicesConnected[0].Name);
                return true;
        }
    }
}
