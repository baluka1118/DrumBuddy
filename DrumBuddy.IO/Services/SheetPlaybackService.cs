using DrumBuddy.Core.Models;
using Melanchall.DryWetMidi.Multimedia;

namespace DrumBuddy.IO.Services;

public class SheetPlaybackService : IDisposable
{
    private readonly MidiOutputService _midiOutputService;
    private Playback? _playback;

    public SheetPlaybackService(MidiOutputService midiOutputService)
    {
        _midiOutputService = midiOutputService;
    }

    public bool IsPlaying => _playback?.IsRunning ?? false;

    public event EventHandler? PlaybackFinished;

    public bool TryPlay(Sheet sheet)
    {
        Stop();

        var device = _midiOutputService.GetDevice();
        if (device is null)
            return false;

        var midiFile = MidiExporter.BuildMidiFile(sheet);
        _playback = midiFile.GetPlayback(device);
        _playback.Finished += OnPlaybackFinished;
        _playback.Start();
        return true;
    }

    public void Stop()
    {
        if (_playback is null)
            return;

        _playback.Finished -= OnPlaybackFinished;
        if (_playback.IsRunning)
            _playback.Stop();

        _playback.Dispose();
        _playback = null;
    }

    private void OnPlaybackFinished(object? sender, EventArgs e)
    {
        Stop();
        PlaybackFinished?.Invoke(this, EventArgs.Empty);
    }

    public void Dispose()
    {
        Stop();
    }
}
