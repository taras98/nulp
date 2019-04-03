using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.VM
{
    public class RateViewModel : INotifyPropertyChanged, IEnumerable
    {
        private string day;
        private string title;
        private string teacher;
        /*private string type;
        private string where;*/

        public string Day
        {
            get { return day; }
            private set
            {
                day = value;
                OnPropertyChanged("Day");
            }
        }
        public string Title
        {
            get { return title; }
            private set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Teacher
        {
            get { return teacher; }
            private set
            {
                teacher = value;
                OnPropertyChanged("Teacher");
            }
        }
      /*  public string Type
        {
            get { return type; }
            private set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public string Where
        {
            get { return where; }
            private set
            {
                where = value;
                OnPropertyChanged("Where");
            }
        }*/



        public ICommand LoadDataCommand { protected set; get; }

        public RateViewModel()
        {
            this.LoadDataCommand = new Command(LoadData);
        }

        private async void LoadData()
        {
            string url = "https://nulp.herokuapp.com/api/shedule/%D0%9F%D0%86-42";

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                var response = await client.GetAsync(client.BaseAddress);
                response.EnsureSuccessStatusCode(); // выброс исключения, если произошла ошибка

                // десериализация ответа в формате json
                var content = await response.Content.ReadAsStringAsync();
              //  JObject o = JObject.Parse(content);
                var x = JsonConvert.DeserializeObject<ScheduleInfo>(content);
                // var character = x[0].response;
                // var str = o.SelectToken("response");
                // var scheduleInfo = JsonConvert.DeserializeObject<ScheduleInfo>(str.ToString());

                
                this.Day = x.response.items[1].title;
                this.Title = x.response.items[1].items[0].items[0].title;
                this.Teacher = x.response.items[1].items[0].items[0].teacher;
            }
            catch (Exception ex)
            {
                this.Day = "ex";
                this.Title = "as";
                this.Teacher = "asda";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
