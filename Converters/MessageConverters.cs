using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Media;
using DCDesktop.Services;
using System;
using System.Globalization;

namespace DCDesktop.Converters
{
    public static class MessageConverterHelper
    {
        public static bool IsCurrentUser(object value)
        {
            int userId;

            switch (value)
            {
                case int i:
                    userId = i;
                    break;
                case uint ui:
                    userId = (int)ui;
                    break;
                case long l:
                    userId = (int)l;
                    break;
                case ulong ul:
                    userId = (int)ul;
                    break;
                default:
                    Console.WriteLine($"[DEBUG] Unhandled value type: {value?.GetType().Name} / value: {value}");
                    return false;
            }

            int currentUserId = (int)AuthenticationStateService.GetUserID();
            return userId == currentUserId;
        }
    }

    public class UserColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCurrentUser = MessageConverterHelper.IsCurrentUser(value);

            return new SolidColorBrush(Color.Parse(
                isCurrentUser ? "#E0F2FE" : "#F3F4F6"
            ));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class MessageAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MessageConverterHelper.IsCurrentUser(value)
                ? HorizontalAlignment.Right
                : HorizontalAlignment.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}