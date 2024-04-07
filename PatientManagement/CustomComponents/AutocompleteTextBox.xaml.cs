using System.Collections.Generic;
using System.Threading.Tasks;
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
            //hintTextBlock.Text = HintText;
        }

        private bool _isFirstLoad = true;
        private bool _isTextAutoCompleted;

        private string _hintText = string.Empty;
        public string HintText
        {
            get { return _hintText; }
            set
            {
                _hintText = value;
                hintTextBlock.Text = value;
            }
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

        private bool _acceptsReturn;

        public bool AcceptsReturn
        {
            get { return _acceptsReturn; }
            set
            {
                _acceptsReturn = value;
                textBox.AcceptsReturn = value; // Assuming textBox is the reference to the underlying TextBox control
            }
        }

        private TextWrapping _textWrapping;

        public TextWrapping TextWrapping
        {
            get { return _textWrapping; }
            set
            {

                _textWrapping = value;
                textBox.TextWrapping = value; // Assuming textBox is the reference to the underlying TextBox control

            }
        }

        // Event handler for text change to filter autocomplete suggestions
        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentText = textBox.Text.ToLower();

            if (_isFirstLoad)
            {
                _isFirstLoad = false;
                hintTextBlock.Visibility = Visibility.Hidden;
                return;
            }
            else
            {
                if (currentText == string.Empty)
                {
                    hintTextBlock.Visibility = Visibility.Visible;
                    suggestionPopup.IsOpen = false;
                }
                else
                {
                    hintTextBlock.Visibility = Visibility.Hidden;

                    // Capture the AutocompleteSuggestions collection
                    var suggestions = AutocompleteSuggestions;

                    // Run heavy computation asynchronously
                    var filteredSuggestions = await Task.Run(() =>
                    {
                        var filtered = new List<string>();

                        foreach (var suggestion in suggestions)
                        {
                            if (suggestion.ToLower().Contains(currentText.ToLower()))
                            {
                                filtered.Add(suggestion);
                            }
                        }
                        return filtered;
                    });

                    suggestionListBox.ItemsSource = filteredSuggestions;

                    // Autofocus on first element in list
                    if (suggestionListBox.SelectedIndex == -1 && filteredSuggestions.Count > 0)
                    {
                        suggestionListBox.SelectedIndex = 0;
                        suggestionListBox.ScrollIntoView(suggestionListBox.SelectedItem);
                    }
                    if (!_isTextAutoCompleted)
                    {
                        suggestionPopup.IsOpen = filteredSuggestions.Count > 0;
                    }
                }
            }

        }



        private void textBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            _isTextAutoCompleted = false;
            if (suggestionPopup.IsOpen == false)
            {
                return;
            }
            else
            {
                if (e.Key == Key.Escape)
                {
                    suggestionPopup.IsOpen = false;
                    e.Handled = true;
                }
                else if (e.Key == Key.Down)
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
                else if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    if (suggestionListBox.SelectedItem != null)
                    {
                        textBox.Text = suggestionListBox.SelectedItem.ToString();
                        textBox.CaretIndex = textBox.Text?.Length ?? 0;
                        _isTextAutoCompleted = true;
                        suggestionPopup.IsOpen = false;
                    }
                    e.Handled = true;
                }
            }

        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            suggestionPopup.IsOpen = false;
            if (textBox.Text == string.Empty)
            {
                hintTextBlock.Visibility = Visibility.Visible;
            }
        }
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != string.Empty)
            {
                hintTextBlock.Visibility = Visibility.Hidden;
            }
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
