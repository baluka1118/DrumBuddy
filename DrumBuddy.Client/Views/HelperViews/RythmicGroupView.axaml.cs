using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using DrumBuddy.Client.ViewModels.HelperViewModels;
using ReactiveUI;

namespace DrumBuddy.Client.Views.HelperViews;

public partial class RythmicGroupView : ReactiveUserControl<RythmicGroupViewModel>
{
    public RythmicGroupView()
    {
        InitializeComponent();
        this.WhenActivated(d =>
        {
            this.OneWayBind(ViewModel, vm => vm.NotesImageAndBoundsList, v => v.NoteImagesList.ItemsSource)
                .DisposeWith(d);
            this.OneWayBind(ViewModel, vm => vm.Lines, v => v.LinesList.ItemsSource).DisposeWith(d);
            this.Bind(ViewModel, vm => vm.Width, v => v.Width);
        });
    }
}