using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;


namespace App1.VM
{
    public class DayViewModel : INotifyPropertyChanged
    {
        private string day;
        private List<string> number_of_lesson;
        private List<string> subject;
        private List<string> teacher;
        private List<string> where;
        private List<string> type;
        private List<object> subgroup;
        private List<int> fraction;
        private List<bool> active;
        private string test;



        public int count_of_lessons { get; set; }
        public string group { get; set; }
        public int number_of_day { get; set; }
        //  public int count_of_lessons { get; set; }


        public string Day
        {
            get { return day; }
            private set
            {
                day = value;
                OnPropertyChanged("Day");
            }
        }
        public List<string> Number_of_lesson
        {
            get { return number_of_lesson; }
            private set
            {
                number_of_lesson = value;
                OnPropertyChanged("Number_of_lesson");
            }
        }
        public List<string> Subject
        {
            get { return subject; }
            private set
            {
                subject = value;
                OnPropertyChanged("Subject");
            }
        }
        public List<string> Teacher
        {
            get { return teacher; }
            private set
            {
                teacher = value;
                OnPropertyChanged("Teacher");
            }
        }
        public List<string> Where
        {
            get { return where; }
            private set
            {
                where = value;
                OnPropertyChanged("Where");
            }
        }
        public List<string> Type
        {
            get { return type; }
            private set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public List<object> Subgroup
        {
            get { return subgroup; }
            private set
            {
                subgroup = value;
                OnPropertyChanged("subgroup");
            }
        }
        public List<int> Fraction
        {
            get { return fraction; }
            private set
            {
                fraction = value;
                OnPropertyChanged("Fraction");
            }
        }
        public List<bool> Active
        {
            get { return active; }
            private set
            {
                active = value;
                OnPropertyChanged("Active");
            }
        }



        public DayViewModel(string _group, int _number_of_day)
        {
            group = _group;
            _number_of_day = number_of_day;
            LoadDataCommand = new Command(LoadData);
        }

        public ICommand LoadDataCommand { protected set; get; }
        private async void LoadData()
        {
            string url = "https://nulp.herokuapp.com/api/shedule/" + group;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                var response = await client.GetAsync(client.BaseAddress);
                response.EnsureSuccessStatusCode(); // выброс исключения, если произошла ошибка

                // десериализация ответа в формате json
                var content = await response.Content.ReadAsStringAsync();
                var x = JsonConvert.DeserializeObject<ScheduleInfo>(content);
                try
                {
                    count_of_lessons = x.response.items[number_of_day].items.Count;
                    Day = x.response.items[number_of_day].title;
                    Number_of_lesson = new List<string> { };
                    Subject = new List<string> { };
                    Teacher = new List<string> { };
                    Where = new List<string> { };
                    Type = new List<string> { };
                    Subgroup = new List<object> { };
                    Fraction = new List<int> { };
                    Active = new List<bool> { };

                    for (int i = 0; i < x.response.items[number_of_day].items.Capacity; i++)
                    {
                        Number_of_lesson.Add(x.response.items[number_of_day].items[i].title);

                        for (int j = 0; j < x.response.items[number_of_day].items[i].items.Count; j++)
                        {


                            Subject.Add(x.response.items[number_of_day].items[i].items[j].title);
                            Teacher.Add(x.response.items[number_of_day].items[i].items[j].teacher);
                            Where.Add(x.response.items[number_of_day].items[i].items[j].where);
                            Type.Add(x.response.items[number_of_day].items[i].items[j].type);
                            Subgroup.Add(x.response.items[number_of_day].items[i].items[j].subgroup);
                            Fraction.Add(x.response.items[number_of_day].items[i].items[j].fraction);
                            Active.Add(x.response.items[number_of_day].items[i].items[j].active);
                        }
                    }


                }
                catch
                {
                    Day = "error";
                    count_of_lessons = 0;
                    Number_of_lesson.Add("eroor");


                }

            }
            catch (Exception ex)
            {
                Day = "Error";
                //   this.Subject = "Error";
                // this.Teacher = "Error";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
