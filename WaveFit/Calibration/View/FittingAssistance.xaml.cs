using HandyControl.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WaveFit2.Calibration.View
{
    /// <summary>
    /// Interaction logic for FittingAssistance.xaml
    /// </summary>
    public partial class FittingAssistance : UserControl
    {
        public FittingAssistance()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is SearchBar searchBar)
            {
                string searchText = searchBar.Text.ToLower();

                var cards = FindVisualChildren<MaterialDesignThemes.Wpf.Card>(ExpanderGrid);

                foreach (var card in cards)
                {
                    var expander = FindVisualChild<Expander>(card);
                    if (expander != null)
                    {
                        TextBlock header = expander.Header as TextBlock;
                        if (header != null)
                        {
                            string headerText = header.Text.ToLower();
                            if (headerText.Contains(searchText))
                            {
                                if (string.IsNullOrEmpty(searchText))
                                {
                                    expander.Visibility = Visibility.Visible;
                                    expander.IsExpanded = false;
                                    expander.BringIntoView();
                                    card.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    expander.Visibility = Visibility.Visible;
                                    expander.IsExpanded = true;
                                    expander.BringIntoView();
                                    card.Visibility = Visibility.Visible;
                                }
                            }
                            else
                            {
                                expander.Visibility = Visibility.Collapsed;
                                expander.IsExpanded = false;
                                card.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                }
            }
        }

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
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

        private T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                    {
                        return childItem;
                    }
                }
            }
            return null;
        }
    }
}