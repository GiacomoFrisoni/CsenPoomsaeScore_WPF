using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CsenPoomsaeScore.Classes
{
    public static class Validator
    {
        public static bool Validate(DependencyObject depObj)
        {
            bool allValids = true;

            foreach (var c in FindVisualChildren<FrameworkElement>(depObj))
            {
                DependencyProperty p = null;

                if (c is TextBox)
                {
                    p = TextBox.TextProperty;
                }
                else if (c is ComboBox)
                {
                    p = ComboBox.TextProperty;
                }

                if (p != null && c.GetBindingExpression(p) != null)
                {
                    c.GetBindingExpression(p).UpdateSource();
                    if (Validation.GetHasError(c)) allValids = false;
                }
            }
            return allValids;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
