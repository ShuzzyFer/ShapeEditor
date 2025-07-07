namespace ShapeEditor.Models
{
    public class PointObject : GeometricObject
    {
        private double _x;
        
        public override double CanvasPositionX => X;
        public override double CanvasPositionY => Y;
        public double X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Details));
                OnPropertyChanged(nameof(CanvasPositionX)); 
            }
        }

        private double _y;
        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Details));
                OnPropertyChanged(nameof(CanvasPositionY)); 
            }
        }
        
        public string Details => GetDetails();

        public PointObject(double x, double y, string name = "Point")
        {

            X = x;
            Y = y;
            Name = name; 
        }

        public override string GetDetails()
        {
            return $"{Name ?? "Unnamed Point"}: ({X:F2}, {Y:F2})";
        }
    }
}