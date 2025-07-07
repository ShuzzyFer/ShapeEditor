using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace ShapeEditor.Models
{
    public abstract class GeometricObject : INotifyPropertyChanged
    {
        private Guid _id;
        private string _name = string.Empty;
        
        public abstract double CanvasPositionX { get; }
        public abstract double CanvasPositionY { get; }
        
        private Color _strokeColor = Colors.Black; // Цвет обводки по умолчанию
        public Color StrokeColor
        {
            get => _strokeColor;
            set
            {
                if (_strokeColor != value)
                {
                    _strokeColor = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _strokeThickness = 1.0; // Толщина обводки по умолчанию
        public double StrokeThickness
        {
            get => _strokeThickness;
            set
            {
                if (_strokeThickness != value)
                {
                    _strokeThickness = value;
                    OnPropertyChanged();
                }
            }
        }

        private Color _fillColor = Colors.Transparent; // Цвет заливки по умолчанию (прозрачный)
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                if (_fillColor != value)
                {
                    _fillColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Guid Id
        {
            get => _id;

            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(); 
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        protected GeometricObject()
        {
            _id = Guid.NewGuid();
            Id = Guid.NewGuid();
        }

        public abstract string GetDetails(); 

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}