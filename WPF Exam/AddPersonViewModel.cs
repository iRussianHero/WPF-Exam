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

        public ICommand CloseButton
        {
            get
            {
                return new ButtonsCommand(() =>
                {
                    ViewModel.addPosition.Close();
                }
           );
            }
        }
    }
}
