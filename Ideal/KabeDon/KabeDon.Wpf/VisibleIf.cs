using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KabeDon
{
    /// <summary>
    /// bool 値に応じて <see cref="Visibility"/> を設定するためのコンバーター。
    /// </summary>
    public class VisibleIf : IValueConverter
    {
        /// <summary>
        /// true なら、入力 true で可視。
        /// false なら 入力 false で可視。
        /// </summary>
        public bool IfTrue { get; set; } = true;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool)) return Visibility.Collapsed;

            return IfTrue == (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
