using CV19.Models.Decanat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CV19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GroupsFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Group group)) return;
            if (group.Name is null) return;

            string filterText = tbGroups.Text;
            if (filterText.Length == 0) return;

            if (group.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;
            if (group.Description != null && group.Description.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }

        private void OnFilterChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var collection = (CollectionViewSource)textBox.FindResource("CollectionGroup");
            collection.View.Refresh();
        }
    }

    public static class StringExtensions
    {
        public static bool Contains(this string group, string filterText, StringComparison comp)
        {
            return group?.IndexOf(filterText, comp) >= 0;
        }
    }
}
