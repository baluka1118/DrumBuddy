using System.Collections.Generic;
using DrumBuddy.Core.Enums;
using DrumBuddy.Core.Models;
using DrumBuddy.IO.Enums;
using ReactiveUI;

namespace DrumBuddy.ViewModels.HelperViewModels;

public class NoteGroupViewModel : ReactiveObject
    {
        private readonly NoteGroup _noteGroup;
        
        public NoteGroupViewModel(NoteGroup noteGroup)
        {
            _noteGroup = noteGroup;
        }

        public NoteValue NoteValue => _noteGroup.Count > 0 ? _noteGroup[0].Value : NoteValue.Sixteenth;
        
        public bool IsRest => _noteGroup.IsRest;
        
        public IEnumerable<Note> Notes => _noteGroup;
        
        // Position information for rendering
        public double XPosition { get; set; }
        
        // Note head type (normal for drums, X for cymbals)
        public string GetNoteHeadType(Beat beat)
        {
            return beat switch
            {
                Beat.HiHat or Beat.Crash1 or Beat.Crash2 or Beat.Ride => "X",
                _ => "Normal"
            };
        }
        
        // Y position mapping for different drum elements
        public double GetYPosition(Beat beat)
        {
            return beat switch
            {
                Beat.Bass => 80, // Between 4th and 5th line
                Beat.Snare => 40, // Between 2nd and 3rd line
                Beat.HiHat => 10, // Above 1st line
                Beat.Tom1 => 30,
                Beat.Tom2 => 50,
                Beat.FloorTom => 70,
                Beat.Crash1 or Beat.Crash2 => 0, // 0th line
                Beat.Ride => 20, // 1st line
                Beat.Rest => 40, // Same as snare for now
                _ => 40
            };
        }
        
        public string GetStemDirection(Beat beat)
        {
            return beat switch
            {
                Beat.HiHat or Beat.Crash1 or Beat.Crash2 or Beat.Ride => "Up",
                _ => "Down"
            };
        }
        
        // Helper method for note flag type based on note value
        public string GetNoteFlag()
        {
            return NoteValue switch
            {
                NoteValue.Eighth => "Single",
                NoteValue.Sixteenth => "Double",
                _ => "None"
            };
        }
    }