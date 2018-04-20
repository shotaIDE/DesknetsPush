using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesknetsPush
{
    public partial class FormMain : Form
    {
        private static string APPLICATION_NAME = "desknet's NEO Push";
        private static string SETTINGS_PATH = "settings.txt";
        private static int BALLOON_TIMEOUT = 10 * 1000; // 通知を表示する時間 [ms]

        private string desknetsUrl;
        private string userName;
        private string password;
        private string slackIncomingWebhookUrl;

        private DesknetsData desknetsData;
        private DesknetsSchedule[] pushSchedules;
        private int numPushSchedules;
        private int targetSchedule;
        private DateTime pushTime;
        private DateTime pushLimitTime;
        private string pushMessage;
        private bool pushed;
        private bool endPushSchedules;

        public FormMain()
        {
            InitializeComponent();

            updateSettings();

            var desknetsData = new DesknetsData(desknetsUrl, userName, password);
            desknetsData.updateScheduleData();
            pushSchedules = desknetsData.getDesknetsSchedule();
            numPushSchedules = pushSchedules.Length;
            targetSchedule = -1;
            pushed = true;
            endPushSchedules = false;

            updateListBox();
            runMainCheck();
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIconEvent_MouseClick(object sender, MouseEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void timerPush_Tick(object sender, EventArgs e)
        {
            runMainCheck();
        }

        private void updateSettings()
        {
            int lineid = 0;
            string[] lines = File.ReadAllLines(SETTINGS_PATH);

            desknetsUrl = lines[lineid++];
            userName = lines[lineid++];
            password = lines[lineid++];
            slackIncomingWebhookUrl = lines[lineid++];
        }

        private void runMainCheck()
        {
            timerPush.Enabled = false;

            if (endPushSchedules)
            {
                this.Close();
            }

            while (true)
            {
                if (pushed)
                {
                    // スケジュールの更新の必要がある場合
                    int updateResult = updateTargetSchedule();
                    if (updateResult == -1)
                    {
                        // 通知するスケジュールが残っていない場合
                        endPushSchedules = true;
                        break;
                    }
                    // 通知するスケジュールが残っている場合
                    pushed = false;
                }

                int checkResult = checkPushTime();
                if (checkResult == -1)
                {
                    // PUSH通知限界の時刻を過ぎていた場合
                    pushed = true;
                    continue;
                }
                else if (checkResult == 1)
                {
                    // PUSH通知の時刻の場合
                    notifyIconEvent.Text = APPLICATION_NAME;
                    notifyIconEvent.BalloonTipText = pushMessage;
                    notifyIconEvent.ShowBalloonTip(BALLOON_TIMEOUT);
                    pushed = true;
                    continue;
                }
                else
                {
                    // 待機中の場合
                    break;
                }
            }

            timerPush.Enabled = true;
        }

        private int updateTargetSchedule()
        {
            ++targetSchedule;

            if (targetSchedule >= numPushSchedules)
            {
                // 通知するスケジュールが残っていない場合
                updateNotifyIconText();
                return -1;
            }

            // 通知するスケジュールが残っている場合
            var startTime = pushSchedules[targetSchedule].startTime;
            var titleWoTime = pushSchedules[targetSchedule].titleWoTime;
            pushTime = startTime.AddMinutes(-5);
            pushLimitTime = startTime.AddMinutes(-2);
            pushMessage = "まもなく，" + titleWoTime + "の時刻です";
            updateNotifyIconText(startTime, titleWoTime);

            return 0;
        }

        private int checkPushTime()
        {
            var now = DateTime.Now;
            if (now > pushLimitTime)
            {
                // PUSH通知限界の時刻を過ぎていた場合
                return -1;
            }
            else if (now >= pushTime && now <= pushLimitTime)
            {
                // PUSH通知の時刻の場合
                return 1;
            }
            return 0;
        }

        private void updateListBox()
        {
            foreach (var schedule in pushSchedules)
            {
                string item = String.Format("{0} {1}", schedule.startTime.ToString("HH:mm"), schedule.titleWoTime);
                listBoxPushSchedules.Items.Add(item);
            }
        }

        private void updateNotifyIconText(DateTime startTime, string eventTitle)
        {
            string text = String.Format("次の予定は，{0}- {1} です", startTime.ToString("HH:mm"), eventTitle);
            notifyIconEvent.Text = text;
        }

        private void updateNotifyIconText()
        {
            notifyIconEvent.Text = "おめでとうございます！今日の予定は全て終了しました";
        }
    }
}
