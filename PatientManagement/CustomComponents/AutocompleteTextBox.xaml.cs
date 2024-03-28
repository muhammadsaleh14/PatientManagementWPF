using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PatientManagement.CustomComponents
{
    /// <summary>
    /// Interaction logic for AutocompleteTextBox.xaml
    /// </summary>
    public partial class AutocompleteTextBox : UserControl
    {
        public AutocompleteTextBox()
        {
            InitializeComponent();

        }

        // Dependency property for autocomplete suggestions
        public static readonly DependencyProperty AutocompleteSuggestionsProperty =
            DependencyProperty.Register("AutocompleteSuggestions", typeof(IEnumerable<string>), typeof(AutocompleteTextBox));

        public IEnumerable<string> AutocompleteSuggestions
        {
            get { return (IEnumerable<string>)GetValue(AutocompleteSuggestionsProperty); }
            set { SetValue(AutocompleteSuggestionsProperty, value); }
        }


        // Dependency property for the text of the TextBox
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AutocompleteTextBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Event handler for text change to filter autocomplete suggestions
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AutocompleteSuggestions != null)
            {
                var textBox = (TextBox)sender;
                var currentText = textBox.Text.ToLower();
                var filteredSuggestions = new List<string>();

                foreach (var suggestion in AutocompleteSuggestions)
                {
                    if (suggestion.ToLower().StartsWith(currentText))
                    {
                        filteredSuggestions.Add(suggestion);
                    }
                }
                suggestionListBox.ItemsSource = filteredSuggestions;

                //autofocus on first element in lust
                if (suggestionListBox.SelectedIndex == -1)
                {
                    suggestionListBox.SelectedIndex++;
                    suggestionListBox.ScrollIntoView(suggestionListBox.SelectedItem);
                }

                suggestionPopup.IsOpen = filteredSuggestions.Count > 0 ? true : false;



                // Show filtered suggestions
                // (you might implement this part based on your UI requirements)
            }
        }


        private void textBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                suggestionPopup.IsOpen = false;
            }

            if (e.Key == Key.Down)
            {
                if (suggestionListBox.SelectedIndex < suggestionListBox.Items.Count - 1)
                {
                    suggestionListBox.SelectedIndex++;
                    suggestionListBox.ScrollIntoView(suggestionListBox.SelectedItem);
                }
                e.Handled = true;
            }
            else if (e.Key == Key.Up)
            {
                if (suggestionListBox.SelectedIndex > 0)
                {
                    suggestionListBox.SelectedIndex--;
                    suggestionListBox.ScrollIntoView(suggestionListBox.SelectedItem);
                }
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                if (suggestionListBox.SelectedItem != null)
                {
                    textBox.Text = suggestionListBox.SelectedItem.ToString();
                    textBox.CaretIndex = textBox.Text.Length;
                    suggestionPopup.IsOpen = false;
                }
                e.Handled = true;
            }
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            suggestionPopup.IsOpen = false;
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            suggestionPopup.IsOpen = false;
        }



        private void suggestionListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                if (listBox.SelectedItem is string textBlockString)
                {

                    textBox.Text = textBlockString;
                    textBox.CaretIndex = textBlockString.Length;
                    suggestionPopup.IsOpen = false;
                }

            }
            e.Handled = true;
        }
    }


}
