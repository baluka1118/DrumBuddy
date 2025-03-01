using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using DrumBuddy.Core.Models;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace DrumBuddy.ViewModels.HelperViewModels
{
    public partial class RythmicGroupViewModel : ReactiveObject
    {
        private readonly RythmicGroup _rythmicGroup;

        [Reactive] private List<NoteGroupViewModel> _noteGroups;

        [Reactive] public string _color;
        
        public RythmicGroupViewModel(RythmicGroup rythmicGroup = null)
        {
            _rythmicGroup = rythmicGroup ?? new RythmicGroup(System.Collections.Immutable.ImmutableArray<NoteGroup>.Empty);
            Color = "Black";
            InitializeNoteGroups();
        }
        
        private void InitializeNoteGroups()
        {
            NoteGroups = _rythmicGroup.NoteGroups
                .Select(ng => new NoteGroupViewModel(ng))
                .ToList();
            
            // Set horizontal spacing
            for (int i = 0; i < NoteGroups.Count; i++)
            {
                NoteGroups[i].XPosition = i * 30;  // 30 pixels between note groups
            }
            
        }
        
        
        // Method to update rhythmic group with new note groups
        public void UpdateNoteGroups(IEnumerable<NoteGroup> noteGroups)
        {
            NoteGroups = noteGroups.Select(ng => new NoteGroupViewModel(ng)).ToList();
            
            // Reset horizontal spacing
            for (int i = 0; i < NoteGroups.Count; i++)
            {
                NoteGroups[i].XPosition = i * 30;
            }
            
        }
    }
}
