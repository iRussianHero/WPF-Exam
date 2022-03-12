using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPF_Exam
{
    public class AddPersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void PropertyChanging(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        StudyDB context;

        public AddPersonViewModel()
        {
            if (ViewModel.selectedItem == null) return;
            context = new StudyDB();
            LastName = ViewModel.selectedItem.LastName;
            Groupe = ViewModel.selectedItem.Groupe;
            Teacher = ViewModel.selectedItem.Teacher;
            AvgScore = ViewModel.selectedItem.AvgScore;
        }

        string lastName;
        string groupe;
        string teacher;
        double avgScore;

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                PropertyChanging("LastName");
            }
        }
        public string Groupe
        {
            get { return groupe; }
            set
            {
                groupe = value;
                PropertyChanging("Groupe");
            }
        }
        public string Teacher
        {
            get { return teacher; }
            set
            {
                teacher = value;
                PropertyChanging("Teacher");
            }
        }
        public double AvgScore
        {
            get { return avgScore; }
            set
            {
                avgScore = value;
                PropertyChanging("AvgScore");
            }
        }

        public ICommand AddButton
        {
            get
            {
                return new ButtonsCommand(() =>
                {
                    context.Students.Add(
                        new Student()
                        {
                            LastName = lastName,
                            Groupe = groupe,
                            Teacher = teacher,
                            AvgScore = avgScore
                        }
                        );
                    context.SaveChanges();
                    ViewModel.addPerson.Close();
                });
            }
        }
        public ICommand UpdateButton
        {
            get
            {
                return new ButtonsCommand(() =>
                {
                    if (ViewModel.selectedItem == null) return;
                    Student update = context.Students.Find(ViewModel.selectedItem.Id);
                    update.LastName = lastName;
                    update.Groupe = groupe;
                    update.Teacher = teacher;
                    update.AvgScore = avgScore;
                    context.SaveChanges();
                    ViewModel.updatePerson.Close();
                });
            }
        }
        public ICommand CloseButton
        {
            get
            {
                return new ButtonsCommand(() =>
                {
                    ViewModel.addPerson.Close();
                }
           );
            }
        }
        public ICommand CloseButtonUP
        {
            get
            {
                return new ButtonsCommand(() =>
                {
                    ViewModel.updatePerson.Close();
                }
           );
            }
        }
    }
}
