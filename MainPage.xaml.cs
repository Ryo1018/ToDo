using System.Collections.ObjectModel;
using System.Formats.Tar;
using Microsoft.Maui.Controls;

namespace ToDo
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<ToDoItem> tasks;
        int rewardPoints = 0;

        public MainPage()
        {
            InitializeComponent();
            tasks = new ObservableCollection<ToDoItem>();
            taskListView.ItemsSource = tasks;
        }

        //private void AddTask_Clicked(object sender, EventArgs e)
        //{
        //    string taskTitle = taskEntry.Text;
        //    if (!string.IsNullOrEmpty(taskTitle))
        //    {
        //        tasks.Add(new ToDoItem { Title = taskTitle, IsDone = false });
        //        taskEntry.Text = string.Empty;
        //    }
        //}

        public class ToDoItem
        {
            public string Title { get; set; }
            public bool IsDone { get; set; }
            public int Points { get; set; }
        }

        private void ClearCompletedTasks_Clicked(object sender, EventArgs e)
        {
            // 完了したタスクをクリア
            var completedTasks = tasks.Where(task => task.IsDone).ToList();
            foreach (var completedTask in completedTasks)
            {
                tasks.Remove(completedTask);
            }
        }

        private void AddTask_Clicked(object sender, EventArgs e)
        {
            string taskTitle = taskEntry.Text;
            if (!string.IsNullOrEmpty(taskTitle))
            {
                // ポイントをランダムで設定
                Random random = new Random();
                int randomPoints = random.Next(1, 11); // ポイントの範囲を設定
                tasks.Add(new ToDoItem { Title = taskTitle, IsDone = false, Points = randomPoints });
                taskEntry.Text = string.Empty;

                // ポイントを追加
                //rewardPoints += randomPoints;
                //pointsLabel.Text = $"現在のポイント: {rewardPoints}";
            }
        }

        private void TaskToggled(object sender, ToggledEventArgs e)
        {
            var switchCell = (Switch)sender;
            ToDoItem task = (ToDoItem)switchCell.BindingContext;
            task.IsDone = e.Value;

            rewardPoints += task.Points;
            pointsLabel.Text = $"現在のポイント: {rewardPoints}";
        }

        private void ResetPoints_Clicked(object sender, EventArgs e)
        {
            // ポイントをリセット
            rewardPoints = 0;
            pointsLabel.Text = "現在のポイント: 0";
        }

        private void RewardEditSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            // ごほうびポイント欄の編集可能状態を切り替え
            rewardEntry.IsEnabled = e.Value;
        }

    }
}
