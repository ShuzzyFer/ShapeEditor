namespace ShapeEditor.Models
{
    public class CircleObject : GeometricObject
    {
        private double _centerX;
        
        public override double CanvasPositionX => CenterX;
        public override double CanvasPositionY => CenterY;

        public double CenterX
        {
            get => _centerX;
            set
            {
                _centerX = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Details));
                OnPropertyChanged(nameof(CanvasPositionX)); 
            }
        }

        public double CenterY
        {
            get => _centerY;
            set
            {
                _centerY = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Details));
                OnPropertyChanged(nameof(CanvasPositionY)); 
            }
        }

        private double _centerY;

        private double _radius;
        public double Radius
        {
            get => _radius;
            set
            {
                if (value > 0) 
                {
                    _radius = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Details));
                }
            }
        }

        public string Details => GetDetails();

        public CircleObject(double centerX, double centerY, double radius, string name = "Circle")
        {
            CenterX = centerX;
            CenterY = centerY;
            Radius = radius; 
            Name = name;
        }
        
        public CircleObject() : this(0,0,1) {}
        
        public override string GetDetails()
        {
            return $"{Name ?? "Unnamed Circle"}: Center({CenterX:F2}, {CenterY:F2}), Radius({Radius:F2})";
        }
    }
}