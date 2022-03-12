using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_Exam
{
    class ViewModel:INotifyPropertyChanged
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
        public static AddPerson addPerson;
        public List<Student> Students { get; private set; }

        public ViewModel()
        {
            myDB = new StudyDB();
            students = new List<Student>();
            teachers = new List<Teacher>();
            RefreshData();
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
            Students = students;
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
    }
}
