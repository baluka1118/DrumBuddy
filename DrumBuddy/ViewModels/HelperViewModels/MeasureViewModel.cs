﻿using System.Collections.Generic;
using System.Collections.Immutable;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using DrumBuddy.Core.Models;
using DynamicData;
using LanguageExt;
using ReactiveUI.SourceGenerators;

namespace DrumBuddy.ViewModels.HelperViewModels
{
    public partial class MeasureViewModel : ReactiveObject
    {

        private double _pointerPosition;
        public double PointerPosition
        {
            get => _pointerPosition;
            set => this.RaiseAndSetIfChanged(ref _pointerPosition, value);
        }
        public Measure Measure = new(new(4));
        public MeasureViewModel()
        {
            IsPointerVisible = true;
        }
        [Reactive]
        private bool _isPointerVisible;
        public void AddRythmicGroupFromNotes(List<Note> notes)
        {
            var rg = new RythmicGroup(notes.ToImmutableArray()); //will be a call to the recordingservice
            Measure.Groups.Add(rg);
            RythmicGroups.Add(new(rg));
        }
        public ObservableCollection<RythmicGroupViewModel> RythmicGroups { get; } = new();
        public void MovePointerToRG(long rythmicGroupIndex)
        {
            PointerPosition = (rythmicGroupIndex * 135) + 35;
        }
        public bool IsEmpty => RythmicGroups.All(rg => rg.RythmicGroup.IsDefault());
    }
}
