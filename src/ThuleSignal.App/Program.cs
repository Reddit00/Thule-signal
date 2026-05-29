using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using Terminal.Gui;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.App.Services.Infrastructure;
using Application = Terminal.Gui.Application;
using Window = Terminal.Gui.Window;
using FrameView = Terminal.Gui.FrameView;
using Label = Terminal.Gui.Label;
using TextField = Terminal.Gui.TextField;
using RadioGroup = Terminal.Gui.RadioGroup;
using Button = Terminal.Gui.Button;
using ListView = Terminal.Gui.ListView;
using MessageBox = Terminal.Gui.MessageBox;
using ProgressBar = Terminal.Gui.ProgressBar;
using ThuleSignal.Domain.Entities;

namespace ThuleSignal.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sqlRepository = new SqlTrackRepository();
            var audioOutput = new HardwareAudioOutput(); 
            audioOutput.InitializeDevice();

            List<Track> currentTracksInUi = new List<Track>();

            Application.Init();
            var top = Application.Top;

            var win = new Window("🎧 THULE-SIGNAL: Premium Media Player & SQL Database")
            {
                X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill()
            };
            top.Add(win);

            var leftPane = new FrameView("Додати новий трек у Базу Даних (SQLite)")
            {
                X = 0, Y = 0, Width = Dim.Percent(42), Height = Dim.Fill()
            };

            var lblId = new Label("ID треку:") { X = 1, Y = 1 };
            var txtId = new TextField("track-" + Guid.NewGuid().ToString().Substring(0, 4)) { X = 15, Y = 1, Width = Dim.Fill() - 2 };

            var lblTitle = new Label("Назва:") { X = 1, Y = 3 };
            var txtTitle = new TextField("Нова адіо-доріжка") { X = 15, Y = 3, Width = Dim.Fill() - 2 };

            var lblSource = new Label("Файл/Шлях:") { X = 1, Y = 5 };
            var txtSource = new TextField("song14.mp3") { X = 15, Y = 5, Width = Dim.Fill() - 2 };

            var lblType = new Label("Тип media:") { X = 1, Y = 7 };
            var radioType = new RadioGroup(new NStack.ustring[] { "Podcast (Підкаст)", "Stream (Радіо-потік)" }) { X = 15, Y = 7 };

            var btnAdd = new Button("Зберегти трек") { X = 1, Y = 11, IsDefault = true };
            var btnExit = new Button("Вихід") { X = 22, Y = 11 };

            leftPane.Add(lblId, txtId, lblTitle, txtTitle, lblSource, txtSource, lblType, radioType, btnAdd, btnExit);

            var rightPane = new FrameView("Поточна Медіатека та Керування Звуком")
            {
                X = Pos.Right(leftPane), Y = 0, Width = Dim.Fill(), Height = Dim.Fill()
            };

            var listView = new ListView() { X = 1, Y = 1, Width = Dim.Fill() - 2, Height = Dim.Fill() - 7 };
            var lblTime = new Label("00:00 / 00:00") { X = 1, Y = Pos.Bottom(listView) + 1 };
            var progressBar = new ProgressBar() { X = 1, Y = Pos.Bottom(lblTime), Width = Dim.Fill() - 2 };
            
            var btnPlay = new Button("Грати") { X = 1, Y = Pos.AnchorEnd(2) };
            var btnStop = new Button("Стоп") { X = 12, Y = Pos.AnchorEnd(2) };
            var btnRewind = new Button("-10с") { X = 23, Y = Pos.AnchorEnd(2) };
            var btnForward = new Button("+10с") { X = 35, Y = Pos.AnchorEnd(2) };
            var btnRefresh = new Button("Оновити") { X = 47, Y = Pos.AnchorEnd(2) };
            
            rightPane.Add(listView, lblTime, progressBar, btnPlay, btnStop, btnRewind, btnForward, btnRefresh);
            win.Add(leftPane, rightPane);

            Action refreshListAction = () =>
            {
                currentTracksInUi = sqlRepository.GetAll().ToList();
                listView.SetSource(currentTracksInUi.Select(t => $"[{t.TrackGenre}] {t.Title}").ToList());
            };

            bool timerCallback()
            {
                if (audioOutput.IsPlaying())
                {
                    double current = audioOutput.GetCurrentPositionSeconds();
                    double total = audioOutput.GetTotalDurationSeconds();

                    if (total > 0)
                    {
                        progressBar.Fraction = (float)(current / total);

                        var curTime = TimeSpan.FromSeconds(current);
                        var totTime = TimeSpan.FromSeconds(total);
                        lblTime.Text = $"{curTime.Minutes:D2}:{curTime.Seconds:D2} / {totTime.Minutes:D2}:{totTime.Seconds:D2}";
                    }
                }
                else if (progressBar.Fraction > 0 && !audioOutput.IsPlaying())
                {
                    progressBar.Fraction = 0f;
                    lblTime.Text = "00:00 / 00:00";
                }
                return true; 
            }

            Application.MainLoop.AddTimeout(TimeSpan.FromMilliseconds(250), (loop) => timerCallback());

            btnPlay.Clicked += () =>
            {
                if (currentTracksInUi.Count == 0) return;

                int selectedIndex = listView.SelectedItem;
                if (selectedIndex >= 0 && selectedIndex < currentTracksInUi.Count)
                {
                    Track trackToPlay = currentTracksInUi[selectedIndex];
                    
                    try
                    {
                        audioOutput.StopStream();
                        audioOutput.PlayStream(trackToPlay);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ErrorQuery("Помилка звуку", ex.Message, "ОК");
                    }
                }
            };

            btnStop.Clicked += () =>
            {
                audioOutput.StopStream();
            };

            btnRewind.Clicked += () =>
            {
                if (!audioOutput.IsPlaying()) return;

                double current = audioOutput.GetCurrentPositionSeconds();
                int newPos = (int)current - 10;
                
                audioOutput.SetPosition(newPos < 0 ? 0 : newPos);
                System.Diagnostics.Debug.WriteLine($"[UI] Перемотано назад на: {newPos} сек.");
            };

            btnForward.Clicked += () =>
            {
                if (!audioOutput.IsPlaying()) return;

                double current = audioOutput.GetCurrentPositionSeconds();
                double total = audioOutput.GetTotalDurationSeconds();
                int newPos = (int)current + 10;
                
                if (newPos < total)
                {
                    audioOutput.SetPosition(newPos);
                    System.Diagnostics.Debug.WriteLine($"[UI] Перемотано вперед на: {newPos} сек.");
                }
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
                    MessageBox.Query("Успіх", "Трек успішно записано в базу SQLite!", "ОК");
                }
                catch (Exception ex)
                {
                    MessageBox.ErrorQuery("Помилка", ex.Message, "ОК");
                }
            };

            btnExit.Clicked += () => {
                audioOutput.StopStream(); 
                Application.RequestStop();
            };
            
            btnRefresh.Clicked += () => refreshListAction();

            refreshListAction();
            Application.Run();
            Application.Shutdown();
        }
    }
}