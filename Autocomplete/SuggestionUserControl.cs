using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Autocomplete
{
    public class SuggestionModel
    {
        public string Name { get; set; }
    }

    public class SuggestionViewModel : BaseViewModel
    {
        private ICommand _command;
        private string _text;
        private bool _isDropdownOpen = false;
        private System.Windows.Visibility _visibility = System.Windows.Visibility.Hidden;
        private ObservableCollection<SuggestionModel> _suggestions = new ObservableCollection<SuggestionModel>();
        private SuggestionModel _suggestionModel;
        private static readonly string[] SuggestionValues = {
            "England",
            "Spain",
            "UK",
            "UEA",
            "USA",
            "France",
            "Germany",
            "Netherlands",
            "Estonia"
        };

        public ICommand TextChangedCommand => _command ?? (_command = new RelayCommand(
                   x =>
                   {
                       TextChanged(x as string);
                   }));

        public ICommand DropdownSelectionChanged => _command ?? (_command = new RelayCommand(
                  x =>
                  {
                      DropdownChanged();
                  }));

        public string LabelText
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(LabelText));

            }
        }

        public SuggestionModel SelectedSuggestionModel
        {
            get { return _suggestionModel; }
            set
            {
                _suggestionModel = value;
                DropdownChanged();
                if (_suggestionModel != null && !string.IsNullOrEmpty(_suggestionModel.Name))
                {
                    LabelText = _suggestionModel.Name;
                }

                OnPropertyChanged(nameof(SelectedSuggestionModel));
            }
        }

        public bool IsDropdownOpen
        {
            get { return _isDropdownOpen; }
            set
            {
                _isDropdownOpen = value;
                OnPropertyChanged(nameof(IsDropdownOpen));

            }
        }

        public System.Windows.Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public ObservableCollection<SuggestionModel> Suggestions
        {
            get { return _suggestions; }
            set
            {
                _suggestions = value;
                OnPropertyChanged(nameof(Suggestions));
            }
        }

        private void DropdownChanged()
        {
            IsDropdownOpen = false;
            Visibility = System.Windows.Visibility.Hidden;
        }

        private void TextChanged(string text)
        {
            if (text is null)
                return;

            var suggestions = SuggestionValues.ToList().Where(p => p.ToLower().Contains(text.ToLower())).ToList();

            Suggestions = new ObservableCollection<SuggestionModel>();

            foreach (var suggestion in suggestions)
            {
                SuggestionModel suggestionStr = new SuggestionModel
                {
                    Name = suggestion
                };

                Suggestions.Add(suggestionStr);
            }

            if (!string.IsNullOrEmpty(text) && Suggestions.Count > 0)
            {
                IsDropdownOpen = true;
                Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                IsDropdownOpen = false;
                Suggestions = new ObservableCollection<SuggestionModel>();
                Visibility = System.Windows.Visibility.Hidden;
            }

        }
    }
}
