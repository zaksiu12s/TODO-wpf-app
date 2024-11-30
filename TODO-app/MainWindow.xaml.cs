using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TODO_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 

    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<TodoItem> todoList = new ObservableCollection<TodoItem>();
        private const string FilePath = "todos.txt";

        public MainWindow()
        {
            InitializeComponent();

            TodoListElement.ItemsSource = todoList;

            LoadTodos();
        }

        private void TodoAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewTodo.Text))
            {
                todoList.Add(new TodoItem(NewTodo.Text, false));
                NewTodo.Text = "";
                SaveTodos();
            }
            else
            {
                MessageBox.Show("Please enter a valid todo item.");
            }
        }

        private void DeleteTodoButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton)
            {
                TodoItem? item = (deleteButton.DataContext as TodoItem);
                if (item != null)
                {
                    todoList.Remove(item);
                    SaveTodos();
                }
            }
        }

        private void SaveTodos()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    foreach (var todo in todoList)
                    {
                        writer.WriteLine($"{todo.TodoText}|{todo.IsRead}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Błąd przy zapisywaniu listy do pliku.");
            }
        }

        private void LoadTodos()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 2)
                            {
                                string todoText = parts[0];
                                bool isRead = bool.Parse(parts[1]);
                                todoList.Add(new TodoItem(todoText, isRead));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Błąd przy odczytywaniu listy z pliku.");
                }
            }
        }

    }
}