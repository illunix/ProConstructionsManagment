﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ProConstructionsManagment.Desktop.Converters
{
    // https://stackoverflow.com/a/14692829
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class VisibilityConverter : IValueConverter
    {
        public const string Invert = "Invert";

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a Visibility.");

            var bValue = (bool?)value;

            if (parameter != null && parameter as string == Invert)
                bValue = !bValue;

            return bValue.HasValue && bValue.Value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion IValueConverter Members
    }
}