using System;
using System.Linq;
using Terminal.Gui; // Наш новий UI движок
using ThuleSignal.App.Services.Infrastructure;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sqlRepository = new SqlTrackRepository();

            Application.Init();
            var top = Application.Top;
            var win = new Window("🎧 THULE-SIGNAL: Premium Media Player & SQL Database DB")
            {
                X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill()
            };
            top.Add(win);

            var leftPane = new FrameView("Додати новий трек у Базу Даних (SQLite)")
            {
                X = 0, Y = 0, Width = Dim.Percent(45), Height = Dim.Fill()
            };

            var lblId = new Label("ID треку:") { X = 1, Y = 1 };
            var txtId = new TextField("track-" + Guid.NewGuid().ToString().Substring(0, 4)) { X = 15, Y = 1, Width = Dim.Fill() - 2 };

            var lblTitle = new Label("Назва:") { X = 1, Y = 3 };
            var txtTitle = new TextField("Нова аудіо-доріжка") { X = 15, Y = 3, Width = Dim.Fill() - 2 };

            var lblSource = new Label("Джерело/URL:") { X = 1, Y = 5 };
            var txtSource = new TextField("audio.mp3") { X = 15, Y = 5, Width = Dim.Fill() - 2 };

            var lblType = new Label("Тип медіа:") { X = 1, Y = 7 };
            var radioType = new RadioGroup(new NStack.ustring[] { "Podcast (Підкаст)", "Stream (Радіо-потік)" }) { X = 15, Y = 7 };

            var btnAdd = new Button("Зберегти трек") { X = 1, Y = 11, IsDefault = true };
            
            var btnExit = new Button("Вихід з системи") { X = 20, Y = 11 };

            leftPane.Add(lblId, txtId, lblTitle, txtTitle, lblSource, txtSource, lblType, radioType, btnAdd, btnExit);

            var rightPane = new FrameView("Поточна Медіатека в thule_signal.db")
            {
                X = Pos.Right(leftPane), Y = 0, Width = Dim.Fill(), Height = Dim.Fill()
            };

            var listView = new ListView() { X = 1, Y = 1, Width = Dim.Fill() - 2, Height = Dim.Fill() - 3 };
            var btnRefresh = new Button("Оновити список") { X = 1, Y = Pos.AnchorEnd(1) };
            
            rightPane.Add(listView, btnRefresh);
            win.Add(leftPane, rightPane);

            Action refreshListAction = () =>
            {
                var tracksFromDb = sqlRepository.GetAll();
                listView.SetSource(tracksFromDb.Select(t => $"[{t.TrackGenre}] {t.Title} ({t.Id})").ToList());
            };

            btnAdd.Clicked += () =>
            {
                string typeStr = radioType.SelectedItem == 0 ? "PODCAST" : "STREAM";
                try
                {
                    Track newTrack = TrackFactory.CreateTrack(
                        typeStr, 
                        txtId.Text.ToString()!, 
                        txtTitle.Text.ToString()!, 
                        txtSource.Text.ToString()!, 
                        "System_User"
                    );

                    sqlRepository.Add(newTrack);

                    txtId.Text = "track-" + Guid.NewGuid().ToString().Substring(0, 4);
                    txtTitle.Text = "Нова аудіо-доріжка";
                    
                    refreshListAction();
                    MessageBox.Query("Успіх", "Трек успішно записано у файл бази даних SQLite!", "ОК");
                }
                catch (Exception ex)
                {
                    MessageBox.ErrorQuery("Помилка", ex.Message, "ОК");
                }
            };

            btnExit.Clicked += () =>
            {
                Application.RequestStop();
            };

            btnRefresh.Clicked += () => refreshListAction();

            refreshListAction();

            Application.Run();
            Application.Shutdown();
        }
    }
}