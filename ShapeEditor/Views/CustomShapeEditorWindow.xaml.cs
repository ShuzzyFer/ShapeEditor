using System.Windows;
using System.Windows.Media;
using ShapeEditor.ViewModels;

namespace ShapeEditor;

public partial class CustomShapeEditorWindow : Window
{
    public CustomShapeEditorWindow()
    {
        InitializeComponent();
        DataContext = new CustomShapeEditorViewModel();
    }
    
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        // Устанавливаем результат диалога и закрываем окно
        this.DialogResult = true;
    }
    
    // Свойство для получения результата
    public PathGeometry ResultGeometry => (DataContext as CustomShapeEditorViewModel)?.PathGeometry;
}