using Microsoft.Xaml.Behaviors;
using ShapeEditor.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShapeEditor.Behaviors
{
    public class CanvasInteractionBehavior : Behavior<ItemsControl>
    {
        // Поля для отслеживания состояния перетаскивания
        private bool _isDragging = false;
        private Point _dragStartPoint;
        private GeometricObject? _draggedObject;
        
        private const double CanvasWidth = 800;
        private const double CanvasHeight = 600;

        // DependencyProperty для привязки к коллекции фигур из ViewModel
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<GeometricObject>), typeof(CanvasInteractionBehavior), new PropertyMetadata(null));

        public IEnumerable<GeometricObject> ItemsSource
        {
            get { return (IEnumerable<GeometricObject>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // DependencyProperty для двусторонней привязки к выбранной фигуре в ViewModel
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(GeometricObject), typeof(CanvasInteractionBehavior), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public GeometricObject SelectedItem
        {
            get { return (GeometricObject)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }


        // Методы жизненного цикла поведения
        protected override void OnAttached()
        {
            base.OnAttached();
            // AssociatedObject - это ItemsControl, к которому мы прикрепили поведение
            AssociatedObject.PreviewMouseLeftButtonDown += OnMouseDown;
            AssociatedObject.PreviewMouseMove += OnMouseMove;
            AssociatedObject.PreviewMouseLeftButtonUp += OnMouseUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseDown;
            AssociatedObject.PreviewMouseMove -= OnMouseMove;
            AssociatedObject.PreviewMouseLeftButtonUp -= OnMouseUp;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var clickPoint = e.GetPosition(AssociatedObject);
            _draggedObject = FindObjectUnderCursor(clickPoint);
            
            SelectedItem = _draggedObject; 

            if (_draggedObject != null)
            {
                _isDragging = true;
                _dragStartPoint = clickPoint;
                AssociatedObject.CaptureMouse();
                e.Handled = true; 
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && _draggedObject != null)
            {
                var currentPoint = e.GetPosition(AssociatedObject);
                var offset = currentPoint - _dragStartPoint;
                
                if (_draggedObject is PointObject point)
                {
                    point.X += offset.X;
                    point.Y += offset.Y;
                }
                else if (_draggedObject is CircleObject circle)
                {
                    circle.CenterX += offset.X;
                    circle.CenterY += offset.Y;
                }
                else if (_draggedObject is RectangleObject rect) 
                {
                    rect.X += offset.X;
                    rect.Y += offset.Y;
                }
                else if (_draggedObject is EllipseObject ellipse) 
                {
                    ellipse.CenterX += offset.X;
                    ellipse.CenterY += offset.Y;
                }
                else if (_draggedObject is CustomPathObject customPath)
                {
                    customPath.X += offset.X;
                    customPath.Y += offset.Y;
                    
                    var transformedGeometry = customPath.DefiningGeometry.Clone();
                    transformedGeometry.Transform = new TranslateTransform(offset.X, offset.Y);
                }
                
                _dragStartPoint = currentPoint;
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
                _draggedObject = null;
                // Освобождаем мышь
                AssociatedObject.ReleaseMouseCapture();
                e.Handled = true;
            }
        }

        // Вспомогательный метод для поиска объекта под курсором
        private GeometricObject? FindObjectUnderCursor(Point position)
        {
            var hitTestResult = VisualTreeHelper.HitTest(AssociatedObject, position);
            if (hitTestResult == null)
            {
                return null;
            }
            
            var element = hitTestResult.VisualHit;
            while (element != null)
            {
                if (element is ContentPresenter presenter)
                {
                    if (presenter.DataContext is GeometricObject geometricObject)
                    {
                        return geometricObject;
                    }
                }
                element = VisualTreeHelper.GetParent(element) as UIElement;
            }
            return null;
        }
    }
}