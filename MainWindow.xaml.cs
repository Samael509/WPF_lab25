using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace uyutov_lab25
{
    public partial class MainWindow : Window
    {
        // класс ViewModel наследует от DependencyObject, чтобы использовать DependencyProperty
        public class ViewModel : DependencyObject
        {
            // объявляю DependencyProperty для строки
            public static readonly DependencyProperty StringProperty =
                DependencyProperty.Register("String", typeof(string), typeof(ViewModel), new PropertyMetadata("Привет"));

            // объявляю DependencyProperty для логического значения
            public static readonly DependencyProperty BoolProperty =
                DependencyProperty.Register("Bool", typeof(bool), typeof(ViewModel), new PropertyMetadata(true));

            // объявляю DependencyProperty для целого числа
            public static readonly DependencyProperty IntProperty =
                DependencyProperty.Register("Int", typeof(int), typeof(ViewModel), new PropertyMetadata(42));

            // объявляю DependencyProperty для числа с плавающей точкой (double)
            public static readonly DependencyProperty DoubleProperty =
                DependencyProperty.Register("Double", typeof(double), typeof(ViewModel), new PropertyMetadata(99.99));

            // объявляю DependencyProperty для цвета (Color)
            public static readonly DependencyProperty ColorProperty =
                DependencyProperty.Register("Color", typeof(Color), typeof(ViewModel), new PropertyMetadata(Colors.LightBlue));

            // DependencyProperty для хранения значения
            public string String
            {
                get { return (string)GetValue(StringProperty); }
                set { SetValue(StringProperty, value); }
            }

            // свойство Bool для логического значения
            public bool Bool
            {
                get { return (bool)GetValue(BoolProperty); }
                set { SetValue(BoolProperty, value); }
            }

            // свойство Int для целого числа
            public int Int
            {
                get { return (int)GetValue(IntProperty); }
                set { SetValue(IntProperty, value); }
            }

            // свойство Double для числа с плавающей точкой
            public double Double
            {
                get { return (double)GetValue(DoubleProperty); }
                set { SetValue(DoubleProperty, value); }
            }

            // свойство Color для цвета
            public Color Color
            {
                get { return (Color)GetValue(ColorProperty); }
                set { SetValue(ColorProperty, value); }
            }
        }

        private ViewModel vm = new ViewModel();

        public MainWindow()
        {
            InitializeComponent();

            // устанавливаем DataContext
            this.DataContext = vm;
            // если хотите проверить, раскомментируйте и закомментируйте строку ниже

            // привязка 1: строка (односторонняя)
            /*Binding bindingString = new Binding("String")
            {
                Mode = BindingMode.OneWay
            };
            TextBoxString.SetBinding(TextBox.TextProperty, bindingString);*/

            // если хотите менять текст
            TextBoxString.SetBinding(TextBox.TextProperty, new Binding("String")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            // привязка 2: логическое (двусторонняя)
            CheckBoxBool.SetBinding(CheckBox.IsCheckedProperty, new Binding("Bool") { Mode = BindingMode.TwoWay });

            // привязка 3: целое число (односторонняя с обновлением при потере фокуса)
            Binding bindingInt = new Binding("Int")
            {
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            TextBoxInt.SetBinding(TextBox.TextProperty, bindingInt);

            // привязка 4: вещественное число (односторонняя)
            Binding bindingDouble = new Binding("Double")
            {
                Mode = BindingMode.OneWay
            };
            TextBoxDouble.SetBinding(TextBox.TextProperty, bindingDouble);

            // привязка 5: цвет (односторонняя)
            Binding bindingColor = new Binding("Color")
            {
                Mode = BindingMode.OneWay,
                Converter = new ColorToBrushConverter()
            };
            ColorRectangle.SetBinding(Rectangle.FillProperty, bindingColor);

            // инициализация значений. Это дефолтные значения, при необходимости можете менять их. ВАЖНО: ЗНАЧЕНИЯ ДОЛЖНЫ СООТВЕТСТВОВАТЬ ОЖИДАЕМОМУ ТИПУ
            vm.String = "Привет, мир!";
            vm.Bool = true;
            vm.Int = 123;
            vm.Double = 456.78;
            vm.Color = Colors.Black;
        }

        private void CreateDynamicBinding(object sender, RoutedEventArgs e)
        {
            var textBlock = new System.Windows.Controls.TextBlock
            {
                Margin = new Thickness(10),
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 100
            };

            var dynamicBinding = new Binding("String")
            {
                Mode = BindingMode.OneWay,
                Source = vm,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            textBlock.SetBinding(System.Windows.Controls.TextBlock.TextProperty, dynamicBinding);

            MainGrid.Children.Add(textBlock);
        }
    }

    // для преобразования Color в Brush
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Color color)
            {
                return new SolidColorBrush(color);
            }
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                return brush.Color;
            }
            return Colors.Transparent;
        }
    }
}