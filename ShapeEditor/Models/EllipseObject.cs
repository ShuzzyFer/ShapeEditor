namespace ShapeEditor.Models
{
    public class EllipseObject : GeometricObject
    {
        private double _centerX;
        public double CenterX
        {
            get => _centerX;
            set { _centerX = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanvasPositionX)); OnPropertyChanged(nameof(Details)); }
        }

        private double _centerY;
        public double CenterY
        {
            get => _centerY;
            set { _centerY = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanvasPositionY)); OnPropertyChanged(nameof(Details)); }
        }

        private double _radiusX; // Горизонтальный радиус
        public double RadiusX
        {
            get => _radiusX;
            set { if (value > 0) { _radiusX = value; OnPropertyChanged(); OnPropertyChanged(nameof(Details)); } }
        }

        private double _radiusY; // Вертикальный радиус
        public double RadiusY
        {
            get => _radiusY;
            set { if (value > 0) { _radiusY = value; OnPropertyChanged(); OnPropertyChanged(nameof(Details)); } }
        }
        
        public override double CanvasPositionX => CenterX - RadiusX;
        public override double CanvasPositionY => CenterY - RadiusY;

        public EllipseObject(double centerX, double centerY, double radiusX, double radiusY, string name = "Ellipse")
        {
            CenterX = centerX; CenterY = centerY; RadiusX = radiusX; RadiusY = radiusY; Name = name;
        }

        public override string GetDetails()
        {
            return $"{Name}: Center({CenterX:F2}, {CenterY:F2}), Radii({RadiusX:F2}, {RadiusY:F2})";
        }
        
        public string Details => GetDetails();
    }
}