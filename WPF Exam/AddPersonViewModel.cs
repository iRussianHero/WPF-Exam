using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        string lastName;
        string groupe;
        string teacher;
        double avgScore;
        string subject;

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
        public string Subject
        {
            get { return subject; }
            set
            {
                subject = value;
                PropertyChanging("Subject");
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
    }
}
