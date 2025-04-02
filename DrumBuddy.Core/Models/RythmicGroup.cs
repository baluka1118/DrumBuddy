﻿using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace DrumBuddy.Core.Models;

/// <summary>
/// Represents a group of notegroups that are played in a time window of one quarter of a measure.
/// </summary>
/// <param name="NoteGroups">Note groups inside the rythmic group</param>
public record RythmicGroup(ImmutableArray<NoteGroup> NoteGroups)
{
    [JsonIgnore]
    public bool IsEmpty => NoteGroups.All(n => n.IsRest);
}