using System;
using System.ComponentModel;

namespace WpfAppMy.Data
{
    public class Data_file : INotifyPropertyChanged
    {
        private string? _id;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        private string? _name;
        public string? name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(); }
        }
        private string? _type;
        public string? type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged(); }
        }
        private string? _content;
        public string? content
        {
            get { return _content; }
            set { _content = value; NotifyPropertyChanged(); }
        }
        private uint? _size;
        public uint? size
        {
            get { return _size; }
            set { _size = value; NotifyPropertyChanged(); }
        }
        private DateTime? _created;
        public DateTime? created
        {
            get { return _created; }
            set { _created = value; NotifyPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
