using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_Exam
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SelectionChangedEventArgs SelectionChanged;
        public void PropertyChanging(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        List<Student> students;
        List<Teacher> teachers;
        bool rb1;
        bool rb2;
        string findText;
        StudyDB myDB;
        public static Student selectedItem;
        public static AddPerson addPerson;
        public static UpdatePerson updatePerson;

        public ViewModel()
        {
            myDB = new StudyDB();
            students = new List<Student>();
            teachers = new List<Teacher>();
            RefreshData();
        }
        public List<Student> Students
        {
            get { return students; }
            set
            {
                students = new List<Student>(students);
                PropertyChanging("Students");
            }
        }
        public List<Teacher> Teachers
        {
            get { return teachers; }
            set
            {
                teachers = new List<Teacher>(teachers);
                PropertyChanging("Teachers");
            }
        }

        void RefreshData(string text = "")
        {
            if (rb1)
            {
                students = (from prod in myDB.Students
                            where prod.Groupe.Contains(text)
                            orderby prod.Groupe
                            select prod).ToList();
            }
            else if (rb2)
            {
                students = (from prod in myDB.Students
                            where true
                            orderby prod.AvgScore
                            select prod).ToList();
            }
            else
            {
                students = (from prod in myDB.Students
                            where prod.LastName.Contains(text)
                            orderby prod.LastName
                            select prod).ToList();
            }
            Students = new List<Student>(students);
        }

        public bool Rb
        {
            get { return rb1; }
            set
            {
                rb1 = value;
                PropertyChanging("Rb1");
                RefreshData(findText);

            }
        }
        public bool Rb2
        {
            get { return rb2; }
            set
            {
                rb2 = value;
                PropertyChanging("Rb2");
                RefreshData(findText);
            }
        }

        public string FindText
        {
            get { return findText; }
            set
            {
                findText = value;
                RefreshData(findText);
                PropertyChanging("FindText");
            }
        }
        public Student SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                PropertyChanging("SelectedItem");
            }
        }

        public ICommand AddButton
        {
            get
            {
                return new ButtonsCommand(
              () =>
              {
                  addPerson = new AddPerson();
                  addPerson.ShowDialog();
                  RefreshData();
              }
              );
            }
        }
        public ICommand UpdatePerson
        {
            get
            {
                return new ButtonsCommand(
              () =>
              {
                  updatePerson = new UpdatePerson();
                  updatePerson.ShowDialog();
                  RefreshData();
              }
              );
            }
        }
        public ICommand DeleteButton
        {
            get
            {
                return new ButtonsCommand(
              () =>
              {
                  if (selectedItem != null)
                  {
                      MessageBoxResult result = MessageBox.Show($"Действительно удалить: ID-{SelectedItem.Id}, Фамилия-{SelectedItem.LastName}", "Удалить",
                      MessageBoxButton.YesNo,
                      MessageBoxImage.Question);

                      if (result == MessageBoxResult.Yes)
                      {
                          myDB.Students.Remove(SelectedItem);
                          myDB.SaveChanges();
                          RefreshData();
                      }
                  }
              }
              );
            }
        }
    }
}
