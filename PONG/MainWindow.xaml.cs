using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Configuration;
using Microsoft.Win32;
using System.Windows.Media.Animation;

namespace PONG
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //hihi

        string gameState = "mainMenu";
        string winnerName = "";
        string player1Name = "Spieler 1";
        string player2Name = "Spieler 2";
        double ballPosX = 475;
        double ballPosY = 275;
        double ballSpeedX = 4;
        double ballSpeedY = 4;
        int spielerPos1 = 250;
        int spielerPos2 = 250;
        int punkte1 = 0;
        int punkte2 = 0;

        string aktuelleDatei;
        string aktuelleMusic;

        Color primaryColor;
        Color secondaryColor;
        Color fgcolor = Color.FromArgb(Convert.ToByte(ConfigurationManager.AppSettings.Get("savedFgAColor")),
                                       Convert.ToByte(ConfigurationManager.AppSettings.Get("savedFgRColor")),
                                       Convert.ToByte(ConfigurationManager.AppSettings.Get("savedFgGColor")),
                                       Convert.ToByte(ConfigurationManager.AppSettings.Get("savedFgBColor")));

        Color bgcolor = Color.FromArgb(Convert.ToByte(ConfigurationManager.AppSettings.Get("savedBgAColor")),
                                       Convert.ToByte(ConfigurationManager.AppSettings.Get("savedBgRColor")),
                                       Convert.ToByte(ConfigurationManager.AppSettings.Get("savedBgGColor")),
                                       Convert.ToByte(ConfigurationManager.AppSettings.Get("savedBgBColor")));



        Ellipse myellipse = new Ellipse();
        Rectangle player1 = new Rectangle();
        Rectangle player2 = new Rectangle();

        Button spielstart = new Button();
        Button settings = new Button();
        Button endGame = new Button();
        Label winnerText = new Label();

        Button fgcolorselection = new Button();
        Rectangle fgcolorshow = new Rectangle();
        Button bgcolorselection = new Button();
        Rectangle bgcolorshow = new Rectangle();

        Button bgpictureselection = new Button();

        Button backToMainMenu = new Button();

        MediaPlayer filePlayer = new MediaPlayer();
        MediaPlayer mediaPlayer = new MediaPlayer();

        Button musicselection = new Button();

        //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public MainWindow()
        {
            InitializeComponent();

            //------------------------------------------------------------------------------------------------------------------------------------------//

            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline),
                new FrameworkPropertyMetadata { DefaultValue = 60 });

            //------------------------------------------------------------------------------------------------------------------------------------------//

            secondaryColor = bgcolor;
            mycanvas2.Background = new SolidColorBrush(secondaryColor);
            spielstart.Background = new SolidColorBrush(secondaryColor);
            settings.Background = new SolidColorBrush(secondaryColor);
            endGame.Background = new SolidColorBrush(secondaryColor);
            bgcolorshow.Fill = new SolidColorBrush(secondaryColor);
            backToMainMenu.Background = new SolidColorBrush(secondaryColor);
            fgcolorselection.Background = new SolidColorBrush(secondaryColor);
            bgcolorselection.Background = new SolidColorBrush(secondaryColor);
            bgpictureselection.Background = new SolidColorBrush(secondaryColor);
            musicselection.Background = new SolidColorBrush(secondaryColor);

            primaryColor = fgcolor;
            PongText.Foreground = new SolidColorBrush(primaryColor);
            punkteSpieler1.Foreground = new SolidColorBrush(primaryColor);
            punkteSpieler2.Foreground = new SolidColorBrush(primaryColor);
            player1.Stroke = new SolidColorBrush(primaryColor);
            player2.Stroke = new SolidColorBrush(primaryColor);
            myellipse.Stroke = new SolidColorBrush(primaryColor);
            winnerText.Foreground = new SolidColorBrush(primaryColor);
            spielstart.Foreground = new SolidColorBrush(primaryColor);
            settings.Foreground = new SolidColorBrush(primaryColor);
            endGame.Foreground = new SolidColorBrush(primaryColor);
            spielstart.BorderBrush = new SolidColorBrush(primaryColor);
            settings.BorderBrush = new SolidColorBrush(primaryColor);
            endGame.BorderBrush = new SolidColorBrush(primaryColor);
            fgcolorselection.Foreground = new SolidColorBrush(primaryColor);
            fgcolorselection.BorderBrush = new SolidColorBrush(primaryColor);
            fgcolorshow.Stroke = new SolidColorBrush(primaryColor);
            fgcolorshow.Fill = new SolidColorBrush(primaryColor);
            bgcolorselection.Foreground = new SolidColorBrush(primaryColor);
            bgcolorselection.BorderBrush = new SolidColorBrush(primaryColor);
            bgcolorshow.Stroke = new SolidColorBrush(primaryColor);
            bgpictureselection.Foreground = new SolidColorBrush(primaryColor);
            bgpictureselection.BorderBrush = new SolidColorBrush(primaryColor);
            musicselection.Foreground = new SolidColorBrush(primaryColor);
            musicselection.BorderBrush = new SolidColorBrush(primaryColor);
            backToMainMenu.Foreground = new SolidColorBrush(primaryColor);
            backToMainMenu.BorderBrush = new SolidColorBrush(primaryColor);

            player1.Fill = new SolidColorBrush(secondaryColor);
            player2.Fill = new SolidColorBrush(secondaryColor);
            myellipse.Fill = new SolidColorBrush(secondaryColor);

            //------------------------------------------------------------------------------------------------------------------------------------------//
            myellipse.Width = 50;
            myellipse.Height = 50;
            myellipse.StrokeThickness = 3;

            myellipse.Stroke = new SolidColorBrush(primaryColor);
            myellipse.Fill = new SolidColorBrush(secondaryColor);

            player1.Width = 30;
            player1.Height = 100;
            player1.StrokeThickness = 3;

            player1.Stroke = new SolidColorBrush(primaryColor);
            player1.Fill = new SolidColorBrush(secondaryColor);

            player2.Width = 30;
            player2.Height = 100;
            player2.StrokeThickness = 3;

            player2.Stroke = new SolidColorBrush(primaryColor);
            player1.Fill = new SolidColorBrush(secondaryColor);

            //------------------------------------------------------------------------------------------------------------------------------------------//

            Canvas.SetTop(myellipse, ballPosY);
            Canvas.SetLeft(myellipse, ballPosX);
            mycanvas2.Children.Add(myellipse);

            Canvas.SetTop(player1, spielerPos1);
            Canvas.SetLeft(player1, 40);
            mycanvas2.Children.Add(player1);

            Canvas.SetTop(player2, spielerPos2);
            Canvas.SetLeft(player2, 895);
            mycanvas2.Children.Add(player2);

            //------------------------------------------------------------------------------------------------------------------------------------------//

            Game();

            //------------------------------------------------------------------------------------------------------------------------------------------//

            async Task Game ()
            {
                //Button spielstart = new Button();

                spielstart.Height = 50;
                spielstart.Width = 200;
                spielstart.Content = "Spiel starten";
                spielstart.FontSize = 20;

                Canvas.SetTop(spielstart, 300);
                Canvas.SetLeft(spielstart, 400);
                mycanvas2.Children.Add(spielstart);

                spielstart.Click += Spielstart_Click;

                //------------------------------------------------------------------------------------------------------------------------------------------//

                //Button settings = new Button();
                settings.Height = 50;
                settings.Width = 200;
                settings.Content = "Einstellungen";
                settings.FontSize = 20;

                Canvas.SetTop(settings, 375);
                Canvas.SetLeft(settings, 400);
                mycanvas2.Children.Add(settings);

                settings.Click += Settings_Click;

                //------------------------------------------------------------------------------------------------------------------------------------------//

                //Button endGame = new Button();
                endGame.Height = 50;
                endGame.Width = 200;
                endGame.Content = "Beenden";
                endGame.FontSize = 20;

                Canvas.SetTop(endGame, 450);
                Canvas.SetLeft(endGame, 400);
                mycanvas2.Children.Add(endGame);

                endGame.Click += EndGame_Click;

                //------------------------------------------------------------------------------------------------------------------------------------------//

                //Button backToMainMenu = new Button();
                backToMainMenu.Height = 50;
                backToMainMenu.Width = 200;
                backToMainMenu.Content = "Hauptmenü";
                backToMainMenu.FontSize = 20;

                Canvas.SetTop(backToMainMenu, 375);
                Canvas.SetLeft(backToMainMenu, 400);
                mycanvas2.Children.Add(backToMainMenu);

                backToMainMenu.Click += BackToMainMenu_Click;

                //------------------------------------------------------------------------------------------------------------------------------------------//

                //Label winnerText = new Label();

                winnerText.Height = 100;
                winnerText.Width = 1000;
                winnerText.FontSize = 60;
                winnerText.Foreground = new SolidColorBrush(primaryColor);
                winnerText.HorizontalContentAlignment = HorizontalAlignment.Center;

                Canvas.SetTop(winnerText, 150);
                Canvas.SetLeft(winnerText, 0);
                mycanvas2.Children.Add(winnerText);

                //------------------------------------------------------------------------------------------------------------------------------------------//

                //Button fgcolorselection = new Button();
                fgcolorselection.Height = 30;
                fgcolorselection.Width = 100;
                fgcolorselection.Content = "Ändern";
                fgcolorselection.FontSize = 20;

                Canvas.SetTop(fgcolorselection, 175);
                Canvas.SetLeft(fgcolorselection, 250);
                mycanvas2.Children.Add(fgcolorselection);

                fgcolorselection.Click += Foregroundcolor_Click;
                
                //------------------------------------------------------------------------------------------------------------------------------------------//

                //Rectangle fgcolorshow = new Rectangle();

                fgcolorshow.Width = 100;
                fgcolorshow.Height = 30;
                fgcolorshow.StrokeThickness = 1;
                fgcolorshow.Stroke = new SolidColorBrush(primaryColor);

                Canvas.SetTop(fgcolorshow, 125);
                Canvas.SetLeft(fgcolorshow, 250);
                mycanvas2.Children.Add(fgcolorshow);


                //------------------------------------------------------------------------------------------------------------------------------------------//

                //Button bgcolorselection = new Button();
                bgcolorselection.Height = 30;
                bgcolorselection.Width = 100;
                bgcolorselection.Content = "Ändern";
                bgcolorselection.FontSize = 20;

                Canvas.SetTop(bgcolorselection, 175);
                Canvas.SetLeft(bgcolorselection, 100);
                mycanvas2.Children.Add(bgcolorselection);

                bgcolorselection.Click += Backgroundcolor_Click;

                //------------------------------------------------------------------------------------------------------------------------------------------//

                //Rectangle bgcolorshow = new Rectangle();

                bgcolorshow.Width = 100;
                bgcolorshow.Height = 30;
                bgcolorshow.StrokeThickness = 1;
                bgcolorshow.Stroke = new SolidColorBrush(primaryColor);;

                Canvas.SetTop(bgcolorshow, 125);
                Canvas.SetLeft(bgcolorshow, 100);
                mycanvas2.Children.Add(bgcolorshow);

                //------------------------------------------------------------------------------------------------------------------------------------------//

                //Button bgpictureselection
                bgpictureselection.Height = 30;
                bgpictureselection.Width = 150;
                bgpictureselection.Content = "Bild/GIF/Video";
                bgpictureselection.FontSize = 20;

                Canvas.SetTop(bgpictureselection, 175);
                Canvas.SetLeft(bgpictureselection, 400);
                mycanvas2.Children.Add(bgpictureselection);

                bgpictureselection.Click += Backgroundpictureselection_Click;

                //------------------------------------------------------------------------------------------------------------------------------------------//

                //Button musicselection
                musicselection.Height = 30;
                musicselection.Width = 150;
                musicselection.Content = "Audio";
                musicselection.FontSize = 20;

                Canvas.SetTop(musicselection, 175);
                Canvas.SetLeft(musicselection, 600);
                mycanvas2.Children.Add(musicselection);

                musicselection.Click += musicselection_Click;

                //------------------------------------------------------------------------------------------------------------------------------------------//

                for (;;)
                {
                    if (gameState == "inGame")
                    {
                        await Task.Delay(1);

                        punkteSpieler1.Visibility = Visibility.Visible;
                        punkteSpieler2.Visibility = Visibility.Visible;
                        player1.Visibility = Visibility.Visible;
                        player2.Visibility = Visibility.Visible;
                        myellipse.Visibility = Visibility.Visible;

                        spielstart.Visibility = Visibility.Hidden;
                        settings.Visibility = Visibility.Hidden;
                        endGame.Visibility = Visibility.Hidden;
                        PongText.Visibility = Visibility.Hidden;
                        backToMainMenu.Visibility = Visibility.Hidden;
                        bgpictureselection.Visibility = Visibility.Hidden;
                        musicselection.Visibility = Visibility.Hidden;

                        /*
                        spielerPos1 = Convert.ToInt32(ballPosY);
                        spielerPos2 = Convert.ToInt32(ballPosY);
                        */

                        ballPosX += ballSpeedX;
                        ballPosY += ballSpeedY;

                        if (ballPosY < 0 || ballPosY > 510)
                        {
                            ballSpeedY = -ballSpeedY;
                        }

                        Canvas.SetTop(myellipse, ballPosY);
                        Canvas.SetLeft(myellipse, ballPosX);

                        if (spielerPos1 > 5)
                        {
                            if (Keyboard.IsKeyDown(Key.W))
                            {
                                spielerPos1 -= 5;
                            }
                        }

                        if (spielerPos1 < 455)
                        {
                            if (Keyboard.IsKeyDown(Key.S))
                            {
                                spielerPos1 += 5;
                            }
                        }

                        if (spielerPos2 > 5)
                        {
                            if (Keyboard.IsKeyDown(Key.Up))
                            {
                                spielerPos2 -= 5;
                            }
                        }
                        if (spielerPos2 < 455)
                        {
                            if (Keyboard.IsKeyDown(Key.Down))
                            {
                                spielerPos2 += 5;
                            }
                        }

                        Canvas.SetTop(player1, spielerPos1); // Abstand zum oberen rand 
                        Canvas.SetLeft(player1, 40);

                        Canvas.SetTop(player2, spielerPos2); // Abstand zum oberen rand 
                        Canvas.SetLeft(player2, 895);

                        if (ballPosX >= (70 - Math.Abs(ballSpeedX)) && ballPosX <= (70 + Math.Abs(ballSpeedX)) && ballPosY >= spielerPos1 - 45 && ballPosY <= spielerPos1 + 95 || ballPosX >= (845 - Math.Abs(ballSpeedX)) && ballPosX <= (845 + Math.Abs(ballSpeedX)) && ballPosY >= spielerPos2 - 45 && ballPosY <= spielerPos2 + 95)
                        {
                            ballSpeedX = -ballSpeedX;
                            if (ballSpeedX < 0)
                            {
                                ballSpeedX -= 0.25;
                            }
                            if (ballSpeedX > 0)
                            {
                                ballSpeedX += 0.25;
                            }
                            if (ballSpeedY < 0)
                            {
                                ballSpeedY -= 0.25;
                            }
                            if (ballSpeedY > 0)
                            {
                                ballSpeedY += 0.25;
                            }
                        }

                        if (ballPosX < -50)
                        {
                            punkte2 += 1;

                            ballPosX = 475;
                            ballPosY = 275;
                            //spielerPos1 = 250;
                            //spielerPos2 = 250;
                            ballSpeedX = 3;
                            ballSpeedY = 3;
                        }

                        if (ballPosX > 1000)
                        {
                            punkte1 += 1;

                            ballPosX = 475;
                            ballPosY = 275;
                            //spielerPos1 = 250;
                            //spielerPos2 = 250;
                            ballSpeedX = 3;
                            ballSpeedY = 3;
                        }

                        punkteSpieler1.Content = punkte1;
                        punkteSpieler2.Content = punkte2;

                        if (punkte1 == 9)
                        {
                            winnerName = player1Name;
                            gameState = "winnerScreen";

                            gameReset();
                        }

                        if (punkte2 == 9)
                        {
                            winnerName = player2Name;
                            gameState = "winnerScreen";

                            gameReset();
                        }

                        filePlayer.MediaEnded += new EventHandler(Media_Ended);
                    }

                    else if (gameState == "mainMenu")
                    {
                        await Task.Delay(1);

                        punkteSpieler1.Visibility = Visibility.Hidden;
                        punkteSpieler2.Visibility = Visibility.Hidden;
                        player1.Visibility = Visibility.Hidden;
                        player2.Visibility = Visibility.Hidden;
                        myellipse.Visibility = Visibility.Hidden;
                        backToMainMenu.Visibility = Visibility.Hidden;
                        winnerText.Visibility = Visibility.Hidden;
                        fgcolorshow.Visibility = Visibility.Hidden;
                        fgcolorselection.Visibility = Visibility.Hidden;
                        bgcolorshow.Visibility = Visibility.Hidden;
                        bgcolorselection.Visibility = Visibility.Hidden;
                        bgpictureselection.Visibility = Visibility.Hidden;
                        musicselection.Visibility = Visibility.Hidden;

                        PongText.Visibility = Visibility.Visible;
                        spielstart.Visibility = Visibility.Visible;
                        settings.Visibility = Visibility.Visible;
                        endGame.Visibility = Visibility.Visible;

                        filePlayer.MediaEnded += new EventHandler(Media_Ended);
                    }

                    else if (gameState == "einstellungsmenu")
                    {
                        await Task.Delay(1);

                        spielstart.Visibility = Visibility.Hidden;
                        settings.Visibility = Visibility.Hidden;
                        endGame.Visibility = Visibility.Hidden;
                        PongText.Visibility = Visibility.Hidden;
                        backToMainMenu.Visibility = Visibility.Hidden;

                        backToMainMenu.Visibility = Visibility.Visible;
                        fgcolorshow.Visibility = Visibility.Visible;
                        fgcolorselection.Visibility = Visibility.Visible;
                        bgcolorshow.Visibility = Visibility.Visible;
                        bgcolorselection.Visibility = Visibility.Visible;
                        bgpictureselection.Visibility = Visibility.Visible;
                        musicselection.Visibility = Visibility.Visible;

                        filePlayer.MediaEnded += new EventHandler(Media_Ended);
                    }

                    else if (gameState == "winnerScreen")
                    {
                        await Task.Delay(1);

                        punkteSpieler1.Visibility = Visibility.Hidden;
                        punkteSpieler2.Visibility = Visibility.Hidden;
                        player1.Visibility = Visibility.Hidden;
                        player2.Visibility = Visibility.Hidden;
                        myellipse.Visibility = Visibility.Hidden;
                        PongText.Visibility = Visibility.Hidden;
                        spielstart.Visibility = Visibility.Hidden;
                        settings.Visibility = Visibility.Hidden;
                        endGame.Visibility = Visibility.Hidden;
                        bgpictureselection.Visibility = Visibility.Hidden;
                        musicselection.Visibility = Visibility.Hidden;

                        winnerText.Content = winnerName + " hat gewonnen!";

                        backToMainMenu.Visibility = Visibility.Visible;
                        winnerText.Visibility = Visibility.Visible;

                        filePlayer.MediaEnded += new EventHandler(Media_Ended);
                    }
                }
            }
        }

        private void Spielstart_Click(object sender, RoutedEventArgs e)
        {
            gameState = "inGame";
            gameStart();
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            gameState = "einstellungsmenu";
        }
        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            gameState = "mainMenu";
        }
        private void EndGame_Click (object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        void Foregroundcolor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog Foregroundcolor = new System.Windows.Forms.ColorDialog(); //Erzeuge neuen ColorDialog

            if (Foregroundcolor.ShowDialog() == System.Windows.Forms.DialogResult.OK) //Aufruf und Auswertung von Backgroundcolor
            {
                Color fgcolor = Color.FromArgb(Foregroundcolor.Color.A, Foregroundcolor.Color.R, Foregroundcolor.Color.G, Foregroundcolor.Color.B);

                /*
                config.AppSettings.Settings["savedFgAColor"].Value = Convert.ToString(Foregroundcolor.Color.A);
                config.AppSettings.Settings["savedFgRColor"].Value = Convert.ToString(Foregroundcolor.Color.R);
                config.AppSettings.Settings["savedFgGColor"].Value = Convert.ToString(Foregroundcolor.Color.G);
                config.AppSettings.Settings["savedFgBColor"].Value = Convert.ToString(Foregroundcolor.Color.B);
                */

                primaryColor = fgcolor;
                PongText.Foreground = new SolidColorBrush(primaryColor);
                punkteSpieler1.Foreground = new SolidColorBrush(primaryColor);
                punkteSpieler2.Foreground = new SolidColorBrush(primaryColor);
                player1.Stroke = new SolidColorBrush(primaryColor);
                player2.Stroke = new SolidColorBrush(primaryColor);
                myellipse.Stroke = new SolidColorBrush(primaryColor);
                winnerText.Foreground = new SolidColorBrush(primaryColor);
                spielstart.Foreground = new SolidColorBrush(primaryColor);
                settings.Foreground = new SolidColorBrush(primaryColor);
                endGame.Foreground = new SolidColorBrush(primaryColor);
                spielstart.BorderBrush = new SolidColorBrush(primaryColor);
                settings.BorderBrush = new SolidColorBrush(primaryColor);
                endGame.BorderBrush = new SolidColorBrush(primaryColor);
                
                fgcolorselection.Foreground = new SolidColorBrush(primaryColor);
                fgcolorselection.BorderBrush = new SolidColorBrush(primaryColor);
                fgcolorshow.Stroke = new SolidColorBrush(primaryColor);
                fgcolorshow.Fill = new SolidColorBrush(primaryColor);

                bgcolorselection.Foreground = new SolidColorBrush(primaryColor);
                bgcolorselection.BorderBrush = new SolidColorBrush(primaryColor);
                bgcolorshow.Stroke = new SolidColorBrush(primaryColor);

                backToMainMenu.Foreground = new SolidColorBrush(primaryColor);
                backToMainMenu.BorderBrush = new SolidColorBrush(primaryColor);

                bgpictureselection.Foreground = new SolidColorBrush(primaryColor);
                bgpictureselection.BorderBrush = new SolidColorBrush(primaryColor);

                musicselection.Foreground = new SolidColorBrush(primaryColor);
                musicselection.BorderBrush = new SolidColorBrush(primaryColor);
            }
        }
        void Backgroundcolor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog Backgroundcolor = new System.Windows.Forms.ColorDialog(); //Erzeuge neuen ColorDialog

            if (Backgroundcolor.ShowDialog() == System.Windows.Forms.DialogResult.OK) //Aufruf und Auswertung von Backgroundcolor
            {
                Color bgcolor = Color.FromArgb(Backgroundcolor.Color.A, Backgroundcolor.Color.R, Backgroundcolor.Color.G, Backgroundcolor.Color.B);

                /*
                config.AppSettings.Settings["savedBgAColor"].Value = Convert.ToString(Backgroundcolor.Color.A);
                config.AppSettings.Settings["savedBgRColor"].Value = Convert.ToString(Backgroundcolor.Color.R);
                config.AppSettings.Settings["savedBgGColor"].Value = Convert.ToString(Backgroundcolor.Color.G);
                config.AppSettings.Settings["savedBgBColor"].Value = Convert.ToString(Backgroundcolor.Color.B);
                */

                secondaryColor = bgcolor;
                mycanvas2.Background = new SolidColorBrush(secondaryColor);
                spielstart.Background = new SolidColorBrush(secondaryColor);
                settings.Background = new SolidColorBrush(secondaryColor);
                endGame.Background = new SolidColorBrush(secondaryColor);
                bgcolorshow.Fill = new SolidColorBrush(secondaryColor);
                backToMainMenu.Background = new SolidColorBrush(secondaryColor);
                fgcolorselection.Background = new SolidColorBrush(secondaryColor);
                bgcolorselection.Background = new SolidColorBrush(secondaryColor);
                bgpictureselection.Background = new SolidColorBrush(secondaryColor);
                musicselection.Background = new SolidColorBrush(secondaryColor);

                player1.Fill = new SolidColorBrush(secondaryColor);
                player2.Fill = new SolidColorBrush(secondaryColor);
                myellipse.Fill = new SolidColorBrush(secondaryColor);
            }
        }

        void Backgroundpictureselection_Click(object sender, RoutedEventArgs e)
        {
            //Select File
            OpenFileDialog myopenFileDialog = new OpenFileDialog();           
            myopenFileDialog.Filter = "MP4 files (*.mp4)|*.mp4|All files(*.*)|*.*";

            if (myopenFileDialog.ShowDialog() == true)
            {
                aktuelleDatei = myopenFileDialog.FileName;
                filePlayer.Open(new Uri(aktuelleDatei));
                bgmedia.Source = new Uri(aktuelleDatei);
                filePlayer.Play();
            }
        }

        void musicselection_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                aktuelleMusic = openFileDialog.FileName;
                mediaPlayer.Open(new Uri(aktuelleMusic));
                mediaPlayer.Play();
            }
        }

        void gameReset()
        {
            ballPosX = 475;
            ballPosY = 275;
            ballSpeedX = 0;
            ballSpeedY = 0;
            spielerPos1 = 250;
            spielerPos2 = 250;
            punkte1 = 0;
            punkte2 = 0;
        }

        void gameStart()
        {
            ballSpeedX = 3;
            ballSpeedY = 3;
        }

        void Media_Ended(object sender, EventArgs e)
        {
            //Dauerschleife
            filePlayer.Open(new Uri(aktuelleDatei));
            bgmedia.Source = new Uri(aktuelleDatei);
            filePlayer.Position = TimeSpan.Zero;
            filePlayer.Play();
        }

        void Music_Ended(object sender, EventArgs e)
        {
            //Dauerschleife
            mediaPlayer.Open(new Uri(aktuelleMusic));
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}
