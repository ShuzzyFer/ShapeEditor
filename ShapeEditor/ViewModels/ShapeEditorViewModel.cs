using ShapeEditor.Commands;
using System.Windows.Input;
using System.Windows.Media;

namespace ShapeEditor.ViewModels
{
    public class CustomShapeEditorViewModel : ViewModelBase
    {
        private PathGeometry _pathGeometry = new PathGeometry();
        public PathGeometry PathGeometry
        {
            get => _pathGeometry;
            set => SetProperty(ref _pathGeometry, value);
        }
        
        private PathFigure? _currentFigure;

        public ICommand StartDrawingCommand { get; }
        public ICommand DrawingCommand { get; }
        public ICommand EndDrawingCommand { get; }

        public CustomShapeEditorViewModel()
        {
            StartDrawingCommand = new RelayCommand(ExecuteStartDrawing);
            DrawingCommand = new RelayCommand(ExecuteDrawing);
            EndDrawingCommand = new RelayCommand(ExecuteEndDrawing);
        }

        private void ExecuteStartDrawing(object? parameter)
        {
            if (parameter is System.Windows.Point startPoint)
            {
                _currentFigure = new PathFigure { StartPoint = startPoint };
                var newGeometry = PathGeometry.Clone(); // Работаем с копией, чтобы UI обновлялся
                newGeometry.Figures.Add(_currentFigure);
                PathGeometry = newGeometry;
            }
        }

        private void ExecuteDrawing(object? parameter)
        {
            if (parameter is System.Windows.Point point && _currentFigure != null)
            {
                _currentFigure.Segments.Add(new LineSegment(point, isStroked: true));
            }
        }

        private void ExecuteEndDrawing(object? parameter)
        {
            _currentFigure = null;
        }
    }
}