using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DesknetsPush
{
    struct DesknetsSchedule
    {
        public string titleWoTime;
        public DateTime startTime;

        public DesknetsSchedule(string title, DateTime time)
        {
            this.titleWoTime = title;
            this.startTime = time;
        }
    }

    class DesknetsData
    {
        private static string MY_WEEK_CLASS = "sch-gcal-target";
        private static string DAY_CLASS = "cal-h-list-data";
        private static string EVENT_CLASS = "cal-item-box";

        private DateTime initTime;
        private int initYear;
        private int initMonth;
        private int initDay;

        private string desknetsUrl;
        private string userName;
        private string password;

        private List<DesknetsSchedule> schedules;

        public DesknetsData(string desknetsUrl, string userName, string password)
        {
            this.desknetsUrl = desknetsUrl;
            this.userName = userName;
            this.password = password;

            initTime = DateTime.Now;
            initYear = initTime.Year;
            initMonth = initTime.Month;
            initDay = initTime.Day;
        }
        
        public void updateScheduleData()
        {
            var browser = new ChromeDriver();

            schedules = new List<DesknetsSchedule>();

            string[] desknetsUrlMatch = Regex.Split(desknetsUrl, "(http://|https://)");
            if (desknetsUrlMatch.Length != 3)
            {
                throw new Exception();
            }
            string getUrl = desknetsUrlMatch[1] + userName + ":" + password + "@" + desknetsUrlMatch[2];
            //browser.Url = getUrl;
            browser.Url = "http://ide.sakura.ne.jp/";

            var myWeekSchedule = browser.FindElement(By.ClassName(MY_WEEK_CLASS));
            var myDaySchedule = myWeekSchedule.FindElement(By.ClassName(DAY_CLASS));
            var myEvents = myDaySchedule.FindElements(By.ClassName(EVENT_CLASS));

            foreach (var myEvent in myEvents)
            {
                var title = myEvent.FindElement(By.ClassName("cal-item")).GetAttribute("title");

                // 開始時刻と終了時刻が付いているイベント
                var matched = Regex.Split(title, "([0-9][0-9]):([0-9][0-9]) - ([0-9][0-9]):([0-9][0-9]) ");
                if (matched.Length != 6)
                {
                    // 開始時刻のみが付いているイベント
                    matched = Regex.Split(title, "([0-9][0-9]):([0-9][0-9]) ");
                    if (matched.Length != 4)
                    {
                        // 時刻が付いていないイベント
                        continue;
                    }
                }
                
                int startHour = int.Parse(matched[1]);
                int startMin = int.Parse(matched[2]);
                DateTime startTime = new DateTime(initYear, initMonth, initDay, startHour, startMin, 0);
                string titleWoTime = matched[matched.Length - 1];

                schedules.Add(new DesknetsSchedule(titleWoTime, startTime));
            }

            browser.Quit();
        }

        public DesknetsSchedule[] getDesknetsSchedule()
        {
            return schedules.ToArray();
        }
    }
}
