using ShapeEditor.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls; // Для ObservableCollection
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ShapeEditor.Commands;
using Svg; // Для ICommand


namespace ShapeEditor.ViewModels
{
    public enum ShapeType
    {
        Point,
        Circle,
        Rectangle,
        Ellipse,
        Custom
    }

    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<GeometricObject> _shapes;

        public ObservableCollection<GeometricObject> Shapes
        {
            get => _shapes;
            set => SetProperty(ref _shapes, value);
        }

        private GeometricObject? _selectedShape; 

        public GeometricObject? SelectedShape
        {
            get => _selectedShape;
            set
            {
                if (SetProperty(ref _selectedShape, value))
                {
                    (RemoveShapeCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (BringToFrontCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (SendToBackCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (BringForwardCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (SendBackwardCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private ShapeType _selectedShapeTypeToAdd = ShapeType.Point;

        private bool CanExecuteZOrderChange(object? parameter)
        {
            return SelectedShape != null; // Команды доступны только если выбрана фигура
        }

        private void ExecuteBringToFront(object? parameter)
        {
            if (SelectedShape == null) return;
            var index = Shapes.IndexOf(SelectedShape);
            if (index < Shapes.Count - 1)
            {
                Shapes.Move(index, Shapes.Count - 1);
            }
        }

        private void ExecuteSendToBack(object? parameter)
        {
            if (SelectedShape == null) return;
            var index = Shapes.IndexOf(SelectedShape);
            if (index > 0)
            {
                Shapes.Move(index, 0);
            }
        }

        private void ExecuteBringForward(object? parameter)
        {
            if (SelectedShape == null) return;
            var index = Shapes.IndexOf(SelectedShape);
            if (index < Shapes.Count - 1)
            {
                Shapes.Move(index, index + 1);
            }
        }

        private void ExecuteSendBackward(object? parameter)
        {
            if (SelectedShape == null) return;
            var index = Shapes.IndexOf(SelectedShape);
            if (index > 0)
            {
                Shapes.Move(index, index - 1);
            }
        }

        public ShapeType SelectedShapeTypeToAdd
        {
            get => _selectedShapeTypeToAdd;
            set
            {
                if (SetProperty(ref _selectedShapeTypeToAdd, value))
                {
                    ClearNewShapeInputs();
                    (AddShapeCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }
        
        private double _newPointX;

        public double NewPointX
        {
            get => _newPointX;
            set => SetProperty(ref _newPointX, value);
        }

        private double _newPointY;

        public double NewPointY
        {
            get => _newPointY;
            set => SetProperty(ref _newPointY, value);
        }

        private string _newPointName = "New Point";

        public string NewPointName
        {
            get => _newPointName;
            set => SetProperty(ref _newPointName, value);
        }
        
        private double _newCircleCenterX;
        public double NewCircleCenterX
        {
            get => _newCircleCenterX;
            set => SetProperty(ref _newCircleCenterX, value);
        }

        private double _newCircleCenterY;

        public double NewCircleCenterY
        {
            get => _newCircleCenterY;
            set => SetProperty(ref _newCircleCenterY, value);
        }

        private double _newCircleRadius = 10; // Значение по умолчанию

        public double NewCircleRadius
        {
            get => _newCircleRadius;
            set => SetProperty(ref _newCircleRadius, value);
        }

        private string _newCircleName = "New Circle";

        public string NewCircleName
        {
            get => _newCircleName;
            set => SetProperty(ref _newCircleName, value);
        }

        private Color _newShapeStrokeColor = Colors.Black;

        public Color NewShapeStrokeColor
        {
            get => _newShapeStrokeColor;
            set => SetProperty(ref _newShapeStrokeColor, value);
        }

        private Color _newShapeFillColor = Colors.Transparent;

        public Color NewShapeFillColor
        {
            get => _newShapeFillColor;
            set => SetProperty(ref _newShapeFillColor, value);
        }


        private double _newRectangleX;

        public double NewRectangleX
        {
            get => _newRectangleX;
            set => SetProperty(ref _newRectangleX, value);
        }

        private double _newRectangleY;

        public double NewRectangleY
        {
            get => _newRectangleY;
            set => SetProperty(ref _newRectangleY, value);
        }

        private double _newRectangleWidth;

        public double NewRectangleWidth
        {
            get => _newRectangleWidth;
            set => SetProperty(ref _newRectangleWidth, value);
        }

        private double _newRectangleHeight;

        public double NewRectangleHeight
        {
            get => _newRectangleHeight;
            set => SetProperty(ref _newRectangleHeight, value);
        }

        private string _newRectangleName = "New Rectangle";

        public string NewRectangleName
        {
            get => _newRectangleName;
            set => SetProperty(ref _newRectangleName, value);
        }


        private double _newEllipseCenterY;

        public double NewEllipseCenterY
        {
            get => _newEllipseCenterY;
            set => SetProperty(ref _newEllipseCenterY, value);
        }

        private double _newEllipseCenterX;

        public double NewEllipseCenterX
        {
            get => _newEllipseCenterX;
            set => SetProperty(ref _newEllipseCenterX, value);
        }

        private double _newEllipseRadiusX;

        public double NewEllipseRadiusX
        {
            get => _newEllipseRadiusX;
            set => SetProperty(ref _newEllipseRadiusX, value);
        }

        private double _newEllipseRadiusY;

        public double NewEllipseRadiusY
        {
            get => _newEllipseRadiusY;
            set => SetProperty(ref _newEllipseRadiusY, value);
        }

        private string _newEllipseName = "New Ellipse";

        public string NewEllipseName
        {
            get => _newEllipseName;
            set => SetProperty(ref _newEllipseName, value);
        }


        public ICommand AddShapeCommand { get; }
        // Командые
        public ICommand RemoveShapeCommand { get; }
        public ICommand BringToFrontCommand { get; }
        public ICommand SendToBackCommand { get; }
        public ICommand BringForwardCommand { get; }
        public ICommand SendBackwardCommand { get; }

        public ICommand SaveAsPngCommand { get; }
        public ICommand SaveAsSvgCommand { get; }

        private void ClearNewShapeInputs()
        {
            NewPointX = 0;
            NewPointY = 0;
            NewPointName = "New Point";

            NewCircleCenterX = 0;
            NewCircleCenterY = 0;
            NewCircleRadius = 10;
            NewCircleName = "New Circle";

            NewShapeStrokeColor = Colors.Black;
            NewShapeFillColor = Colors.Transparent;
        }

        public MainViewModel()
        {
            _shapes = new ObservableCollection<GeometricObject>();

            // Инициализация команд
            AddShapeCommand = new RelayCommand(ExecuteAddShape, CanExecuteAddShape);
            RemoveShapeCommand = new RelayCommand(ExecuteRemoveShape, CanExecuteRemoveShape);
            BringToFrontCommand = new RelayCommand(ExecuteBringToFront, CanExecuteZOrderChange);
            SendToBackCommand = new RelayCommand(ExecuteSendToBack, CanExecuteZOrderChange);
            BringForwardCommand = new RelayCommand(ExecuteBringForward, CanExecuteZOrderChange);
            SendBackwardCommand = new RelayCommand(ExecuteSendBackward, CanExecuteZOrderChange);
            SaveAsPngCommand = new RelayCommand(ExecuteSaveAsPng);
            SaveAsSvgCommand = new RelayCommand(ExecuteSaveAsSvg);


            // Добавим несколько фигур для примера
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            Shapes.Add(new PointObject(10, 20, "Start Point"));
            Shapes.Add(new CircleObject(50, 50, 25, "Main Circle"));
            Shapes.Add(new PointObject(100, 10, "End Point"));
        }

        private void ExecuteAddShape(object? parameter)
        {
            GeometricObject newShape = null;

            if (SelectedShapeTypeToAdd == ShapeType.Custom)
            {
                var editorWindow = new CustomShapeEditorWindow();
                if (editorWindow.ShowDialog() == true)
                {
                    var geometry = editorWindow.ResultGeometry;
                    if (geometry != null && !geometry.IsEmpty())
                    {
                        var newName = $"Custom Shape {Shapes.Count(s => s is CustomPathObject) + 1}";
                        var newCustomShape = new CustomPathObject(geometry, newName);
                        newCustomShape.FillColor = NewShapeFillColor;
                        newCustomShape.StrokeColor = NewShapeStrokeColor;
                        Shapes.Add(newCustomShape);
                    }
                }

                return; 
            }

            switch (SelectedShapeTypeToAdd)
            {
                case ShapeType.Point:
                    newShape = new PointObject(NewPointX, NewPointY, NewPointName);
                    break;
                case ShapeType.Circle:
                    if (NewCircleRadius <= 0)
                    {
                        return;
                    }

                    newShape = new CircleObject(NewCircleCenterX, NewCircleCenterY, NewCircleRadius, NewCircleName);
                    break;
                case ShapeType.Rectangle: 
                    if (NewRectangleWidth <= 0 || NewRectangleHeight <= 0)
                    {
                        return;
                    }

                    newShape = new RectangleObject(NewRectangleX, NewRectangleY, NewRectangleWidth, NewRectangleHeight,
                        NewRectangleName);
                    break;
                case ShapeType.Ellipse:
                    if (NewEllipseRadiusX <= 0 || NewEllipseRadiusY <= 0)
                    {
                        return;
                    }

                    newShape = new EllipseObject(NewEllipseCenterX, NewEllipseCenterY, NewEllipseRadiusX,
                        NewEllipseRadiusY, NewEllipseName);
                    break;
            }

            if (newShape != null)
            {
                newShape.StrokeColor = NewShapeStrokeColor;
                newShape.FillColor = NewShapeFillColor;
                // Для точки:
                if (newShape is PointObject p)
                {
                    p.X = Math.Max(0, Math.Min(p.X, 290)); // Оставляем небольшой отступ
                    p.Y = Math.Max(0, Math.Min(p.Y, 290));
                }
                // Для окружности:
                else if (newShape is CircleObject c)
                {

                    c.CenterX = Math.Max(c.Radius, Math.Min(c.CenterX, 300 - c.Radius));
                    c.CenterY = Math.Max(c.Radius, Math.Min(c.CenterY, 300 - c.Radius));
                    
                    if (c.Radius * 2 > 300) c.Radius = 150;
                }

                Shapes.Add(newShape);
                ClearNewShapeInputs(); 
            }
        }

        private bool CanExecuteAddShape(object? parameter)
        {
            if (SelectedShapeTypeToAdd == ShapeType.Circle && NewCircleRadius <= 0)
            {
                return false; // Нельзя добавить круг с невалидным радиусом
            }

            return true;
        }

        // Логика для RemoveShapeCommand
        private void ExecuteRemoveShape(object? parameter)
        {
            if (SelectedShape != null)
            {
                Shapes.Remove(SelectedShape);
                SelectedShape = null; // Сбросить выделение
            }
        }

        private bool CanExecuteRemoveShape(object? parameter)
        {
            return SelectedShape != null; // Удалить можно, только если что-то выбрано
        }

        private void ExecuteSaveAsPng(object? parameter)
        {
            if (parameter is not ItemsControl itemsControl) return;
            
            var itemsPresenter = FindVisualChild<ItemsPresenter>(itemsControl);
            var canvas = FindVisualChild<Canvas>(itemsPresenter);

            if (canvas == null)
            {
                MessageBox.Show("Could not find the canvas to save.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image (*.png)|*.png",
                Title = "Save Canvas as PNG"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var renderBitmap = new RenderTargetBitmap(
                        (int)canvas.ActualWidth, 
                        (int)canvas.ActualHeight,
                        96d, 96d, PixelFormats.Pbgra32);
                    
                    canvas.UpdateLayout();

                    renderBitmap.Render(canvas);
                    
                    var pngEncoder = new PngBitmapEncoder();
                    pngEncoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                    using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        pngEncoder.Save(fileStream);
                    }

                    MessageBox.Show("Canvas saved as PNG successfully!", "Success", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save as PNG. Error: {ex.Message}", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private T? FindVisualChild<T>(DependencyObject? parent) where T : Visual
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    var childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }

            return null;
        }

        private void ExecuteSaveAsSvg(object? parameter)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "SVG Image (*.svg)|*.svg",
                Title = "Save Canvas as SVG"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var svgDoc = new SvgDocument

                    {
                        Width = new SvgUnit(SvgUnitType.Pixel, 800), 
                        Height = new SvgUnit(SvgUnitType.Pixel, 600),
                        ViewBox = new SvgViewBox(0, 0, 800, 600),
                    };
                    
                    foreach (var shape in Shapes)
                    {
                        SvgElement? svgElement = shape switch
                        {
                            PointObject p => new SvgCircle
                            {
                                // Явное приведение double -> float
                                CenterX = new SvgUnit((float)p.X), CenterY = new SvgUnit((float)p.Y),
                                Radius = new SvgUnit(2),
                                Fill = new SvgColourServer(p.FillColor.ToSystemDrawingColor()),
                            },
                            CircleObject c => new SvgCircle
                            {
                                CenterX = new SvgUnit((float)c.CenterX), CenterY = new SvgUnit((float)c.CenterY),
                                Radius = new SvgUnit((float)c.Radius),
                                Stroke = new SvgColourServer(c.StrokeColor.ToSystemDrawingColor()),
                                StrokeWidth = new SvgUnit((float)c.StrokeThickness),
                                Fill = new SvgColourServer(c.FillColor.ToSystemDrawingColor())
                            },
                            RectangleObject r => new SvgRectangle
                            {
                                X = new SvgUnit((float)r.X), Y = new SvgUnit((float)r.Y),
                                Width = new SvgUnit((float)r.Width), Height = new SvgUnit((float)r.Height),
                                Stroke = new SvgColourServer(r.StrokeColor.ToSystemDrawingColor()),
                                StrokeWidth = new SvgUnit((float)r.StrokeThickness),
                                Fill = new SvgColourServer(r.FillColor.ToSystemDrawingColor())
                            },
                            EllipseObject e => new SvgEllipse
                            {
                                CenterX = new SvgUnit((float)e.CenterX), CenterY = new SvgUnit((float)e.CenterY),
                                RadiusX = new SvgUnit((float)e.RadiusX), RadiusY = new SvgUnit((float)e.RadiusY),
                                Stroke = new SvgColourServer(e.StrokeColor.ToSystemDrawingColor()),
                                StrokeWidth = new SvgUnit((float)e.StrokeThickness),
                                Fill = new SvgColourServer(e.FillColor.ToSystemDrawingColor())
                            },

                            // Здесь ничего не меняется, т.к. FromPathGeometry уже возвращает нужный тип
                            CustomPathObject cp => new SvgPath
                            {
                                PathData = SvgPathBuilder.FromPathGeometry(cp.DefiningGeometry),
                                Stroke = new SvgColourServer(cp.StrokeColor.ToSystemDrawingColor()),
                                StrokeWidth = new SvgUnit((float)cp.StrokeThickness),
                                Fill = new SvgColourServer(cp.FillColor.ToSystemDrawingColor())
                            },
                            _ => null
                        };

                        if (svgElement != null)
                        {
                            svgDoc.Children.Add(svgElement);
                        }
                    }

                    svgDoc.Write(saveFileDialog.FileName);

                    MessageBox.Show("Canvas saved as SVG successfully!", "Success", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save as SVG. Error: {ex.Message}", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }
    }
}