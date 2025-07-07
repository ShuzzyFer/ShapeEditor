using System.Windows;
using System.Windows.Media;

namespace ShapeEditor.Models
{
    public class CustomPathObject : GeometricObject
    {
        public PathGeometry DefiningGeometry { get; private set; }

        private double _x;
        public double X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanvasPositionX)); OnPropertyChanged(nameof(Details)); }
        }

        private double _y;
        public double Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanvasPositionY)); OnPropertyChanged(nameof(Details)); }
        }

        public override double CanvasPositionX => X;
        public override double CanvasPositionY => Y;

        public CustomPathObject(PathGeometry rawGeometry, string name = "Custom Shape")
        {
            Name = name;
            DefiningGeometry = rawGeometry;
            DefiningGeometry.Freeze(); // Оптимизация

            // Устанавливаем начальные X и Y из границ геометрии
            Rect bounds = rawGeometry.Bounds;
            _x = bounds.Left;
            _y = bounds.Top;
        }

        public override string GetDetails()
        {
            return $"{Name ?? "Unnamed Shape"}: Custom path at ({X:F2}, {Y:F2})";
        }
        
        public string Details => GetDetails();
    }
}