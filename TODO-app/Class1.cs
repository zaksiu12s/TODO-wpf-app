using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO_app
{
    internal class TodoItem : INotifyPropertyChanged
    {
        private string todoText;
        private bool isRead;

        public string TodoText
        {
            get { return todoText; }
            set
            {
                if (todoText != value)
                {
                    todoText = value;
                    OnPropertyChanged(nameof(TodoText));
                }
            }
        }

        public bool IsRead
        {
            get { return isRead; }
            set
            {
                if (isRead != value)
                {
                    isRead = value;
                    OnPropertyChanged(nameof(IsRead));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TodoItem(string todoText, bool isRead)
        {
            TodoText = todoText;
            IsRead = isRead;
        }
    }
}
