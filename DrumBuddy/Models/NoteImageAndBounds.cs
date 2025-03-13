﻿using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;

namespace DrumBuddy.Models;

public class NoteImageAndBounds(Uri imagePath, Rect bounds) : ReactiveObject
{
    public Uri ImagePath { get; } = imagePath;
    public Rect Bounds { get; } = bounds;
    public Bitmap Image { get; } = new Bitmap(AssetLoader.Open(imagePath)); //might get cached and injected from constructor
}