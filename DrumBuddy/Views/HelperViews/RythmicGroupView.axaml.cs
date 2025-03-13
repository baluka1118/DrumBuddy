using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using DrumBuddy.ViewModels.HelperViewModels;
using ReactiveUI;

namespace DrumBuddy.Views.HelperViews;

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
        });
    }
}