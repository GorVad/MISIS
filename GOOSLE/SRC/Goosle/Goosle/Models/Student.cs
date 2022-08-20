using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Goosle.Models
{
    public class Student : INotifyPropertyChanged
    {
        public bool isFromData = false;
        public bool _isChanged = false;
        public bool _isAttendFromData = false;
        public int _pointsFromData = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isAttend = false;
        private int _points = 0;
        public Brush Color { get; set; }
        public string Name { get; private set; }
        public int Id { get; private set; }
        public bool isAttend
        {
            get => _isAttend;
            set
            {
                _isAttend = value;
                NotifyPropertyChanged("isAttend");
                if (value != _isAttendFromData)
                {
                    isChanged = true;
                }
                else
                {
                    isChanged = false;
                }
            }
        }
        public bool isChanged
        {
            get => _isChanged;
            set
            {
                _isChanged = value;
                NotifyPropertyChanged("isAttend");
            }
        }
        public int Points
        {
            get => _points;
            set
            {
                _points = value;
                NotifyPropertyChanged("Points");
                if (value != _pointsFromData)
                {
                    isChanged = true;
                }
                else
                {
                    isChanged = false;
                }
            }
        }

        public Student(int id, string Name)
        {
            this.Name = Name;
            this.Id = id;
        }

        public void SetFromData(bool isAttend, int points)
        {
            isFromData = true;
            _points = points;
            _isAttend = isAttend;
            _isAttendFromData = isAttend;
            _pointsFromData = points;
            NotifyPropertyChanged("Points");
            NotifyPropertyChanged("isAttend");
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
