using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using DrumBuddy.Core.Enums;
using DrumBuddy.IO.Enums;
using DrumBuddy.ViewModels.HelperViewModels;
using ReactiveUI;

namespace DrumBuddy.Views.HelperViews
{
    public partial class NoteGroupView : ReactiveUserControl<NoteGroupViewModel>
    {
        private Canvas _noteCanvas => this.FindControl<Canvas>("NoteCanvas");
        public NoteGroupView()
        {
            InitializeComponent();
            
            this.WhenActivated(disposables =>
            {
                if (ViewModel != null)
                {
                    DrawNotes();
                }
            });
        }
        
        private void DrawNotes()
        {
            _noteCanvas.Children.Clear();
            
            if (ViewModel.IsRest)
            {
                DrawRest();
                return;
            }
            
            // Sort notes by Y position to properly connect them
            var notes = ViewModel.Notes.OrderBy(n => ViewModel.GetYPosition(n.Beat)).ToList();
            
            // Draw note heads
            foreach (var note in notes)
            {
                DrawNoteHead(note.Beat);
            }
            
            // Draw stem and connect notes if multiple exist
            if (notes.Count > 0)
            {
                double stemX = 20; // X position for the stem
                double topY = ViewModel.GetYPosition(notes.First().Beat);
                double bottomY = ViewModel.GetYPosition(notes.Last().Beat);
                
                // Add some padding based on stem direction
                string stemDirection = ViewModel.GetStemDirection(notes.First().Beat);
                
                // Draw the vertical connector line between notes
                if (notes.Count > 1)
                {
                    var connector = new Line
                    {
                        StartPoint = new Point(stemX, topY),
                        EndPoint = new Point(stemX, bottomY),
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };
                    _noteCanvas.Children.Add(connector);
                }
                
                // Draw the stem
                double stemHeight = 30;
                double stemStartY = stemDirection == "Up" ? topY - stemHeight : bottomY;
                double stemEndY = stemDirection == "Up" ? topY : bottomY + stemHeight;
                
                var stem = new Line
                {
                    StartPoint = new Point(stemX, stemStartY),
                    EndPoint = new Point(stemX, stemEndY),
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                _noteCanvas.Children.Add(stem);
                
                // Draw flags based on note value
                DrawNoteFlag(stemX, stemDirection == "Up" ? stemStartY : stemEndY, ViewModel.NoteValue, stemDirection);
            }
        }
        
        private void DrawNoteHead(Beat beat)
        {
            double y = ViewModel.GetYPosition(beat);
            string headType = ViewModel.GetNoteHeadType(beat);
            
            if (headType == "X")
            {
                // Draw X for cymbals
                var line1 = new Line
                {
                    StartPoint = new Point(10, y - 5),
                    EndPoint = new Point(20, y + 5),
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                
                var line2 = new Line
                {
                    StartPoint = new Point(10, y + 5),
                    EndPoint = new Point(20, y - 5),
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                
                _noteCanvas.Children.Add(line1);
                _noteCanvas.Children.Add(line2);
            }
            else
            {
                // Draw regular note head
                var ellipse = new Ellipse
                {
                    Width = 10,
                    Height = 8,
                    Fill = Brushes.Black,
                };
                
                Canvas.SetLeft(ellipse, 10);
                Canvas.SetTop(ellipse, y - 4);
                _noteCanvas.Children.Add(ellipse);
            }
        }
        
        private void DrawNoteFlag(double stemX, double stemEndY, NoteValue noteValue, string stemDirection)
        {
            switch (noteValue)
            {
                case NoteValue.Eighth:
                    DrawEighthFlag(stemX, stemEndY, stemDirection);
                    break;
                case NoteValue.Sixteenth:
                    DrawSixteenthFlag(stemX, stemEndY, stemDirection);
                    break;
                case NoteValue.Quarter:
                    // Quarter notes don't have flags
                    break;
            }
        }
        
        private void DrawEighthFlag(double stemX, double stemEndY, string stemDirection)
        {
            var flag = new PathFigure();
            var segments = new PathSegments();
            
            if (stemDirection == "Up")
            {
                var lineSegment = new LineSegment();
                lineSegment.Point = new Point(stemX + 10, stemEndY + 10);
                segments.Add(lineSegment);
                flag.StartPoint = new Point(stemX, stemEndY);
            }
            else
            {
                var lineSegment = new LineSegment();
                lineSegment.Point = new Point(stemX + 10, stemEndY - 10);
                segments.Add(lineSegment);
                flag.StartPoint = new Point(stemX, stemEndY);
            }
            
            flag.Segments = segments;
            
            var geometry = new PathGeometry();
            geometry.Figures.Add(flag);
            
            var path = new Path
            {
                Data = geometry,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            
            _noteCanvas.Children.Add(path);
        }
        
        private void DrawSixteenthFlag(double stemX, double stemEndY, string stemDirection)
        {
            // Draw two flags for sixteenth notes
            var flag1 = new PathFigure();
            var segments1 = new PathSegments();
            
            var flag2 = new PathFigure();
            var segments2 = new PathSegments();
            
            if (stemDirection == "Up")
            {
                var lineSegment1 = new LineSegment();
                lineSegment1.Point = new Point(stemX + 10, stemEndY + 8);
                segments1.Add(lineSegment1);
                flag1.StartPoint = new Point(stemX, stemEndY);
                
                var lineSegment2 = new LineSegment();
                lineSegment2.Point = new Point(stemX + 10, stemEndY + 16);
                segments2.Add(lineSegment2);
                flag2.StartPoint = new Point(stemX, stemEndY + 8);
            }
            else
            {
                var lineSegment1 = new LineSegment();
                lineSegment1.Point = new Point(stemX + 10, stemEndY - 8);
                segments1.Add(lineSegment1);
                flag1.StartPoint = new Point(stemX, stemEndY);
                
                var lineSegment2 = new LineSegment();
                lineSegment2.Point = new Point(stemX + 10, stemEndY - 16);
                segments2.Add(lineSegment2);
                flag2.StartPoint = new Point(stemX, stemEndY - 8);
            }
            
            flag1.Segments = segments1;
            flag2.Segments = segments2;
            
            var geometry1 = new PathGeometry();
            geometry1.Figures.Add(flag1);
            
            var geometry2 = new PathGeometry();
            geometry2.Figures.Add(flag2);
            
            var path1 = new Path
            {
                Data = geometry1,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            
            var path2 = new Path
            {
                Data = geometry2,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            
            _noteCanvas.Children.Add(path1);
            _noteCanvas.Children.Add(path2);
        }
        
        private void DrawRest()
        {
            switch (ViewModel.NoteValue)
            {
                case NoteValue.Quarter:
                    DrawQuarterRest();
                    break;
                case NoteValue.Eighth:
                    DrawEighthRest();
                    break;
                case NoteValue.Sixteenth:
                    DrawSixteenthRest();
                    break;
            }
        }
        
        private void DrawQuarterRest()
        {
            // Quarter rest is a complex shape
            var path = new Path
            {
                Data = Geometry.Parse("M15,30 C18,25 10,40 15,45 C10,50 18,40 15,35 L15,30"),
                Fill = Brushes.Black
            };
            _noteCanvas.Children.Add(path);
        }
        
        private void DrawEighthRest()
        {
            // Simplified eighth rest
            var path = new Path
            {
                Data = Geometry.Parse("M15,35 C20,30 10,45 15,55"),
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            _noteCanvas.Children.Add(path);
        }
        
        private void DrawSixteenthRest()
        {
            // Simplified sixteenth rest (similar to eighth but with two flags)
            var path = new Path
            {
                Data = Geometry.Parse("M15,35 C20,30 10,45 15,55 M18,40 C23,35 13,50 18,60"),
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            _noteCanvas.Children.Add(path);
        }
    }
}