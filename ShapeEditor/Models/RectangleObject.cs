namespace ShapeEditor.Models
{
    public class RectangleObject : GeometricObject
    {
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

        private double _width;
        public double Width
        {
            get => _width;
            set { if (value > 0) { _width = value; OnPropertyChanged(); OnPropertyChanged(nameof(Details)); } }
        }

        private double _height;
        public double Height
        {
            get => _height;
            set { if (value > 0) { _height = value; OnPropertyChanged(); OnPropertyChanged(nameof(Details)); } }
        }

        public override double CanvasPositionX => X;
        public override double CanvasPositionY => Y;

        public RectangleObject(double x, double y, double width, double height, string name = "Rectangle")
        {
            X = x; Y = y; Width = width; Height = height; Name = name;
        }

        public override string GetDetails()
        {
            return $"{Name}: TopLeft({X:F2}, {Y:F2}), Size({Width:F2}x{Height:F2})";
        }
        
        public string Details => GetDetails();
    }
}