using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShapeEditor.Behaviors
{
    /// <summary>
    /// Поведение, которое отслеживает действия мыши на Canvas
    /// и транслирует их в команды для ViewModel, предназначенные для рисования.
    /// </summary>
    public class DrawingBehavior : Behavior<Canvas>
    {
        private bool _isDrawing = false;

        #region Dependency Properties for Commands
        
        public static readonly DependencyProperty StartDrawingCommandProperty =
            DependencyProperty.Register(
                nameof(StartDrawingCommand), 
                typeof(ICommand), 
                typeof(DrawingBehavior), 
                new PropertyMetadata(null));

        public ICommand StartDrawingCommand
        {
            get { return (ICommand)GetValue(StartDrawingCommandProperty); }
            set { SetValue(StartDrawingCommandProperty, value); }
        }
        
        public static readonly DependencyProperty DrawingCommandProperty =
            DependencyProperty.Register(
                nameof(DrawingCommand), 
                typeof(ICommand), 
                typeof(DrawingBehavior), 
                new PropertyMetadata(null));

        public ICommand DrawingCommand
        {
            get { return (ICommand)GetValue(DrawingCommandProperty); }
            set { SetValue(DrawingCommandProperty, value); }
        }
        
        public static readonly DependencyProperty EndDrawingCommandProperty =
            DependencyProperty.Register(
                nameof(EndDrawingCommand), 
                typeof(ICommand), 
                typeof(DrawingBehavior), 
                new PropertyMetadata(null));

        public ICommand EndDrawingCommand
        {
            get { return (ICommand)GetValue(EndDrawingCommandProperty); }
            set { SetValue(EndDrawingCommandProperty, value); }
        }

        #endregion
        
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += OnMouseDown;
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseLeftButtonUp += OnMouseUp;
        }
        
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseLeftButtonDown -= OnMouseDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseLeftButtonUp -= OnMouseUp;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = true;
            AssociatedObject.CaptureMouse();
            
            if (StartDrawingCommand?.CanExecute(null) ?? false)
            {
                var position = e.GetPosition(AssociatedObject);
                StartDrawingCommand.Execute(position);
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                if (DrawingCommand?.CanExecute(null) ?? false)
                {
                    var position = e.GetPosition(AssociatedObject);
                    DrawingCommand.Execute(position);
                }
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDrawing)
            {
                _isDrawing = false;
                AssociatedObject.ReleaseMouseCapture();
                
                if (EndDrawingCommand?.CanExecute(null) ?? false)
                {
                    EndDrawingCommand.Execute(null);
                }
            }
        }
    }
}