using Melanchall.DryWetMidi.Multimedia;

namespace DrumBuddy.IO.Services;

public class MidiOutputService : IDisposable
{
    private OutputDevice? _device;

    public bool IsConnected => _device != null;

    public MidiDeviceConnectionResult TryConnect(string desiredDeviceName, bool forceDeviceChoosing = false)
    {
        var devices = GetOutputDevices();
        if (devices.Length == 0)
            return new MidiDeviceConnectionResult([]);

        if (devices.Length > 1)
        {
            if (!forceDeviceChoosing &&
                !string.IsNullOrWhiteSpace(desiredDeviceName) &&
                devices.Any(d => d.Name == desiredDeviceName))
            {
                OpenDevice(desiredDeviceName);
                return new MidiDeviceConnectionResult([new MidiDeviceShortInfo(0, desiredDeviceName)]);
            }

            return new MidiDeviceConnectionResult(devices);
        }

        OpenDevice(devices[0].Name);
        return new MidiDeviceConnectionResult([devices[0]]);
    }

    public void SetUserChosenDevice(MidiDeviceShortInfo? chosenDeviceInfo)
    {
        if (chosenDeviceInfo is null)
            return;

        OpenDevice(chosenDeviceInfo.Name);
    }

    public OutputDevice? GetDevice() => _device;

    public static MidiDeviceShortInfo[] GetOutputDevices()
    {
        return OutputDevice.GetAll()
            .Select((device, index) => new MidiDeviceShortInfo(index, device.Name))
            .ToArray();
    }

    private void OpenDevice(string deviceName)
    {
        CloseDevice();

        var device = OutputDevice.GetByName(deviceName);
        device.IsEnabled = true;
        _device = device;
    }

    private void CloseDevice()
    {
        if (_device == null)
            return;

        if (_device.IsEnabled)
            _device.IsEnabled = false;

        _device.Dispose();
        _device = null;
    }

    public void Dispose()
    {
        CloseDevice();
    }
}
